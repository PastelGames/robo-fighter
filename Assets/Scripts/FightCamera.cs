using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FightCamera : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;

    private Vector3 velocity;

    public float maxZoom = 40;
    public float minZoom = 10;
    public float zoomLimiter = 10;

    public float smoothTime = 0.5f;

    public float cameraZoomLerpSpeed;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (targets.Count == 0) return;
        MoveCamera();
    }

    public IEnumerator Shake(int durationInFrames, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        int elapsed = 0;

        while (elapsed < durationInFrames)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition += new Vector3(x, y, 0);

            elapsed++;

            yield return new WaitForFixedUpdate();
        }
    }

    float GetGreatestDistance()
    {
        Bounds bounds = CreateBounds();

        return bounds.size.x;
    }

    private void MoveCamera()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        Bounds bounds = CreateBounds();

        return bounds.center;
    }

    private Bounds CreateBounds()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform target in targets)
        {
            bounds.Encapsulate(target.position);
        }

        return bounds;
    }
}

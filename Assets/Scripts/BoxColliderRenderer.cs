using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(LineRenderer))]
public class BoxColliderRenderer : MonoBehaviour
{
    private LineRenderer _lr;
    private BoxCollider2D _boxCollider2D;

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _lr = GetComponent<LineRenderer>();

        _lr.positionCount = 5;
        _lr.startWidth = .05f;
    }

    void Update()
    {
        Vector2 width = new Vector2(_boxCollider2D.bounds.size.x, 0);

        Vector2 bottomLeft = _boxCollider2D.bounds.min;
        Vector2 topRight = _boxCollider2D.bounds.max;
        Vector2 bottomRight = bottomLeft + width;
        Vector2 topLeft = topRight - width;

        _lr.SetPositions(new Vector3[5]
        {
            topLeft, topRight, bottomRight, bottomLeft, topLeft
        }); ;
    }
}

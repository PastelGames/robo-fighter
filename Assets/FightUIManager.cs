using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightUIManager : MonoBehaviour
{
    public Fighter player1;
    public Fighter player2;

    public TMP_Text player1HealthText;
    public TMP_Text player2HealthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthStatus();
    }

    void UpdateHealthStatus()
    {
        player1HealthText.text = player1.currentHP.ToString();
        player2HealthText.text = player2.currentHP.ToString();
    }
}

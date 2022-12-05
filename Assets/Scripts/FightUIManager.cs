using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightUIManager : MonoBehaviour
{
    public Fighter player1;
    public Fighter player2;

    public Slider player1HealthSlider;
    public Slider player2HealthSlider;

    public Image player1CooldownWheel;
    public Image player2CooldownWheel;

    public TMP_Text player1CooldownValue;
    public TMP_Text player2CooldownValue;

    [SerializeField]
    private TMP_Text _roundTimeRemainingText;

    public RoundTickHolder player1RoundTickHolder;
    public RoundTickHolder player2RoundTickHolder;

    public TMP_Text player1ComboCounter;
    public TMP_Text player2ComboCounter;

    // Start is called before the first frame update
    void Start()
    {
        player1HealthSlider.maxValue = player1.startingHP;
        player2HealthSlider.maxValue = player2.startingHP;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthStatus(player1, player1HealthSlider);
        UpdateHealthStatus(player2, player2HealthSlider);
        UpdateCooldownStatus(player1, player1CooldownWheel, player1CooldownValue);
        UpdateCooldownStatus(player2, player2CooldownWheel, player2CooldownValue);
        UpdateFighterComboCounter(player1);
        UpdateFighterComboCounter(player2);
    }

    public void UpdateFighterComboCounter(Fighter fighter)
    {
        FighterController fighterController = fighter.GetComponent<FighterController>();
        if (fighterController.playerSlot == PlayerSlot.Player1)
        {
            UpdateComboCounterStatusText(player2ComboCounter, fighter.comboHitCount);
        }
        else
        {
            UpdateComboCounterStatusText(player1ComboCounter, fighter.comboHitCount);
        }
    }

    private void UpdateComboCounterStatusText(TMP_Text comboCounterText, int comboHitCount)
    {
        if (comboHitCount > 1)
        {
            comboCounterText.gameObject.SetActive(true);
            comboCounterText.text = $"{comboHitCount} Hits";
        }
        else
        {
            comboCounterText.gameObject.SetActive(false);
        }
    }

    public void UpdateRoundTimeRemainingStatus(float timeRemaining)
    {
        _roundTimeRemainingText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }

    void UpdateCooldownStatus(Fighter fighter, Image wheel, TMP_Text value)
    {
        if (fighter.specialAttackCooldownRemaining > 0)
        {
            value.gameObject.SetActive(true);
            value.text = Mathf.CeilToInt(fighter.specialAttackCooldownRemaining).ToString();
        }
        else
        {
            value.gameObject.SetActive(false);
        }
        wheel.fillAmount = 1 - (fighter.specialAttackCooldownRemaining / fighter.specialAttackCooldownDuration);
    }

    void UpdateHealthStatus(Fighter fighter, Slider slider)
    {
        slider.value = fighter.currentHP;
    }
}

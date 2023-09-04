using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : Singleton<UI_InGame>
{
    [Header("Player Stat Slider")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Main Stat_Img")]
    [SerializeField] private Image playerHp;
    [SerializeField] private Image playerExp;
    [SerializeField] private Image playerTrauma;

    [Header("Main Stat_Text")]
    [SerializeField] private TextMeshProUGUI HpTMP;
    [SerializeField] private TextMeshProUGUI ExpTMP;
    [SerializeField] private TextMeshProUGUI levelTMP;
    [SerializeField] private TextMeshProUGUI traumaTMP;

    [Space]
    [SerializeField] private Image dashImg;
    [SerializeField] private Image circleFireImg;
    [SerializeField] private Image blackholeImg;
    [SerializeField] private Image FlaskImg;


    [Header("Score Info")]
    [SerializeField] private TextMeshProUGUI curSouls;
    [SerializeField] private float soulsAmount;
    [SerializeField] private float increaseRate = 100;



    // Health
    private float UI_curHp;
    private float UI_maxHp;

    // Exp
    private float UI_curExp;
    private float UI_expToNextLevel;

    // Trauma
    private float UI_curTrauma;
    private float UI_maxTrauma;


    private SkillManager skills;

    private void Start()
    {
        skills = SkillManager.Instance;
    }

    private void Update()
    {
        UpdateUI();
        UpdateScoreUI();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            SetCooldownOf(dashImg);
        if(Input.GetKeyDown(KeyCode.Z))
            SetCooldownOf(circleFireImg);
        if(Input.GetKeyDown(KeyCode.E))
            SetCooldownOf(blackholeImg);
        if (Input.GetKeyDown(KeyCode.F))
            SetCooldownOf(FlaskImg);

        CheckCoolDownOf(dashImg, skills.dash.coolDown);
        CheckCoolDownOf(circleFireImg, skills.circleFire.coolDown);
        CheckCoolDownOf(blackholeImg, skills.blackhole.coolDown);
        CheckCoolDownOf(FlaskImg, Inventory.Instance.flaskCooldown);
    }

    private void UpdateUI()
    {
        playerHp.fillAmount = Mathf.Lerp(playerHp.fillAmount, UI_curHp / UI_maxHp, 10f * Time.deltaTime);
        playerExp.fillAmount = Mathf.Lerp(playerExp.fillAmount, UI_curExp / UI_expToNextLevel, 10f * Time.deltaTime);
        playerTrauma.fillAmount = Mathf.Lerp(playerTrauma.fillAmount, UI_curTrauma / UI_maxTrauma, 10f * Time.deltaTime);

        HpTMP.text = $"{UI_curHp} / {UI_maxHp}";
        ExpTMP.text = $"{((UI_curExp / UI_expToNextLevel) * 100):F2}%";
        traumaTMP.text = $"{UI_curTrauma} / {UI_maxTrauma}";
        levelTMP.text = $"{playerStats.Curlevel}";
    }

    private void UpdateScoreUI()
    {
        if (soulsAmount < PlayerManager.Instance.GetScore())
            soulsAmount += Time.deltaTime * increaseRate;
        else
            soulsAmount = PlayerManager.Instance.GetScore();


        curSouls.text = ((int)soulsAmount).ToString("#,#");
    }

    public void UpdateHealth(float New_curHp, float New_maxHp)
    {
        UI_curHp = New_curHp;
        UI_maxHp = New_maxHp;
    }

    public void UpdateExp(float New_curExp, float New_expToNextLevel)
    {
        UI_curExp = New_curExp;
        UI_expToNextLevel = New_expToNextLevel;
    }

    public void UpdateTrauma(float New_curTrauma, float New_maxTrauma)
    {
        UI_curTrauma = New_curTrauma;
        UI_maxTrauma = New_maxTrauma;
    }


    private void SetCooldownOf(Image _img)
    {
        if (_img.fillAmount <= 0)
            _img.fillAmount = 1;
    }

    private void CheckCoolDownOf(Image _img, float _coolDown)
    {
        if (_img.fillAmount > 0)
            _img.fillAmount -= 1 / _coolDown * Time.deltaTime;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHpBar : MonoBehaviour
{
    [SerializeField] private Image HP_Bar;

    private float curHp;
    private float maxHp;

    private void Update()
    {
        HP_Bar.fillAmount = Mathf.Lerp(HP_Bar.fillAmount, curHp / maxHp, 10f * Time.deltaTime);
    }
    public void ModifyHealth(float p_CurHp, float p_MaxHp)
    {
        curHp = p_CurHp;
        maxHp = p_MaxHp;
    }

}

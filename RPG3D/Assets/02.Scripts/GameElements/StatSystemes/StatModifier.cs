using RPG.GameElements.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//MonoBehaviour �� ����? �׳� ���������� ��� ������ �ִ����� ���� �����͸� SriotableObject �������� ����Ʈ�� �־�η� ��Ÿ�ӿ� StatModifier �����ص� ��.


public enum StatModifiyingOption
{
    None,
    AddFlat,
    AddPercent,
    MulPercent
}


public class StatModifier : MonoBehaviour
{
    public StatType type;
    public float value;

    

}

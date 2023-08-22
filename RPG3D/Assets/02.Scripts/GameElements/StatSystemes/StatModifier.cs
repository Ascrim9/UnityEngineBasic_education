using RPG.GameElements.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour �� ���� ��ӹ��� �ʿ�� ����.. �׳� ���������ڸ� ��� �������־���ϴ����� ����
// �����͸� ScriptableObject �������� ����Ʈ�� �־�ΰ� ��Ÿ�ӿ� StatModifier �����ؼ� �ᵵ��.

public enum StatModifyingOption
{
    None,
    AddFlat,
    AddPercent,
    MulPercent,
}

public class StatModifier : MonoBehaviour
{
    public StatType type;
    public StatModifyingOption modifyingOption;
    public float value;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public Dash_Skill dash { get; private set; }
    public Clone_Skill clone { get; private set; }  
    public CircleFire_Skill circleFire { get; private set; }
    public Blackhole_Skill blackhole { get; private set; }
    private void Start()
    {
        dash = GetComponent<Dash_Skill>();
        clone = GetComponent<Clone_Skill>();
        circleFire = GetComponent<CircleFire_Skill>();
        blackhole = GetComponent<Blackhole_Skill>();
    }
}

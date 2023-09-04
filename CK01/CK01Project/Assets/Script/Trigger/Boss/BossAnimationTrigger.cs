using UnityEngine;

public class BossAnimationTriggers : MonoBehaviour
{
    private Boss boss => GetComponentInParent<Boss>();

    private void AnimationTrigger()
    {
        boss.AnimationFinishTrigger();
    }
    private void SKillTrigger()
    {
        boss.SkillFinishTrigger();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{

    private Player player => GetComponentInParent<Player>();
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }


    private void ThrowCircleFireProjectile()
    {
        SkillManager.Instance.circleFire.CreateProjectile();
    }
}

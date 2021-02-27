using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestis : MonoBehaviour
{
    public Animator characterAnimator;
    private GameObject particles;
    // Start is called before the first frame update
    public void Attack1 ()
    {
        characterAnimator.SetTrigger("3letter");


    }
    public void Attack2()
    {
        characterAnimator.SetTrigger("4letter");
    }
    public void InCombatT()
    {
        characterAnimator.SetBool("inCombat", true);
    }
        public void InCombatF()
    {
        characterAnimator.SetBool("inCombat", false);
    }
    public void Celebrate()
    {
        characterAnimator.SetTrigger("celebrate");
    }
    public void Die()
    {
        characterAnimator.SetTrigger("isDead");
    }
    public void GotHit()
    {
        characterAnimator.SetTrigger("gotHit");
    }
}

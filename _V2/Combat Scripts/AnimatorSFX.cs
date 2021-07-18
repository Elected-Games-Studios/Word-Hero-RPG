using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSFX : StateMachineBehaviour
{
    [SerializeField]
    private AudioClip SFX;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<AudioSource>().clip = SFX;
        animator.gameObject.GetComponent<AudioSource>().Play();
    }
}

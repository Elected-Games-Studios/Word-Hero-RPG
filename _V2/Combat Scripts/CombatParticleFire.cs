using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatParticleFire : StateMachineBehaviour
{
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private float timeToExecute;
    [SerializeField]
    private bool isEnemyAttacking = false;
    [SerializeField]
    private AudioClip attackSoundFX;

    private GameObject animatingCharacter;
    private AudioSource characterSound;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!isEnemyAttacking)
        {
           
        }
        animatingCharacter = animator.gameObject;
        characterSound = animatingCharacter.GetComponent<AudioSource>();
        FireParticle();
    }
    private void FireParticle()
    {
        characterSound.clip = attackSoundFX;
        characterSound.Play();
        particle.GetComponent<ParticleSystem>().startDelay = timeToExecute;
        Instantiate(particle, animatingCharacter.transform);
    }
}

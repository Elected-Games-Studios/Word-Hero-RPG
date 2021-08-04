using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DisableEnemy : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CombatLogic.instance.enemyIsDying();
        if(!CombatLogic.instance.beatLevel)
        {
            CombatLogic.instance.displayNewEnemyPrefab();
        }

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SetActive(false);
    }
}

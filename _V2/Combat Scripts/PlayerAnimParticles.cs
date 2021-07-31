using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//fuckk all of this....
public class MonoBehaviourish : MonoBehaviour
{
    //private float attackTimeOffset;
    //private Transform spawnLocation;
    //private GameObject particleFX;
    //private GameObject spawnedFX;

    //public MonoBehaviourish(PlayerAnimParticles pAnimPart)
    //{
    //    attackTimeOffset = pAnimPart.attackTimeOffset;
    //    spawnedFX = pAnimPart.spawnedFX;
    //    spawnLocation = pAnimPart.spawnLocation;
    //    particleFX = pAnimPart.particleFX;
    //}

    //public void JuLeiDoTheThing()
    //{
    //    StartCoroutine(generateParticle());
    //}
    //public IEnumerator generateParticle()
    //{
    //    Debug.Log("Generating Particle...");
    //    yield return new WaitForSeconds(attackTimeOffset);
    //    if (spawnLocation)
    //        spawnedFX = Instantiate(particleFX, spawnLocation);
    //    yield return new WaitForSeconds(3f);
    //    Destroy(spawnedFX);
    //}
}
public class PlayerAnimParticles : StateMachineBehaviour
{
    private MonoBehaviourish monoB;
    public GameObject particleFX;

    [SerializeField]
    private attackType aType;

    [SerializeField]
    private float startFrame;

    private float attackTimeOffset;
    private Transform spawnLocation;
    private GameObject spawnedFX;
    private enum attackType
    {
        melee,
        ranged,
        AOE
    }



    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monoB = animator.gameObject.AddComponent<MonoBehaviourish>();

        attackTimeOffset = (1f/24f) * startFrame;
        Debug.Log("Attacking..." + "Offset = " + attackTimeOffset);
        switch (aType)
        {
            case attackType.melee:
                spawnLocation = GameObject.FindGameObjectWithTag("Enemy").transform;
                break;

            case attackType.ranged:
                spawnLocation = animator.transform;
                break;

            case attackType.AOE:
                spawnLocation = GameObject.FindGameObjectWithTag("Enemy").transform;
                break;
        }
        monoB.StartCoroutine(generateParticle());
        //monoB.JuLeiDoTheThing();
    }
    public IEnumerator generateParticle()
    {
        Debug.Log("Generating Particle...");
        yield return new WaitForSeconds(attackTimeOffset);
        if (spawnLocation)
            spawnedFX = Instantiate(particleFX, spawnLocation);
        //yield return new WaitForSeconds(3f);
        //Destroy(spawnedFX);
    }

}

using UnityEngine;

public class AttackAnims : MonoBehaviour
{
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject attack4;
    public GameObject attack5;
    public GameObject attack6;
    public GameObject limitBreak;
    public AudioClip attackAudio1;
    public AudioClip attackAudio2;
    public AudioClip attackAudio3;
    public AudioClip attackAudio4;
    public AudioClip attackAudio5;
    public AudioClip attackAudio6;
    public AudioClip limitBreakAudio;
    AudioSource audioSource;
    GameObject player;

    private void Start()
    {
        player = transform.parent.gameObject;
        audioSource = player.GetComponent<AudioSource>();
    }
    public void Attack1()
    {
        Instantiate(attack1, player.transform);
        audioSource.clip = attackAudio1;
        audioSource.Play();
    }

    public void Attack2()
    {
        Instantiate(attack2, player.transform);
        audioSource.clip = attackAudio2;
        audioSource.Play();
    }

    public void Attack3()
    {
        Instantiate(attack3, player.transform);
        audioSource.clip = attackAudio3;
        audioSource.Play();
    }

    public void Attack4()
    {
        Instantiate(attack4, player.transform);
        audioSource.clip = attackAudio4;
        audioSource.Play();
    }

    public void Attack5()
    {
        Instantiate(attack5, player.transform);
        audioSource.clip = attackAudio5;
        audioSource.Play();
    }

    public void Attack6()
    {
        Instantiate(attack6, player.transform);
        audioSource.clip = attackAudio6;
        audioSource.Play();
    }

    public void LimitBreak()
    {
        Instantiate(limitBreak, player.transform);
        audioSource.clip = limitBreakAudio;
        audioSource.Play();
        GameObject.FindGameObjectWithTag("Manager").GetComponent<CombatManager>().mana = 0;

    }
}

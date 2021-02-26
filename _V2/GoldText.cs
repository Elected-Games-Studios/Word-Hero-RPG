using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{
    [SerializeField]
    private CombatLogic CL;
    private void Awake()
    {
        onParticleLeaveScreen.particleLeftScreen += AddGold;
    }
    void Start()
    {
        gameObject.GetComponent<Text>().text = "0";
    }

    private void AddGold()
    {
        int temp = Int32.Parse(gameObject.GetComponent<Text>().text);
        temp++;
        gameObject.GetComponent<Text>().text = temp.ToString();

    }
    private void OnDisable()
    {
        onParticleLeaveScreen.particleLeftScreen -= AddGold;
    }
}

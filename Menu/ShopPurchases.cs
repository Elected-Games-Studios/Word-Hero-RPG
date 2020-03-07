using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPurchases : MonoBehaviour
{
    PlayerStats stats;
    PlayServices cloud;
    // Start is called before the first frame update
    void Start()
    {
        stats = PlayerStats.Instance;
        cloud = PlayServices.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SmallChestPurchase()
    {
        stats.playerGold += 500;
        stats.timePU += 2;
        stats.healthPU += 4;
        stats.secondWindPU += 4;
        stats.shieldPU += 8;
        cloud.SaveData();
        //cloud.feedback.text = "Purchase Successfull";
    }

    public void MediumChestPurchase()
    {
        stats.playerGold += 1250;
        stats.timePU += 5;
        stats.healthPU += 10;
        stats.secondWindPU += 10;
        stats.shieldPU += 25;
        cloud.SaveData();
        //cloud.feedback.text = "Purchase Successfull";
    }

    public void LargeChestPurchase()
    {
        stats.playerGold += 3000;
        stats.timePU += 12;
        stats.healthPU += 25;
        stats.secondWindPU += 25;
        stats.shieldPU += 70;
        cloud.SaveData();
        //cloud.feedback.text = "Purchase Successfull";
    }

    public void EnormousChestPurchase()
    {
        stats.playerGold += 9000;
        stats.timePU += 50;
        stats.healthPU += 75;
        stats.secondWindPU += 75;
        stats.shieldPU += 230;
        cloud.SaveData();
        //cloud.feedback.text = "Purchase Successfull";
    }

    public void Gold100()
    {
        stats.playerGold += 100;
        cloud.SaveData();
        //cloud.feedback.text = "Purchase Successfull";
    }

    public void Gold300()
    {
        stats.playerGold += 300;
        cloud.SaveData();
        //cloud.feedback.text = "Purchase Successfull";
    }

    public void Gold750()
    {
        stats.playerGold += 750;
        cloud.SaveData();
        //cloud.feedback.text = "Purchase Successfull";
    }

    public void Gold2000()
    {
        stats.playerGold += 2000;
        cloud.SaveData();
        //cloud.feedback.text = "Purchase Successfull";
    }

    public void Gold5500()
    {
        stats.playerGold += 5500;
        cloud.SaveData();
        //cloud.feedback.text = "Purchase Successfull";
    }

    public void PurchaseFailed()
    {
        Debug.Log("Purchase Failed");
        //cloud.feedback.text = "Purchase Failed";
    }
}

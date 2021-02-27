using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundTweener : MonoBehaviour
{
    public List<Image> backgrounds;

    private GameObject previous;

    //public void setBackground(int level)
    //{
    //    backgrounds[level].SetActive(true);
    //    previous = backgrounds[level];
    //}
 
    public void changeBackground(int level)
    {
        if (level != 0) { previous = backgrounds[level - 1].gameObject; }
        if (previous) { removePrevious(); }
        backgrounds[level].gameObject.SetActive(true);
        
            Debug.Log("level " + level);
            LeanTween.alpha(backgrounds[level].GetComponent<RectTransform>(), 0f, 0f);
            LeanTween.alpha(backgrounds[level].GetComponent<RectTransform>(), .5f, 2f);//take 2 sec
        
       
    }

    private void removePrevious()
    {
        previous.SetActive(true);
        //test line
        LeanTween.alpha(previous.GetComponent<RectTransform>(), .5f, 0f);
        //end test
        LeanTween.alpha(previous.GetComponent<RectTransform>(), 0f, 2f).setOnComplete(setInactive); 
    }
    private void setInactive()
    {
        previous.SetActive(false);
    }
}

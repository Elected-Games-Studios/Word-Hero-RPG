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
    private void Awake()
    {
        previous = backgrounds[0].gameObject;
    }
    public IEnumerator changeBackground(int level)
    {

        fadePrevious();
        backgrounds[level].gameObject.SetActive(true);
        LeanTween.alpha(backgrounds[level].GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(backgrounds[level].GetComponent<RectTransform>(), 1f, 1f);
        yield return new WaitForSeconds(1f);
        previous = backgrounds[level].gameObject;
        
    }

    private void fadePrevious()
    {

        previous.SetActive(false);
    }
}

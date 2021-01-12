using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //for adding new regions
    private int sceneToLoad = 6; //temporary

    public void OnEnable()//technically this is called extra time on first load of CanvasDisplayManager, but doesn't finish because the object(LoaderObj) is immediately deactivated before the 3 seconds. Might be a bug later, but it works for now.
    {
            StartCoroutine(LoadCoroutine());
        //scenetoload = based on Static Region in GameManager
    }
    IEnumerator LoadCoroutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneToLoad);
    }
}

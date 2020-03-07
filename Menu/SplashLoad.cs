using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadScene", 10);
    }
    
    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

}

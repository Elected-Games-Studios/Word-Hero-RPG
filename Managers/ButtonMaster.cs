using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMaster : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene(1);
    }
}

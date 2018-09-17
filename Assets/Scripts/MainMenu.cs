using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void tapToBegin()
    {
        SceneManager.LoadScene("Main");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    public GameObject highScore;
    public GameObject soundButton;
    public GameObject panel;

    public Text hScore;

    public Sprite soundOff;
    public Sprite soundOn;

    private bool sound = true;
    private bool validInput;

    private void Start()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore.SetActive(true);
            hScore.text = PlayerPrefs.GetInt("highScore").ToString();
        }
    }
    
    private void Update()
    {
        validateInput();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetMouseButton(0) && validInput)
            SceneManager.LoadScene("Main");

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && validInput)
            SceneManager.LoadScene("Main");
    }


    public void Sound()
    {
        if(sound == true)
        {
            sound = false;
            AudioListener.volume = 0;
            soundButton.GetComponent<Image>().sprite = soundOff;
        }
        else
        {
            sound = true;
            AudioListener.volume = 1;
            soundButton.GetComponent<Image>().sprite = soundOn;
        }
    }

    public void Panel()
    {
        panel.SetActive(true);
    }

    public void mainMenu()
    {
        panel.SetActive(false);
    }

    void validateInput()
    {

        //DESKTOP
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                validInput = false;
            else
                validInput = true;
        }

        //MOBILE
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                validInput = false;
            else
                validInput = true;
        }
    }
}

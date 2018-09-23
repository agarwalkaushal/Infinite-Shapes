﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;
using UnityEngine.Audio;

public class Gameplay : MonoBehaviour {

    public static bool startGame;
    public static bool gameOver;
    public static int score;
    public static int distance;

    public float speed = 5f; //speed of the player,camera
    public float spawnRate = .8f;
    public float fuel;

    public GameObject player;
    public GameObject gameOverText;
    public GameObject gameOverFuel;
    public GameObject oval;
    public GameObject triangle;
    public GameObject square;
    public GameObject retry;
    public GameObject highScore;
    public GameObject newHighScore;
    public GameObject count;
    public GameObject fuelSliderG;
    public GameObject fuelPump;
    public GameObject road;

    public Camera cam;

    public Sprite trianglePlayer;
    public Sprite ovalPlayer;
    public Sprite squarePlayer;

    public Slider fuelSlider;

    public Image fuelFill;

    public Text displayDistance;
    public Text finalScoreT;
    public Text finalScoreF;
    public Text displayHighScore;

    public AudioSource gameover;
    public AudioSource backgroundMusic;

    private int randomPrefabIndex;
    private int fuelMilage;
    private int currentShape=0;
    private int check = 0;
    private int check2 = 0;
    private int check3 = 0;
    private int goc = 0;
    private int hScore;

    private float timeSinceLastDestroyed;
    private float timeSinceLastSpawned;
    private float destroyRate = 10f;

    private float spawnXPosition;
    private float spawnYPosition;

    private Vector2 objectPoolPosition = new Vector2(0f, -6f);

    private PlayerController playerController;

    private BoxCollider2D boxCollider2D;

    private GameObject[] gameObjects;
    private GameObject randomGameObject;

    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public Color color6;

    // Use this for initialization
    void Start () {

        boxCollider2D = GetComponent<BoxCollider2D>();
        playerController = player.GetComponent<PlayerController>();
        cam = GetComponent<Camera>();
        score = 0;
        fuel = 2.5f;
        distance = 0;
        startGame = true;
        gameOver = false;
        gameObjects = new GameObject[3];
        gameObjects[0] = oval;
        gameObjects[1] = triangle;
        gameObjects[2] = square;
        fuelSlider.value = fuel/10;

        if (PlayerPrefs.HasKey("highScore"))
            hScore = PlayerPrefs.GetInt("highScore");

    }

    // Update is called once per frame
    void FixedUpdate () {

        Debug.Log(fuel);

        timeSinceLastSpawned += Time.deltaTime;

        if (fuel <= 0)
            gameOver = true;

       if (gameOver && goc==0)
        {
            fuelPump.SetActive(false);
            road.SetActive(false);
            backgroundMusic.Stop();
            gameover.Play();
            fuelSliderG.SetActive(false);

            if(PlayerPrefs.HasKey("highScore") && check3==1)
            {
                displayHighScore.text = "High Score: " + distance.ToString();
                PlayerPrefs.SetInt("highScore", distance);                
            }
            else if(!PlayerPrefs.HasKey("highScore"))
            {
                displayHighScore.text = "High Score: " + distance.ToString();
                PlayerPrefs.SetInt("highScore", distance);
            }
            else
                displayHighScore.text = "High Score: " + hScore.ToString();

            highScore.SetActive(true);

            boxCollider2D.offset = new Vector2(0, 0);
            if (fuel <= 0)
            {
                gameOverFuel.SetActive(true);                
                finalScoreF.text = displayDistance.text;
            }
            else
            {
                gameOverText.SetActive(true);
                finalScoreT.text = displayDistance.text;
            }

            player.SetActive(false);
            retry.SetActive(true);
            displayDistance.text = "";

            goc++;
        }

        if (startGame && !gameOver && fuel > 0)
        {
            fuelSlider.value = fuel/10;

            if (fuel < 2.5)
            {
                fuelFill.color = color4;
            }
            else if (fuel < 5)
                fuelFill.color = color5;
            else
                fuelFill.color = color6;

            if (fuel > 10)
                fuel = 10;

            transform.Translate(Vector2.up * Time.deltaTime * speed);
            count.transform.Translate(Vector2.up * Time.deltaTime * speed);
            distance = (int)transform.position.y;
            fuelMilage = (int)count.transform.position.y;

            //Spawing shapes randomly after the spawnRate expires
            if (timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0;
                spawnXPosition = Random.Range(-2.2f, 2.2f);
                spawnYPosition = player.transform.position.y + 7f;

                randomPrefabIndex = Random.Range(0, 3);
                randomGameObject = Instantiate(gameObjects[randomPrefabIndex], objectPoolPosition, Quaternion.identity);
                randomGameObject.transform.position = new Vector2(spawnXPosition, spawnYPosition);


            }

            //Checks score every 10's multiple. Changes shapes and background color randomly
            if(score%10==0 && score!=0 && check2<1)
            {
                speed = speed + 1f;
                playerController.swipeSpeed = playerController.swipeSpeed + 1f;
                Time.timeScale = 0.1f;
                check2++;
                if(spawnRate>=.2f)
                    spawnRate -= .1f;

                if (player.tag == "Triangle")
                {
                    int temp = Random.Range(0, 2);
                    if (temp == 0)
                    {
                        player.GetComponent<SpriteRenderer>().sprite = ovalPlayer;
                        player.tag = "Oval";
                        cam.backgroundColor = color2;
                    }
                    else
                    {
                        player.GetComponent<SpriteRenderer>().sprite = squarePlayer;
                        player.tag = "Square";
                        cam.backgroundColor = color3;
                    }
                }
                else if (player.tag == "Oval")
                {
                    int temp = Random.Range(0, 2);
                    if (temp == 0)
                    {
                        player.GetComponent<SpriteRenderer>().sprite = squarePlayer;
                        player.tag = "Square";
                        cam.backgroundColor = color3;
                    }
                    else
                    {
                        player.GetComponent<SpriteRenderer>().sprite = trianglePlayer;
                        player.tag = "Triangle";
                        cam.backgroundColor = color1;
                    }
                }
                else
                {
                    int temp = Random.Range(0, 2);
                    if (temp == 0)
                    {
                        player.GetComponent<SpriteRenderer>().sprite = ovalPlayer;
                        player.tag = "Oval";
                        cam.backgroundColor = color2;
                    }
                    else
                    {
                        player.GetComponent<SpriteRenderer>().sprite = trianglePlayer;
                        player.tag = "Triangle";
                        cam.backgroundColor = color1;
                    }
                }
            }
            else
            {
                Time.timeScale = 1;
            }
          
            if(score%10!=0)
                check2 = 0;

            //Check every 15 units of distance covered, reduces fuel by 1
            if(fuelMilage%15==0 && fuelMilage!=0 && check<1)
            {
                fuel -= 0.5f;
                check++;
            }

            if(fuelMilage%15!=0)
                check = 0;

            if(PlayerPrefs.HasKey("highScore") && distance > hScore && check3<1)
            {
                StartCoroutine(displayNewHigScore());
                check3++;
            }
            
            displayDistance.text = distance.ToString();

        }
		
	}

    private IEnumerator displayNewHigScore()
    {
        newHighScore.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        newHighScore.SetActive(false);

    }

    public void Retry()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}

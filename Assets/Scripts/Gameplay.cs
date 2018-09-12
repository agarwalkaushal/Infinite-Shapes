using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;

public class Gameplay : MonoBehaviour {

    public static bool startGame;
    public static bool gameOver;
    public static int score;
    public static int fuel;
    public static int distance;

    public float speed = 4f; //speed of the player,camera

    public GameObject player;
    public GameObject gameOverText;
    public GameObject oval;
    public GameObject triangle;
    public GameObject square;
    public GameObject retry;

    public Sprite trianglePlayer;
    public Sprite ovalPlayer;
    public Sprite squarePlayer;

    public Text displayFuel;
    public Text displayDistance;

    private int randomPrefabIndex;
    private int currentShape=0;
    private int check = 0;
    private int check2 = 0;

    private float timeSinceLastDestroyed;
    private float timeSinceLastSpawned;
    private float destroyRate = 10f;
    private float spawnRate = .8f;
    private float spawnXPosition;
    private float spawnYPosition;

    private Vector2 objectPoolPosition = new Vector2(0f, -6f);

    private PlayerController playerController;

    private GameObject[] gameObjects;
    private GameObject randomGameObject;





    // Use this for initialization
    void Start () {

        playerController = player.GetComponent<PlayerController>();
        score = 0;
        fuel = 5;
        distance = 0;
        startGame = true;
        gameOver = false;
        gameObjects = new GameObject[3];
        gameObjects[0] = oval;
        gameObjects[1] = triangle;
        gameObjects[2] = square;
        

    }

    // Update is called once per frame
    void Update () {

        timeSinceLastSpawned += Time.deltaTime;

        if (fuel <= 0)
            gameOver = true;

        if(gameOver)
        {
            gameOverText.SetActive(true);
            player.SetActive(false);
            retry.SetActive(true);
        }

        if (startGame && !gameOver && fuel > 0)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
            distance = (int)transform.position.y;
            
            if (timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0;
                spawnXPosition = Random.Range(-2.2f, 2.2f);
                spawnYPosition = player.transform.position.y + 7f;

                randomPrefabIndex = Random.Range(0, 3);
                randomGameObject = Instantiate(gameObjects[randomPrefabIndex], objectPoolPosition, Quaternion.identity);
                randomGameObject.transform.position = new Vector2(spawnXPosition, spawnYPosition);


            }

            if(score%10==0 && score!=0 && check2<1)
            {
                speed = speed + 1.25f;
                playerController.swipeSpeed = playerController.swipeSpeed + 1f;
                check2++;
                if(spawnRate<=.2f)
                    spawnRate -= .2f;

                if (player.tag == "Triangle")
                {
                    int temp = Random.Range(0, 2);
                    if (temp == 0)
                    {
                        player.GetComponent<SpriteRenderer>().sprite = ovalPlayer;
                        player.tag = "Oval";
                    }
                    else
                    {
                        player.GetComponent<SpriteRenderer>().sprite = squarePlayer;
                        player.tag = "Square";
                    }
                }
                else if (player.tag == "Oval")
                {
                    int temp = Random.Range(0, 2);
                    if (temp == 0)
                    {
                        player.GetComponent<SpriteRenderer>().sprite = squarePlayer;
                        player.tag = "Square";
                    }
                    else
                    {
                        player.GetComponent<SpriteRenderer>().sprite = trianglePlayer;
                        player.tag = "Triangle";
                    }
                }
                else
                {
                    int temp = Random.Range(0, 2);
                    if (temp == 0)
                    {
                        player.GetComponent<SpriteRenderer>().sprite = ovalPlayer;
                        player.tag = "Oval";
                    }
                    else
                    {
                        player.GetComponent<SpriteRenderer>().sprite = trianglePlayer;
                        player.tag = "Triangle";
                    }
                }
            }

            if(score%10!=0)
            {
                check2 = 0;

            }

            if(distance%10==0 && distance!=0 && check<1)
            {
                fuel -= 1;
                check++;
            }

            if(distance%10!=0)
            {
                check = 0;
            }

            displayFuel.text = "Fuel: " + fuel.ToString();
            displayDistance.text = distance.ToString();

        }
		
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

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

    public float speed = 3.5f; //speed of the player,camera

    private int randomPrefabIndex;
    private int currentShape=0;
    private int check = 0;
    private int check2 = 0;

    private float timeSinceLastDestroyed;
    private float timeSinceLastSpawned;
    private float destroyRate = 10f;
    private float spawnRate = 1f;
    private float spawnXPosition;
    private float spawnYPosition;

    private Vector2 objectPoolPosition = new Vector2(0f, -6f);

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

    private GameObject[] gameObjects;
    private GameObject[] randomGameObjects;
    private Rigidbody2D[] rbs;

    // Use this for initialization
    void Start () {

        score = 0;
        fuel = 0;
        distance = 0;
        startGame = true;
        gameOver = false;
        gameObjects = new GameObject[3];
        randomGameObjects = new GameObject[6];
        rbs = new Rigidbody2D[6];
        gameObjects[0] = oval;
        gameObjects[1] = triangle;
        gameObjects[2] = square;

        SpawnRandom();
        

    }

    // Update is called once per frame
    void Update () {

        timeSinceLastSpawned += Time.deltaTime;

        if(gameOver)
        {
            gameOverText.SetActive(true);
            player.SetActive(false);
            retry.SetActive(true);
        }

        if (startGame && !gameOver)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
            distance = (int)transform.position.y;

            //loop through all instantiated prefabs
            for(int i = 0; i < 6; i++)
            {                
                //Rotate instantiated prefabs
                randomGameObjects[i].transform.RotateAround(randomGameObjects[i].transform.position, Vector3.back, 90 * Time.deltaTime);
            }
            
            if (timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0;
                spawnXPosition = Random.Range(-2.2f, 2.2f);
                spawnYPosition = player.transform.position.y + 7f;
                
                randomGameObjects[currentShape++].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                
                if(currentShape>5)
                {
                    SpawnRandom();
                    currentShape = 0;
                }

            }

            if(score%10==0 && score!=0 && check2<1)
            {
                speed = speed + 1.5f;
                check2++;

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

            if(distance%50==0 && distance!=0 && check<1)
            {
                fuel -= 2;
                check++;
                spawnRate -= .1f;
            }

            if(distance%50!=0)
            {
                check = 0;
            }

            Debug.Log(fuel);

            displayFuel.text = "Fuel: " + fuel.ToString();
            displayDistance.text = distance.ToString();

        }
		
	}

    /*
    void DestroyAll()
    {
        for (int i = 0; i < 6; i++)
        {
            Destroy(randomGameObjects[i]);
        }
    }
    */

    void SpawnRandom()
    {
        for (int i = 0; i < 6; i++)
        {
            randomPrefabIndex = Random.Range(0, 3);
            randomGameObjects[i] = Instantiate(gameObjects[randomPrefabIndex], objectPoolPosition, Quaternion.identity);
            rbs[i] = randomGameObjects[i].GetComponent<Rigidbody2D>();
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

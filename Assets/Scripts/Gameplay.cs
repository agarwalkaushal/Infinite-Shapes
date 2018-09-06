using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour {

    public static bool startGame;
    public static bool gameOver;
    public static int score;

    public float speed = 2.5f; //speed of the player,camera

    private int randomPrefabIndex;
    private int currentShape=0;

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

    public Text displayScore;

    private GameObject[] gameObjects;
    private GameObject[] randomGameObjects;
    private Rigidbody2D[] rbs;

    // Use this for initialization
    void Start () {

        score = 0;
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

            //loop through all instantiated prefabs
            for(int i = 0; i < 6; i++)
            {                
                //Rotate instantiated prefabs
                randomGameObjects[i].transform.RotateAround(randomGameObjects[i].transform.position, Vector3.back, 60 * Time.deltaTime);

                //Add velocity in x-axis
                int rVelocity = Random.Range(0, 2);                
                if (rVelocity == 0)
                    rbs[i].velocity = new Vector2(1f, 0f);
                else
                    rbs[i].velocity = new Vector2(-1f, 0f);

                //Clamp in x-axis                
                rbs[i].position = new Vector2(Mathf.Clamp(rbs[i].position.x, -3f, 3f), rbs[i].position.y);
            }
            
            if (timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0;
                spawnXPosition = Random.Range(-2f, 2f);
                spawnYPosition = player.transform.position.y + 7.5f;
                
                randomGameObjects[currentShape++].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                
                if(currentShape>5)
                {
                    SpawnRandom();
                    currentShape = 0;
                }

            }
        }

        displayScore.text = score.ToString();
		
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

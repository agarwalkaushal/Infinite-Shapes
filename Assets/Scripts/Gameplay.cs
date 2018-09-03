using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour {

    public static bool startGame = false;
    public static bool gameOver = false;

    public float speed = 2.5f; //speed of the player,camera

    private int randomPrefabIndex;
    private int currentShape=0;

    private float timeSinceLastDestroyed;
    private float timeSinceLastSpawned;
    private float destroyRate = 10f;
    private float spawnRate = 1.5f;
    private float spawnXPosition;
    private float spawnYPosition;

    private Vector2 objectPoolPosition = new Vector2(0f, -6f);

    public GameObject player;
    public GameObject tap;
    public GameObject oval;
    public GameObject triangle;
    public GameObject square;

    private GameObject[] gameObjects;
    private GameObject[] randomGameObjects;
    private Rigidbody2D[] rbs;

    // Use this for initialization
    void Start () {

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

        if (startGame)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
            for(int i = 0; i < 6; i++)
            {
                randomGameObjects[i].transform.RotateAround(randomGameObjects[i].transform.position, Vector3.back, 60 * Time.deltaTime);
                int rVelocity = Random.Range(0, 2);

                
                if (rVelocity == 0)
                    rbs[i].velocity = new Vector2(1f, 0f);
                else
                    rbs[i].velocity = new Vector2(-1f, 0f);
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

            //Clamp the random gameobjects in the x axis

            /*Vector2 clampedPosition = randomGameObjects[i].transform.position;
            clampedPosition.x = Mathf.Clamp(randomGameObjects[i].transform.position.x, -3f, 3f);
            randomGameObjects[i].transform.position = clampedPosition;*/

            //get the rigidbody components of all the gameobjects to assign force to them in the Update method
            rbs[i] = randomGameObjects[i].GetComponent<Rigidbody2D>();

            randomGameObjects[i].transform.position = new Vector3(Mathf.Clamp(Time.time, -3.0F, 3.0F), -6, 0);
        }
    }

    public void tapToBegin()
    {
        startGame = true;
        tap.SetActive(false);

    }
}

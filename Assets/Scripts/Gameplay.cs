using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour {

    public static bool startGame = false;
    public static bool gameOver = false;

    private int randomPrefabIndex;
    private int currentShape=0;

    private float timeSinceLastSpawned;
    private float spawnRate = 2f;
    private float spawnXPosition;
    private float spawnYPosition;

    private Vector2 objectPoolPosition = new Vector2(-5f, 0f);

    public GameObject player;
    public GameObject tap;
    public GameObject oval;
    public GameObject triangle;
    public GameObject square;

    private GameObject[] gameObjects;
    private GameObject[] randomGameObjects;

    // Use this for initialization
    void Start () {

        gameObjects = new GameObject[3];
        randomGameObjects = new GameObject[6];
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
            transform.Translate(Vector2.up * Time.deltaTime * 2.5f);
            if(timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0;
                spawnXPosition = Random.Range(-2f, 2f);
                spawnYPosition = player.transform.position.y + 5f;
                randomGameObjects[currentShape].transform.Rotate(Vector3.back, Time.deltaTime);
                randomGameObjects[currentShape++].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                
                if(currentShape>5)
                {
                    SpawnRandom();
                    currentShape = 0;
                }

            }
        }

		
	}

    void SpawnRandom()
    {
        for (int i = 0; i < 6; i++)
        {
            randomPrefabIndex = Random.Range(0, 3);
            randomGameObjects[i] = Instantiate(gameObjects[randomPrefabIndex], objectPoolPosition, Quaternion.identity);
        }
    }

    public void tapToBegin()
    {
        startGame = true;
        tap.SetActive(false);

    }
}

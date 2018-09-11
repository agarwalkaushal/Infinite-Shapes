using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private int c = 0;

    private Gameplay gameplay;

    // Use this for initialization
    void Start()
    {
        Camera camera = Camera.main;
        gameplay = camera.GetComponent<Gameplay>();
    }

    void FixedUpdate()
    {

        // initially, the temporary vector should equal the player's position
        Vector2 clampedPosition = transform.position;
        // Now we can manipulte it to clamp the y element
        clampedPosition.x = Mathf.Clamp(transform.position.x, -2.25f,2.25f);
        // re-assigning the transform's position will clamp it
        transform.position = clampedPosition;

        if (Gameplay.startGame && !Gameplay.gameOver)
        {
            //Keep the player moving in the upward direction
            transform.Translate(Vector2.up * Time.deltaTime * gameplay.speed);

            //Checks for the keypress A, if true then translate in left direction
            if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector2.left * Time.deltaTime * 10);

            //Checks for the keypress D, if true then translate in right direction
            if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector2.right * Time.deltaTime * 10);

            transform.Translate(Input.acceleration.x * Time.deltaTime * 11.5f,0,0);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != gameplay.player.tag)
            Gameplay.gameOver = true;
        else
        {
            other.gameObject.SetActive(false);
            Gameplay.score++;
            Gameplay.fuel++;
            Debug.Log(Gameplay.score);
        }
    }
}

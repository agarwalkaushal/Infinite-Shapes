using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 2.5f; //speed of the player,camera
    private int c = 0;

    // Use this for initialization
    void Start()
    {
        
    }

    void FixedUpdate()
    {

        // initially, the temporary vector should equal the player's position
        Vector2 clampedPosition = transform.position;
        // Now we can manipulte it to clamp the y element
        clampedPosition.x = Mathf.Clamp(transform.position.x, -2f,2f);
        // re-assigning the transform's position will clamp it
        transform.position = clampedPosition;

        if (Gameplay.startGame)
        {
            //Keep the player moving in the upward direction
            transform.Translate(Vector2.up * Time.deltaTime * speed);

            //Checks for the keypress A, if true then translate in left direction
            if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector2.left * Time.deltaTime * 9);

            //Checks for the keypress D, if true then translate in right direction
            if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector2.right * Time.deltaTime * 9);

            transform.Translate(Input.acceleration.x * Time.deltaTime * 9,0,0);
        }

    }
}

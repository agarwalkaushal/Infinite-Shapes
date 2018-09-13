using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Camera camera;

    public float swipeSpeed = 4f;

    private int c = 0;
    private int horizontal;

    private bool canInvoke = true;

    private Vector2 touchOrigin;

    private Gameplay gameplay;

    private Rigidbody2D rbd;

    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
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

            //Swiping to move player
            if(Input.touchCount > 0 && Time.timeScale > 0.0f)
            {
                Touch myTouch = Input.touches[0];

                if (myTouch.phase == TouchPhase.Began)
                {
                    touchOrigin = myTouch.position;
                }
                else if (myTouch.phase != TouchPhase.Ended && myTouch.phase != TouchPhase.Canceled)
                {
                    float x = myTouch.position.x - touchOrigin.x;
                    Vector2 direction = myTouch.position - touchOrigin;
                    if (Mathf.Abs(x) > 1f)
                    {
                        if (Mathf.Sign(direction.x) > 0)
                        {
                            horizontal = 1;
                            rbd.velocity = new Vector2(horizontal * swipeSpeed, 0f);
                        }
                        else if (Mathf.Sign(direction.x) < 0)
                        {
                            horizontal = -1;
                            rbd.velocity = new Vector2(horizontal * swipeSpeed, 0f);
                        }
                        else
                            horizontal = 0;
                    }
                        
                }
                else if (myTouch.phase == TouchPhase.Ended)
                {
                    touchOrigin.x = -1;
                    rbd.velocity = Vector2.zero;
                }
            }

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
        }
    }
}

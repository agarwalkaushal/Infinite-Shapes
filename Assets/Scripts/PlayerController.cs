using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Camera camera;

    public float swipeSpeed;

    private float x;
    private float smoothness = 0.3f;

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


            //Using touch.deltaposition 

            if(Input.touchCount > 0)
            {
                Touch myTouch = Input.GetTouch(0);
                rbd.velocity = Vector2.zero;

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    x = myTouch.deltaPosition.x;

                    rbd.velocity = new Vector2(smoothness*x,0);
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

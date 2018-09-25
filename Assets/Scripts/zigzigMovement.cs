using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zigzigMovement : MonoBehaviour {

    private int randomForceValue;

    private Rigidbody2D rigidbody2D;

    private float randomForceAddRate = .5f;
    private float timeSinceForceAdded;

    void Start ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {

        timeSinceForceAdded += Time.deltaTime;

        if (timeSinceForceAdded >= randomForceAddRate)
        {
            timeSinceForceAdded = 0;
            randomForce();
        }
    }

    private void randomForce()
    {
        randomForceValue = Random.Range(-1, 2);
        rigidbody2D.AddForce(new Vector2(randomForceValue*50, 0));
    }
}

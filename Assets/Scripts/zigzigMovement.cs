using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zigzigMovement : MonoBehaviour {

    private int randomForceValue;
    private Rigidbody2D rigidbody2D;


    void Start ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        randomForceValue = Random.Range(-5, 6);
        StartCoroutine(randomForce());        
    }

    private IEnumerator randomForce()
    {        
        yield return new WaitForSeconds(1f);
        rigidbody2D.AddForce(new Vector2(randomForceValue, 0));
    }
}

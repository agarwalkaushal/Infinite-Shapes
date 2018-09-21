using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectMisses : MonoBehaviour {

    public Camera camera;

    private Gameplay gameplay;

    private string playerTag;

    void Start () {

        gameplay = camera.GetComponent<Gameplay>();
    }
	
	void FixedUpdate () {

        playerTag = gameplay.player.tag;

        transform.Translate(Vector2.up * Time.deltaTime * gameplay.speed);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerTag == other.tag)
        {
            Gameplay.fuel--;
            gameplay.count.transform.position = new Vector3(0, 0, 0);
        }
    }

}

using UnityEngine;

public class Rotation : MonoBehaviour {

	void Update () {

        transform.RotateAround(transform.position, Vector3.back, 90 * Time.deltaTime);

    }
}

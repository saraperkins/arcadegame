using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {
    public float speed;

	void Update () {
        transform.Rotate(new Vector3 (0, 90, 0) * speed);
	}
}

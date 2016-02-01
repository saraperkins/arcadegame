using UnityEngine;
using System.Collections;

public class ShotMover : MonoBehaviour {

    public float speed;
	// Use this for initialization
	void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
	}
}

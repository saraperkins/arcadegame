using UnityEngine;
using System.Collections;

public class ShotMover : MonoBehaviour {

    public float speed;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
	}
    
    void reverseVelocity()
    {
        speed = -speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ShotReflector")
        {
            reverseVelocity();
            rb.velocity = transform.forward * speed;
        }
    }
}

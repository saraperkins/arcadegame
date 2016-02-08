using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	public bool forward;
    //public int direction;

	void Start () {
		rb = GetComponent<Rigidbody> ();
        /*
		if (forward) {
			rb.velocity = transform.forward * speed;
		} else {
			rb.velocity = transform.right * speed;
		}
        */
	}

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + new Vector3(speed, 0, 0));
    }
}
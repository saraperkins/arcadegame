using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	public bool forward;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		if (forward) {
			rb.velocity = transform.forward * speed;
		} else {
			rb.velocity = transform.right * speed;
		}
	}
}
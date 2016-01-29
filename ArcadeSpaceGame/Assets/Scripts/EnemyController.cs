using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject player;
	public float speed;
	public float catchup;
	public float pushBack;
	public GameObject explosion;

	private bool isHit;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

	}

	void Update () {
		if (isHit) {
			transform.position -= new Vector3(pushBack,0,0);
			isHit = false;
		}
	}

	// Update is called once per frame
	void LateUpdate () {

		float offset = transform.position.z - player.transform.position.z;

		Vector3 mov = Vector3.zero;

		if (offset > .5) {
			mov = Vector3.back;
		} else if (offset < -.5) {
			mov = Vector3.forward;
		}

		mov = (mov * speed) + new Vector3(catchup,0,0);

		rb.AddForce (mov);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary"){
			return;
		}

		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (other.gameObject);
        isHit = true;

		//gameObject.transform.position.x -= pushback;
		//gameObject.transform.position.x = transform.position * Vector3.left * pushback;
		//Destroy (gameObject);

		//if (other.tag == "Player") {
			//gameController.GameOver ();
		//}
	}
}

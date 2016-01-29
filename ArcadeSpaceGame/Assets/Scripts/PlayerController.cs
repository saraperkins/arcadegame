using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float pushBack;
	private bool isHit;

	private Rigidbody rb;
	private AudioSource audioSource;

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	private GameController gameController;
	public GameObject explosion;
    private float rotation;
    public float rotOffset;
    private int spin;

    private bool canShoot = true;
    public float gunTimeout;
    private float gunRestart;


	void Start () {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource> ();
        spin = 0;

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void Update () {
        if (Time.time >= gunRestart) { canShoot = true; }
        if (canShoot && Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}
		/*if (isHit) {
			transform.position -= new Vector3(pushBack,0,0);
			isHit = false;
		}//*/
	}

	void FixedUpdate () {
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (/*moveHorizontal*/ 0.0f, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3 
			(
				Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);
        if(spin > 0)
        {
            if(rotation < 360)
            {
                rotation += rotOffset;
            }
            else
            {
                spin -= 1;
                rotation = 0;
                gameController.UpdateSpins();
            }
        }
		rb.rotation = Quaternion.Euler (0.0f, 90f + rotation, rb.velocity.x * -tilt);
	}


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary"){
			return;
		}

		//Instantiate (explosion, transform.position, transform.rotation);
		Destroy (other.gameObject);
		//isHit = true;

		//gameObject.transform.position.x -= pushback;
		//gameObject.transform.position.x = transform.position * Vector3.left * pushback;
		//Destroy (gameObject);

		if (this.tag == "Player" && other.tag == "Enemy") {
			gameController.GameOver ();
		}

        if (this.tag == "Player" && other.tag == "gunDisabler")
        {
            if (canShoot)
            {
                gunRestart = Time.time + gunTimeout;
                canShoot = false;
            }
            else
            {
                gunRestart += gunTimeout;
            }
        }

        else if(other.tag == "spin")
        {
            spin += 1;
            gameController.UpdateSpins();
        }
        else
        {
            transform.position -= new Vector3(pushBack, 0, 0);
        }
	}

    public int GetSpins() { return spin; }
}
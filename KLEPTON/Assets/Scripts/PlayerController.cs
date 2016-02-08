using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float pushBack;
	private bool hitByLBall;
    private float LBallTime = 0;

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

    private int toPush;
    private bool shieldActive;

    private bool nux = false;


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

        shieldActive = false;
	}

	void Update () {
        if (Time.timeSinceLevelLoad >= gunRestart) { canShoot = true; }
        if (canShoot && Input.GetButton("Fire1") && Time.timeSinceLevelLoad > nextFire)
        {
            nextFire = Time.timeSinceLevelLoad + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}

        if(Input.GetKeyDown(KeyCode.N))
        {
            nux = !nux;
            gameController.UpdateSpins(nux);
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

        if (LBallTime > 0)
        {
            movement = -1 * movement;
            LBallTime -= Time.deltaTime;
        }
        else
        {
            LBallTime = 0;
        }

        rb.velocity = movement * speed;

        if (toPush > 0)
        {
            toPush--;
            transform.position += new Vector3(0.1f, 0, 0);
        }

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
                //gameController.UpdateSpins();
            }
        }
		rb.rotation = Quaternion.Euler (0.0f, 90f + rotation, rb.velocity.x * -tilt);
	}


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary"){
			return;
		}

        if (other.tag == "Bolt")
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.tag != "Indes")
        {
            Destroy(other.gameObject);
        }
        //isHit = true;

        //gameObject.transform.position.x -= pushback;
        //gameObject.transform.position.x = transform.position * Vector3.left * pushback;
        //Destroy (gameObject);

        if (other.tag == "Enemy") {
			gameController.GameOver ();
		}

        else if (other.tag == "LBall")
        {
            hitByLBall = true;
            LBallTime += 5;
        }

        else if (other.tag == "gunDisabler")
        {
            if (canShoot)
            {
                gunRestart = Time.timeSinceLevelLoad + gunTimeout;
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
            //gameController.UpdateSpins();
        }
        else if(other.tag == "Gas")
        {
            //transform.position += new Vector3(pushBack, 0, 0);
            toPush += (int)(10 * pushBack);
        }
        else if (other.tag == "Shield")
        {
            this.transform.GetChild(2).gameObject.SetActive(true);
            shieldActive = true;
        }
        else if (shieldActive && (other.tag != "Shield" && other.tag != "Gas" && other.tag != "spin"))
        {
            this.transform.GetChild(2).gameObject.SetActive(false);
            shieldActive = false;
        }
        else if (!nux)
        {
            transform.position -= new Vector3(pushBack, 0, 0);
        }
	}

    public int GetSpins() { return spin; }
}
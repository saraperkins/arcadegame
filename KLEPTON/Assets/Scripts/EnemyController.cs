using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject player;
	public float speed;
	public float catchup;
	public float pushBack;
	public GameObject explosion;
    public Boundary boundary;

	//pick up effects, stopping and spinning
    private float stopTime = 0;
    private int spin = 0;
    private float rotation = 0;
    private float rotOffset = 2;
    private bool shieldActive;

    //debug mode
    private bool debug = false;

    //how slow it makes the cop
    //and how long the effect lasts
    public float slowDown, effectTime;

    //shooting
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float fasterFireRate;
    public float firstShot;
    private float nextFire;
    private float regFireRestart;
    private bool canShoot = true;
    private float gunRestart;
    public float gunTimeout;

    public int gameStart;

	private Rigidbody rb;
    private bool nux = false;
    private int nuxoll = 1;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

        //initialize sheildActive to false
        shieldActive = false;

        //firstShot takes longer
        nextFire = firstShot;
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.N))
        {
            nux = !nux;
            nuxoll =  1 - nuxoll;
        }
        
        /*/
        if (Input.GetKeyDown(KeyCode.D))
        {
            debug = !debug;
        }
        //*/

        if (Time.timeSinceLevelLoad > gunRestart) { canShoot = true; }
        if (Time.timeSinceLevelLoad > nextFire && canShoot && Time.timeSinceLevelLoad > gameStart + 1)
        {
            if (Time.timeSinceLevelLoad > regFireRestart)
            {
                nextFire = Time.timeSinceLevelLoad + fireRate;

            }

            // currently not working, once activated, it stays activated
            else
            {
                nextFire = Time.timeSinceLevelLoad + fasterFireRate;
            }//*/

            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

        }
	}

	// Update is called once per frame
	void LateUpdate () {

        Vector3 mov = Vector3.zero;

        //debugging allows control of the enemy
        //if (!debug)
        {
            float offsetZ = transform.position.z - player.transform.position.z;

            if (offsetZ > .5)
            {
                mov = Vector3.back;
            }
            else if (offsetZ < -.5)
            {
                mov = Vector3.forward;
            }

        }
        /*/
        else
        {
            float vert = Input.GetAxis("Vertical");
            mov = new Vector3(0.0f, 0.0f, vert);
        }//*/

        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        //pickups cause the enemy to slow its movement for an amount of time
        /*/
        if (stopTime > 0)
        {
            mov = mov * speed / slowDown;
            stopTime -= Time.deltaTime;
        }
        else
        {
            mov = (mov * speed) + new Vector3(catchup * nuxoll, 0, 0);
            stopTime = 0;
        }//*/

        //float offsetX = transform.position.x - player.transform.position.x;
        //if (offsetX > 0 || catchup < 0) { catchup = -catchup; }

        //new
        if (Time.timeSinceLevelLoad > stopTime)
        {
            mov = (mov * speed) + new Vector3(catchup * nuxoll, 0, 0);
        }
        else
        {
            mov = mov * speed / slowDown;
        }

        if (Time.timeSinceLevelLoad > gameStart)
        {
            rb.AddForce(mov);
        }
        
        if (spin > 0)
        {
            if(rotation < 360)
            {
                rotation += rotOffset;
            }
            else
            {
                spin -= 1;
                rotation = 0;
            }
        }
        rb.rotation = Quaternion.Euler(0.0f, 90f + rotation, rb.velocity.x);
        
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary"){
			return;
		}

		//Instantiate (explosion, transform.position, transform.rotation);
        if (other.tag != "Indes")
        {
            Destroy(other.gameObject);
        }

        if(other.tag == "LBall")
        {
            stopTime = Time.timeSinceLevelLoad + effectTime;
            regFireRestart = Time.timeSinceLevelLoad + effectTime * 3;
        }
        else if (other.tag =="spin")
        {
            spin += 1;
            stopTime = Time.timeSinceLevelLoad + effectTime;
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
        else if (other.tag == "Gas")
        {
            transform.position += new Vector3(pushBack, 0, 0);
        }
        else if (other.tag == "Shield" && !shieldActive)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
            shieldActive = true;
        }
        else if ((other.tag != "Shield" || other.tag != "Gas" || other.tag != "spin") && shieldActive)
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
            shieldActive = false;
        }

        else if (!nux) //check nux mode after effect is activated
        {
            transform.position -= new Vector3(pushBack, 0, 0);
        }

        

		//gameObject.transform.position.x -= pushback;
		//gameObject.transform.position.x = transform.position * Vector3.left * pushback;
		//Destroy (gameObject);

		//if (other.tag == "Player") {
			//gameController.GameOver ();
		//}
	}
}

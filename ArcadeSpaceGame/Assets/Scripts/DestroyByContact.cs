using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
	public GameObject foreignShipExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}


	//explosions disabled
	void OnTriggerEnter(Collider other) {
        if (other.tag == "Boundary")
        {
            return;
        }

        if (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Bolt")
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);
        }

        if (other.tag == "Bolt") {
		    Destroy(other.gameObject);
            if (tag != "Indes")
            {
                Destroy(gameObject);
            }
	    }
    }

}
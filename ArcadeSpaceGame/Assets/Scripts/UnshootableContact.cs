using UnityEngine;
using System.Collections;

public class UnshootableContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject foreignShipExplosion;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }


    //explosions disabled
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            //Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            //gameController.GameOver ();
        }
        else {
            if (other.tag == "Boundary")
            {
                return;
            }

            //Instantiate(explosion, transform.position, transform.rotation);


            if (other.tag == "Enemy")
            {
                Destroy(gameObject);
                //Instantiate(foreignShipExplosion, other.transform.position, other.transform.rotation);

            }
            else {
                //gameController.AddScore (scoreValue);
            }
        }

        
        if (other.tag == "Bolt")
        {
            Destroy(other.gameObject);
        }
    }

}
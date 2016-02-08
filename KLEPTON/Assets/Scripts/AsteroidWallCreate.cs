using UnityEngine;
using System.Collections;

public class AsteroidWallCreate : MonoBehaviour {

    public GameObject indestructable;
    public GameObject destructable;
    public int height;
    public int topZ;
    public float spacing;

	// Use this for initialization
	void Start () {
        Vector3 wallPos = transform.position;
        int numObj = (int)((height / spacing) +.5);
        int breakNum = (int)Random.Range(1, numObj - 1 - .01f);

        for(int i = 0; i < numObj; ++i)
        {
            if(i == breakNum)
            {
                Instantiate(destructable, new Vector3(transform.position.x, transform.position.y, topZ - spacing * i), Quaternion.identity);
            }
            else
            {
                Instantiate(indestructable, new Vector3(transform.position.x, transform.position.y, topZ - spacing * i), Quaternion.identity);
            }
        }
	}
}

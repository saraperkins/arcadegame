using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	private Rigidbody rb;
	public float tumble;
    private float rand1;
    private float rand2;
    private float rand3;

	void Start () {
		//rb = GetComponent<Rigidbody> ();
        //rb.angularVelocity = Random.insideUnitSphere * tumble;
        rand1 = Random.Range(-1, 1);
        rand2 = Random.Range(-1, 1);
        rand3 = Random.Range(-1, 1);
	}

    void Update()
    {
        transform.Rotate(new Vector3(rand1, rand2, rand3) * tumble);
    }
}

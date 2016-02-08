using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public bool vertical;
    public float endWait;

	void Start () {
		
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves () {
       
            yield return new WaitForSeconds (startWait);
            while (Time.timeSinceLevelLoad < endWait)
            {
			    for (int i = 0; i < hazardCount; i++) {
				    Vector3 spawnPosition;
				    if (vertical) {
					    spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				    } else {
					    spawnPosition = new Vector3 (spawnValues.x, spawnValues.y, Random.Range (-spawnValues.z, spawnValues.z));
				    }
				    Quaternion spawnRotation = Quaternion.identity;
				    Instantiate (hazard, spawnPosition, spawnRotation);
				    yield return new WaitForSeconds (spawnWait);
			    }
			    yield return new WaitForSeconds (waveWait);

		    }
        
	}

}
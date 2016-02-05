using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnV2 : MonoBehaviour {

    public int spawnX;
    public int spawnZ;

    [System.Serializable]
    public struct SpawnData
    {
        public GameObject enemy;

        //times in seconds from game start
        public int startTime;
        public int midStart;
        public int midEnd;
        public int endTime;

        //1 in spawnMult chance to spawn each second
        //less than one increases rate further
        public float spawnMult;
    }

    public SpawnData[] spawns;

    [System.Serializable]
    public struct SuspendData
    {
        public int startTime;
        public int endTime;
    }

    public SuspendData[] suspends;

    private bool suspended;



    // Use this for initialization
    void Start () {
        foreach (SpawnData data in spawns)
        {
            StartCoroutine(Spawner(data.enemy, data.startTime, data.midStart, data.midEnd, data.endTime, data.spawnMult, this));
        }
        suspended = false;
    }

    public bool SpawnSuspended()
    {
        return suspended;
    }

    public void Update()
    {
        suspended = CheckIfSuspended();
    }

    private bool CheckIfSuspended()
    {
        foreach(SuspendData data in suspends)
        {
            if (Time.time > data.startTime && Time.time < data.endTime)
            {
                return true;
            }
        }
        return false;
    }

    //controls the spawning of passed in prefabs
    IEnumerator Spawner(GameObject enemy, int start, int midStart, int midEnd, int end, float mult, SpawnV2 hoster)
    {
        bool continueSpawning = true;

        if (start < 0 || midStart < 0)//reject improper values
        {
            continueSpawning = false;
        }

        if (mult < .04f)
        {
            mult = .04f;
        }

        while (continueSpawning)
        {
            //do nothing if before spawn start time
            if(Time.time <= start || hoster.SpawnSuspended())
            {
                yield return new WaitForSeconds(1);
                continue;
            }

            //end script if past spawn end time
            else if (Time.time >= end && end != -1)
            {
                continueSpawning = false;
                continue;
            }

            float effectiveMult;
            int numToSpawn;

            //skew multiplier downwards if in buildup or dropoff phases
            if ((Time.time >= midStart && Time.time <= midEnd) || midEnd == -1)
            {
                effectiveMult = mult;//if in middle, default
            }
            else if (Time.time > midEnd)
            {
                if (end == -1)//endless dropoff
                {
                    if (midEnd == start)//dropoff scales from start
                    {
                        effectiveMult = (mult * Mathf.Sqrt(Time.time - start));
                    }
                    else//dropoff scales based on time zone
                    {
                        effectiveMult = (mult * (Time.time - start)) / (midEnd - start);
                    }
                }
                else
                {
                    effectiveMult = (mult * (end - midEnd)) / (end - Time.time);
                }
            }
            else
            {
                effectiveMult = (mult * (midStart - start)) / (Time.time - start);
            }

            //calculate how many objects to spawn this second
            if (effectiveMult > 1)
            {
                if(Random.Range(0, effectiveMult) < 1)
                {
                    numToSpawn = 1;
                }
                else
                {
                    numToSpawn = 0;
                }
            }
            else
            {
                numToSpawn = (int)(1.0f / effectiveMult);
            }

            //spawn the objects
            for(int i = 0; i < numToSpawn; ++i)
            {
                Instantiate(enemy, new Vector3(hoster.spawnX + Random.Range(0, 8) , 0.0f, Random.Range(-hoster.spawnZ, hoster.spawnZ)), Quaternion.identity);
                yield return new WaitForSeconds(.04f);
            }
            yield return new WaitForSeconds(1 - .04f * numToSpawn);
        }
    }
}

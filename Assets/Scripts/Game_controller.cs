using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_controller : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float hazardWait;
    public float startWait;
    public float waveWait;

    void Start()
    {
        StartCoroutine( SpawnWaves());
       
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnposition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnrot = Quaternion.identity;
                Instantiate(hazard, spawnposition, spawnrot);
                yield return new WaitForSeconds(hazardWait);
            }

            yield return new WaitForSeconds(waveWait);
        }

        
        
    }
	
}

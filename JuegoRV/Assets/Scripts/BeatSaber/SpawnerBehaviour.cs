using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    public Transform spawnerRojo;
    public Transform spawnerAzul;
    public GameObject beatCube;
    private bool allowSpawn = false;
    public List<SpawningInfo> spawningQueue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawningQueue.Count>0)
        if ((int)Time.timeSinceLevelLoad == spawningQueue[0].timeToSpawn)
        {
            if (allowSpawn)
            {
                allowSpawn = false;
                Debug.Log("Attemp spawn");
                GameObject spawnedCube;
                if (spawningQueue[0].spawnSide=="Red")
                {
                    spawnedCube = Instantiate(beatCube, spawnerRojo);
                    spawnedCube.GetComponent<BeatCubeBehaviour>().color = Color.red;
                }
                else
                {
                    spawnedCube = Instantiate(beatCube, spawnerAzul);
                    spawnedCube.GetComponent<BeatCubeBehaviour>().color = Color.blue;
                }
                spawnedCube.transform.Rotate(0,0,45*spawningQueue[0].rotationSegment);
                spawningQueue.RemoveAt(0);
            }
        }
        else 
        {
            allowSpawn = true;
        }
    }
}

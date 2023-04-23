using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

public class SpawnerBehaviour : MonoBehaviour
{
    public Transform spawnerRojo;
    public Transform spawnerAzul;
    public GameObject beatCube;
    public List<SpawningInfo> spawningQueue;
    private ObjectPool cubePool;
    string selectedSong;

    // Start is called before the first frame update
    void Start()
    {
        selectedSong = PlayerPrefs.GetString("selectedSong");
        cubePool = new ObjectPool(beatCube.GetComponent<BeatCubeBehaviour>(), 10, true);
        string CubeInfoPath = Application.streamingAssetsPath + "/CubeInfo/" + selectedSong +".txt";
        if (File.Exists(CubeInfoPath))
        {
            inputJSON(CubeInfoPath);
        }
    }

    #region Lectura JSON
    public void inputJSON(string path)  //import del json
    {
        string json = File.ReadAllText(path);
        SpawningInfoList RecordingList = JsonUtility.FromJson<SpawningInfoList>(json);
        spawningQueue = RecordingList.list;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (spawningQueue.Count > 0) //Spawneo de cubos
        {
            if (Time.timeSinceLevelLoad >= spawningQueue[0].timeToSpawn)
            {
                    BeatCubeBehaviour spawnedCube;
                    if (spawningQueue[0].spawnSide == "Red")
                    {
                        spawnedCube = (BeatCubeBehaviour)cubePool.Get();
                        spawnedCube.pool = cubePool;
                        spawnedCube.gameObject.transform.position = spawnerRojo.position;
                        spawnedCube.GetComponent<Renderer>().material.color = Color.red;
                    }
                    else
                    {
                    spawnedCube = (BeatCubeBehaviour)cubePool.Get();
                    spawnedCube.pool = cubePool;
                    spawnedCube.gameObject.transform.position = spawnerAzul.position;
                    spawnedCube.GetComponent<Renderer>().material.color = Color.blue;
                    }
                    spawnedCube.transform.Rotate(0, 0, 45 * spawningQueue[0].rotationSegment);
                    spawningQueue.RemoveAt(0);
                
            }
        }

    }

}

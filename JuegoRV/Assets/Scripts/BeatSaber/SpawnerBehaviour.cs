using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnerBehaviour : MonoBehaviour
{
    public Transform spawnerRojo;
    public Transform spawnerAzul;
    public GameObject beatCube;
    private bool allowSpawn = false;
    public List<SpawningInfo> spawningQueue;


    public AudioSource audioPlayer;
    AudioClip audioClip;
    List<string> names=new List<string>();
    string randomSong;
    string soundPath;
    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo SoundsDir = new DirectoryInfo(Application.streamingAssetsPath + "/Sounds");
        FileInfo[] info = SoundsDir.GetFiles("*.mp3");
        
        foreach (FileInfo f in info) 
        {
            Debug.Log(Path.GetFileNameWithoutExtension(f.Name));
            names.Add(Path.GetFileNameWithoutExtension(f.Name));
        }
        randomSong = names[Random.Range(0, names.Count)];
        soundPath = Application.streamingAssetsPath + "/Sounds/"+ randomSong +".mp3";
        StartCoroutine(LoadAudio());
        string CubeInfoPath = Application.streamingAssetsPath + "/CubeInfo/" + randomSong +".json";
    }



    private IEnumerator LoadAudio() 
    {
        WWW request = GetAudioFromFile(soundPath);
        yield return request;

        audioClip = request.GetAudioClip();
        audioClip.name = randomSong;
        Debug.Log(audioClip.LoadAudioData());
        audioPlayer.clip = audioClip;
        audioPlayer.Play();
    }
    private WWW GetAudioFromFile(string path) 
    {
        WWW request = new WWW(path);
        return request;
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

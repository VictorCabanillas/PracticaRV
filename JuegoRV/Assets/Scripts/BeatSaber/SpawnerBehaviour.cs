using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnerBehaviour : MonoBehaviour
{
    public Transform spawnerRojo;
    public Transform spawnerAzul;
    public GameObject beatCube;
    public List<SpawningInfo> spawningQueue;

    
    public AudioSource audioPlayer;
    AudioClip audioClip;
    List<string> names=new List<string>();
    string randomSong;

    int count = 0;
    string CubeInfoPath;
    public SpawningInfoList cubeRecording = new ();

    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo SoundsDir = new DirectoryInfo(Application.streamingAssetsPath + "/Sounds"); //Saco la ruta de donde estan los archivos
        FileInfo[] info = SoundsDir.GetFiles("*.mp3"); //Saco todos los archivos .mp3 a un array
        
        foreach (FileInfo f in info) 
        {
            names.Add(Path.GetFileNameWithoutExtension(f.Name)); //Muestro y guardo los nombres sin la extension en una lista
        }
        randomSong = names[0]; //Elijo una cancion aleatoria
        StartCoroutine(LoadAudio()); //Pido que se reproduzca la cancion elegida


        CubeInfoPath = Application.streamingAssetsPath + "/CubeInfo/" + randomSong +".txt";
        if (File.Exists(CubeInfoPath))
        {
            inputJSON(CubeInfoPath);
        }
    }

    #region Lectura y escritura JSON
    public void outputJSON(string path,SpawningInfoList obj) 
    {
        string strOutput = JsonUtility.ToJson(obj);
        File.AppendAllText(path,strOutput);
    }

    public void inputJSON(string path) 
    {
        string json = File.ReadAllText(path);
        SpawningInfoList RecordingList = JsonUtility.FromJson<SpawningInfoList>(json);
        spawningQueue = RecordingList.list;
    }
    #endregion

    #region Extraer lista de canciones y poner la seleccionada
    private IEnumerator LoadAudio() 
    {
        string soundPath = Application.streamingAssetsPath + "/Sounds/" + randomSong + ".mp3"; //Construyo el path a la cancion
        WWW request = GetAudioFromFile(soundPath); //Pido que descargue el archivo
        yield return request; //Devuelvo a unity el control hasta que termine de descargar y despues continuo en este punto

        audioClip = request.GetAudioClip();  //Transformo el archivo a un audioClip
        audioClip.name = randomSong; //Le asigno un nombre (no es necesario)
        audioPlayer.clip = audioClip; //Cargo el clip al audioPlayer
        audioPlayer.Play(); //le doy al play
    }
    private WWW GetAudioFromFile(string path) 
    {
        WWW request = new WWW(path);
        return request;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (spawningQueue.Count > 0)
        {
            if (Time.timeSinceLevelLoad >= spawningQueue[0].timeToSpawn)
            {
                    GameObject spawnedCube;
                    if (spawningQueue[0].spawnSide == "Red")
                    {
                        spawnedCube = Instantiate(beatCube, spawnerRojo);
                        spawnedCube.GetComponent<BeatCubeBehaviour>().color = Color.red;
                    }
                    else
                    {
                        spawnedCube = Instantiate(beatCube, spawnerAzul);
                        spawnedCube.GetComponent<BeatCubeBehaviour>().color = Color.blue;
                    }
                    spawnedCube.transform.Rotate(0, 0, 45 * spawningQueue[0].rotationSegment);
                    spawningQueue.RemoveAt(0);
                
            }
        }


        

        if(Input.GetKeyDown(KeyCode.A)) 
        {
            float tiempo = (float)System.Math.Round((Time.timeSinceLevelLoad - (System.Math.Abs(-10 + spawnerAzul.transform.position.z) / beatCube.GetComponent<BeatCubeBehaviour>().speed)), 2);
            SpawningInfo spawnInfo = new SpawningInfo(count, tiempo, Random.Range(0, 8), "Red");
            count += 1;
            cubeRecording.list.Add(spawnInfo);
        }
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            float tiempo = (float)System.Math.Round((Time.timeSinceLevelLoad - (System.Math.Abs(-10 + spawnerAzul.transform.position.z) / beatCube.GetComponent<BeatCubeBehaviour>().speed)), 2);
            Debug.Log(tiempo);
            SpawningInfo spawnInfo = new SpawningInfo(count, tiempo, Random.Range(0, 8), "Blue");
            Debug.Log(spawnInfo.timeToSpawn);
            count += 1;
            cubeRecording.list.Add(spawnInfo);
        }

    }

    private void OnApplicationQuit()
    {
        if (!File.Exists(CubeInfoPath))
        {
            outputJSON(CubeInfoPath, cubeRecording);
        }
    }
}

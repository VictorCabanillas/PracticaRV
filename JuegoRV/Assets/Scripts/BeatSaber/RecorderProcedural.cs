using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RecorderProcedural : MonoBehaviour
{

    public GameObject audioRecorder;
    private SpawningInfoList cubeRecording = new();
    private int count = 0;

    private string CubeInfoPath;

    public Transform spawnerAzul;
    public GameObject beatCube;

    private bool allowCubeR = true;
    private bool allowCubeL = true;

    public float recordingThreshold = 0.2f;

    bool recarga = true;



    private float[] samples;
    int bufferSize;
    int numBuffers;
    private AudioSource audioSource;
    public GameObject sphere;
    public GameObject sphere2;

    // Start is called before the first frame update
    void Start()
    {
        recordingThreshold = PlayerPrefs.GetFloat("Threshold");
        string selectedSong = PlayerPrefs.GetString("selectedSong");
        CubeInfoPath = Application.streamingAssetsPath + "/CubeInfo/" + selectedSong + ".txt";
        SceneManager.sceneUnloaded+=OnSceneUnload;
        AudioSettings.GetDSPBufferSize(out bufferSize,out numBuffers);
        samples = new float[bufferSize];
        audioSource = audioRecorder.GetComponent<AudioSource>();
    }


    private float getVolume() 
    {
        audioSource.GetOutputData(samples, 0);
        float sum=0;
        for (int i = 0; i < bufferSize; i++)
        {
            sum += samples[i] * samples[i]; // sum squared samples
        }
        sum = Mathf.Sqrt(sum/bufferSize);
        return sum;
    }

    private void Update()
    {
        float volume = getVolume();
        Debug.Log(volume);
        sphere.transform.localScale =new Vector3(volume,volume,volume);
        sphere2.transform.localScale = new Vector3(volume, volume, volume);
        if (volume > recordingThreshold && recarga)
        {
            recarga = false;
            StartCoroutine(recargando());
            Debug.Log("Pop");
            calculateSide(Random.Range(0, 10) % 2 == 0 ? "Red" : "Blue");
        }
    }

    IEnumerator recargando() 
    {
        yield return new WaitForSecondsRealtime(0.75f);
        recarga = true;
    }

    void calculateSide(string side)
    {
        int rotSegment;
        bool both = Random.Range(0, 10) < 1 ? true : false;
        if ((side == "Red"|| both) && allowCubeL)
        {
            side = "Red";
            allowCubeL = false;
            do { rotSegment = Random.Range(0, 8); } while (rotSegment == 3 || rotSegment == 5);
            writeCube(rotSegment, side);
            StartCoroutine(cooldown(true));
        }
        if ((side == "Blue" || both) && allowCubeR) 
        {
            side = "Blue";
            allowCubeR = false;
            do { rotSegment = Random.Range(0, 8); } while (rotSegment == 3 || rotSegment == 5);
            writeCube(rotSegment, side);
            StartCoroutine(cooldown(false));
        }

    }
    
    IEnumerator cooldown(bool red)
    {
        yield return new WaitForSecondsRealtime(1f);
        if (red)
        {
            allowCubeL = true;
        }
        else
        {
            allowCubeR = true;
        }
    }

    void writeCube(int rotSegment, string side)
    {
        float tiempo = (float)System.Math.Round((Time.timeSinceLevelLoad - ((spawnerAzul.transform.position.z- 1f) / beatCube.GetComponent<BeatCubeBehaviour>().speed)), 2);
        SpawningInfo spawnInfo = new SpawningInfo(count, tiempo, rotSegment, side);
        count += 1;
        cubeRecording.list.Add(spawnInfo);
    }

    private void OnSceneUnload(Scene current) 
    {
        if (!File.Exists(CubeInfoPath))
        {
            outputJSON(CubeInfoPath, cubeRecording);
        }
    }

    private void OnApplicationQuit()
    {
        if (!File.Exists(CubeInfoPath))
        {
            outputJSON(CubeInfoPath, cubeRecording);
        }
    }
    public void outputJSON(string path, SpawningInfoList obj)
    {
        string strOutput = JsonUtility.ToJson(obj);
        File.AppendAllText(path, strOutput);
    }
}


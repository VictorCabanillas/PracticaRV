using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RecorderProcedural : MonoBehaviour
{

    public GameObject audioRecorder;
    private AudioInput audioInput;
    private SpawningInfoList cubeRecording = new();
    private int count = 0;

    private string CubeInfoPath;

    public Transform spawnerAzul;
    public GameObject beatCube;

    [Space] [SerializeField] private InputActionAsset myActionsAsset;

    private bool allowCubeR = true;
    private bool allowCubeL = true;

    public float recordingThreshold = 0.65f;

    // Start is called before the first frame update
    void Start()
    {
        audioInput = audioRecorder.GetComponent<AudioInput>();
        string selectedSong = PlayerPrefs.GetString("selectedSong");
        CubeInfoPath = Application.streamingAssetsPath + "/CubeInfo/" + selectedSong + ".txt";

        StartCoroutine(timer());
        SceneManager.sceneUnloaded+=OnSceneUnload;
    }

    IEnumerator timer() 
    {
        yield return new WaitForSecondsRealtime(1f);
        //Debug.Log(audioInput.GetAmplitude());
        //Debug.Log(audioInput.GetFreqBand(2));
        //if (audioInput.GetAmplitude() > recordingThreshold)
        if (audioInput.GetFreqBand(2)>recordingThreshold)
        {
            calculateSide(Random.Range(0, 10)%2 == 0 ? "Red" : "Blue");
        }
        StartCoroutine(timer());
    }

    void calculateSide(string side)
    {
        string both = (Random.Range(0, 10) > 4 ? side : "Both");
        int rotSegment = Random.Range(0, 8);
        if (rotSegment == 5 || rotSegment == 3) rotSegment = rotSegment==3? rotSegment-1:rotSegment+1;
        if ((side == "Red"|| both == "Both") && allowCubeL)
        {
            allowCubeL = false;
            StartCoroutine(cooldown(side));
            writeCube(rotSegment, side);
        }
        if ((side == "Blue" || both == "Both") && allowCubeR) 
        {
            allowCubeR = false;
            StartCoroutine(cooldown(side));
            writeCube(rotSegment, side);
        }

    }

    IEnumerator cooldown(string side)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (side == "Red")
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
        float tiempo = (float)System.Math.Round((Time.timeSinceLevelLoad - (spawnerAzul.transform.position.z / beatCube.GetComponent<BeatCubeBehaviour>().speed)), 2);
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


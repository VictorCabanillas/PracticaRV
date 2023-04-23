using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

public class Recorder : MonoBehaviour
{
    private SpawningInfoList cubeRecording = new();
    private int count = 0;

    private string CubeInfoPath;

    public Transform spawnerAzul;
    public GameObject beatCube;

    private InputAction leftAction;
    private InputAction rightAction;
    [Space] [SerializeField] private InputActionAsset myActionsAsset;

    private bool allowCube = true;

    // Start is called before the first frame update
    void Start()
    {
        string selectedSong = PlayerPrefs.GetString("selectedSong");
        CubeInfoPath = Application.streamingAssetsPath + "/CubeInfo/" + selectedSong + ".txt";

        leftAction = myActionsAsset.FindAction("XRI LeftHand Interaction/Translate Anchor");
        leftAction.performed += LeftContol;

        rightAction = myActionsAsset.FindAction("XRI RightHand Interaction/Translate Anchor");
        rightAction.performed += RightControl;
    }

    void LeftContol(InputAction.CallbackContext context)
    {
        calculateRot(context, "Red");
    }
    void RightControl(InputAction.CallbackContext context) 
    {
        calculateRot(context, "Blue");
    }
    void calculateRot(InputAction.CallbackContext context, string side)
    {
        if (allowCube)
        {
            allowCube = false;
            Debug.Log(context.ReadValue<Vector2>());
            int rotSegment = 0;
            if (context.ReadValue<Vector2>().x > 0.5f)
            {
                if (context.ReadValue<Vector2>().y > 0.5f)
                {
                    rotSegment = 1; //1,1
                }
                else if (context.ReadValue<Vector2>().y < 0.5f)
                {
                    rotSegment = 7; //1,-1
                }
                else
                {
                    rotSegment = 0; //1,0
                }
            }
            else if (context.ReadValue<Vector2>().x < 0.5f)
            {
                if (context.ReadValue<Vector2>().y > 0.5f)
                {
                    rotSegment = 3; //-1,1
                }
                else if (context.ReadValue<Vector2>().y < 0.5f)
                {
                    rotSegment = 5; //-1,-1
                }
                else
                {
                    rotSegment = 4; //-1,0
                }
            }
            else
            {
                if (context.ReadValue<Vector2>().y > 0.5f)
                {
                    rotSegment = 2; //0,1
                }
                else if (context.ReadValue<Vector2>().y < 0.5f)
                {
                    rotSegment = 6; //0,-1
                }
            }

            StartCoroutine(cooldown());
            writeCube(rotSegment, side);
        }
    }

    IEnumerator cooldown() 
    {
        yield return new WaitForSecondsRealtime(0.5f);
        allowCube = true;
    }

    void writeCube(int rotSegment,string side) 
    {
        float tiempo = (float)System.Math.Round((Time.timeSinceLevelLoad - (spawnerAzul.transform.position.z / beatCube.GetComponent<BeatCubeBehaviour>().speed)), 2);
        SpawningInfo spawnInfo = new SpawningInfo(count, tiempo, rotSegment, side);
        count += 1;
        cubeRecording.list.Add(spawnInfo);
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

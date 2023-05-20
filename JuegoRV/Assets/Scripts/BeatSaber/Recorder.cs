using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

public class Recorder : MonoBehaviour
{
    //Creacion de variables
    private SpawningInfoList cubeRecording = new();
    private int count = 0;

    private string CubeInfoPath;

    public Transform spawnerAzul;
    public GameObject beatCube;

    private InputAction leftAction;
    private InputAction rightAction;
    [Space] [SerializeField] private InputActionAsset myActionsAsset;

    private bool allowCubeR = true;
    private bool allowCubeL = true;

    void Start()
    {
        //Asignacion de variables y creacion de callbacks a eventos de los mandos
        string selectedSong = PlayerPrefs.GetString("selectedSong");
        CubeInfoPath = Application.streamingAssetsPath + "/CubeInfo/" + selectedSong + ".txt";

        leftAction = myActionsAsset.FindAction("XRI LeftHand Interaction/Translate Anchor");
        leftAction.performed += LeftContol;

        rightAction = myActionsAsset.FindAction("XRI RightHand Interaction/Translate Anchor");
        rightAction.performed += RightControl;
    }

    //Cuando se mueve el joystick de un mando si no esta en cooldown se crea un cubo en el lado correspondiente al mando
    void LeftContol(InputAction.CallbackContext context)
    {
        if (allowCubeL)
        {
            allowCubeL = false;
            calculateRot(context, "Red");
        }
    }
    void RightControl(InputAction.CallbackContext context)
    {
        if (allowCubeR)
        {
            allowCubeR = false;
            calculateRot(context, "Blue");
        }
    }

    //En funcion de la posicion del joystick se determina la orientacion del cubo
    void calculateRot(InputAction.CallbackContext context, string side)
    {
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

            StartCoroutine(cooldown(side));
            writeCube(rotSegment, side);
        
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

    //Se crea el cubo y se añade a la lista
    void writeCube(int rotSegment,string side) 
    {
        float tiempo = (float)System.Math.Round((Time.timeSinceLevelLoad - (spawnerAzul.transform.position.z / beatCube.GetComponent<BeatCubeBehaviour>().speed)), 2);
        SpawningInfo spawnInfo = new SpawningInfo(count, tiempo, rotSegment, side);
        count += 1;
        cubeRecording.list.Add(spawnInfo);
    }

    //Al salir de la escena o finalizar la aplicacion se crea el archivo con los cubos generados
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

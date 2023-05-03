using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] UIShooter uiShooter;
    [SerializeField] XROrigin XRUI;
    [SerializeField] Canvas canvas;
    
    public void EscenaShooter()
    {
        SceneManager.LoadScene("Shooter");
    }

    public void EmpezarJuego()
    {
        XRUI.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
        uiShooter.EmpezarJuego();
        
       
    }

}

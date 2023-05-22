using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] UIShooter uiShooter;
    [SerializeField] XROrigin XRUI;
    [SerializeField] XROrigin XRShooter;
    [SerializeField] Canvas canvas;

    [SerializeField] TimeManager timeManager;
    [SerializeField] CanvasGroup timePanel;

    
    //cambio al shooter
    public void EscenaShooter()
    {
        SceneManager.LoadScene("Shooter");
    }

    //cambio al beatsaber
    public void EscenaBeatSaber()
    {
        SceneManager.LoadScene("SongSelector");
    }

    //empezar juego del shooter 
    public void EmpezarJuego()
    {
        XRUI.gameObject.SetActive(true);
        canvas.gameObject.SetActive(false);
        uiShooter.EmpezarJuego();
        timePanel.alpha = 1.0f;
        timeManager.Restart();
    }

    //salir al menu sel shooter
    public void SalirAlMenu()
    {
        XRShooter.gameObject.SetActive(false) ;
        XRUI.gameObject.SetActive(false) ;
        SceneManager.LoadScene("MainMenu");
    }


}

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

    

    public void EscenaShooter()
    {
        SceneManager.LoadScene("Shooter");
    }

    public void EscenaBeatSaber()
    {
        SceneManager.LoadScene("SongSelector");
    }

    public void EmpezarJuego()
    {
        XRUI.gameObject.SetActive(true);
        canvas.gameObject.SetActive(false);
        uiShooter.EmpezarJuego();
        timePanel.alpha = 1.0f;
        timeManager.Restart();
    }

    public void SalirAlMenu()
    {
        XRShooter.gameObject.SetActive(false) ;
        XRUI.gameObject.SetActive(false) ;
        SceneManager.LoadScene("MainMenu");
    }


}

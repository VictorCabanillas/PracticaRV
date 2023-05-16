using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Records : MonoBehaviour
{
    [SerializeField] XROrigin XRUI;
    [SerializeField] UIShooter uiShooter;
    [SerializeField] Score score;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TimeManager timeManager;


    private List<int> scores;
    public int scoreNumbers = 5;
    public Canvas canvas;

    void Awake()
    {
        CerrarRecords();
        scores = new List<int>();
    }

    public void AbrirRecords()
    {
        gameObject.SetActive(true);

        textScore.text = "Aciertos \n " + score.score;

        scores.Add(score.score);
        scores.Sort(delegate (int A, int B)
        {
            if (A > B) { return -1; }
            return 1;
        });

        textMesh.text = "";

        for (int i = 0; i < scoreNumbers; i++)
        {
            if (i == scores.Count)
            {
                break;
            }
            textMesh.text += (i + 1) + "º: " + scores[i] + "\n";
        }

        XRUI.gameObject.SetActive(true);
    }
    public void CerrarRecords()
    {
        XRUI.gameObject.SetActive(false);
        gameObject.SetActive(false);

    }

    public void VolverAJugar()
    {
        CerrarRecords();
        uiShooter.EmpezarJuego();
        score.ResetJuego();
        timeManager.Restart();
        
    }
    
    public void SalirAlMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

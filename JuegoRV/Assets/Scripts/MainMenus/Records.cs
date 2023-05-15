using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Records : MonoBehaviour
{
    [SerializeField] XROrigin XRUI;
    [SerializeField] UIShooter uiShooter;
    [SerializeField] Score score;


    private List<int> scores;

    void Awake()
    {
        scores = new List<int>();
    }
    public void AbrirRecords()
    {
        scores.Add(score.score);
        scores.Sort(delegate(int A, int B)
        {
            if (A > B) { return -1; }
            return 1;
        });

        XRUI.gameObject.SetActive(true);
    }
    public void CerrarRecords()
    {
        XRUI.gameObject.SetActive(false);

    }

    public void VolverAJugar()
    {
        CerrarRecords();
        uiShooter.EmpezarJuego();
    }
}

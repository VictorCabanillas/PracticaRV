using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Records : MonoBehaviour
{
    [SerializeField] XROrigin XRUI;
    [SerializeField] UIShooter uiShooter;

    public void AbrirRecords()
    {
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

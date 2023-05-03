using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class UIShooter : MonoBehaviour
{
    [SerializeField] XROrigin XRShooter;
    [SerializeField] Records record;

    public void EmpezarJuego()
    {
        XRShooter.gameObject.SetActive(true);
    }

    public void TerminarJuego()
    {
        XRShooter.gameObject.SetActive(false);
        record.AbrirRecords();
    }

}

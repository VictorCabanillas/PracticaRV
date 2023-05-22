using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class UIShooter : MonoBehaviour
{
    [SerializeField] XROrigin XRShooter;
    [SerializeField] XROrigin UIshooter;

    [SerializeField] Records record;
    [SerializeField] DianaManager diana;

    [SerializeField] GameObject pistola1;
    [SerializeField] GameObject pistola2;
    [SerializeField] GameObject mesa;

    Vector3 v1 = new Vector3(2.38100004f, 3.2809999f, 4.58500004f);

    public void EmpezarJuego()
    {
        UIshooter.gameObject.SetActive(false);

        XRShooter.gameObject.SetActive(true);
        diana.gameObject.SetActive(true);

        pistola1.gameObject.SetActive(true);
        pistola2.gameObject.SetActive(true);
        mesa.gameObject.SetActive(true);

        pistola1.gameObject.transform.position = new Vector3(2.38100004f, 3.2809999f, 4.58500004f);
        pistola2.gameObject.transform.position = new Vector3(2.38000011f, 3.2809999f, 4.25f);
        XRShooter.gameObject.transform.position = new Vector3(0f, 3.04999995f, 5.46999979f);
        
    }

    public void TerminarJuego()
    {
        XRShooter.gameObject.SetActive(false);
        pistola1.gameObject.SetActive(false);
        pistola2.gameObject.SetActive(false);
        mesa.gameObject.SetActive(false);
        diana.gameObject.SetActive(false);


       

        record.AbrirRecords();
    }

}

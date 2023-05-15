using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    [SerializeField] int minutos, segundos;
    [SerializeField] TextMeshProUGUI tiempo;
    [SerializeField] UIShooter uIShooter;

    private float restante;
    public bool cuentaActiva;

    
    public void Restart()
    {
        restante = (minutos * 60) + segundos;
        cuentaActiva = true;
    }


    void Update()
    {
        if (cuentaActiva)
        {
            restante -= Time.deltaTime;
            if (restante < 1)
            {
                cuentaActiva = false;
                uIShooter.TerminarJuego();
            }
            int tempMinutos = Mathf.FloorToInt(restante / 60);

            int tempSegundos = Mathf.FloorToInt(restante % 60);
            tiempo.text = string.Format("{00:00}:{01:00}", tempMinutos, tempSegundos);

        }

    }
}

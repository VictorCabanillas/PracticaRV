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

    //Cuenta atr�s segun el tiempo especificado en el inspector
    public void Restart()
    {
        restante = (minutos * 60) + segundos;
        cuentaActiva = true;
    }

    //Actualizamos el contador por cada segundo que pase
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

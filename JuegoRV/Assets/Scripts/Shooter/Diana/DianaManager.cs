using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaManager : MonoBehaviour
{
    public int MaxDianas;
    public List<Diana> dianasActivas;
    public List<Diana> dianasDesactivadas;

    //Establecemos cual va a ser la diana activa en funci�n del n�mero de diana que se ubiquen en el escenario, activando un total de dianas en funci�n de las 
    //especificadas en el inspector
    private void Update()
    {
        if(dianasActivas.Count != MaxDianas)
        {
            if(dianasDesactivadas.Count != 0)
            {
                int RandomIndice = UnityEngine.Random.Range(0, dianasDesactivadas.Count);
                dianasActivas.Add(dianasDesactivadas[RandomIndice]);
                dianasDesactivadas[RandomIndice].Activar();
                dianasDesactivadas.RemoveAt(RandomIndice);
            }
        }
    }

    private void Awake()
    {
        dianasDesactivadas = new List<Diana>(FindObjectsOfType<Diana>());

    }
}

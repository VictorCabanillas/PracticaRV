using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaManager : MonoBehaviour
{
    public int MaxDianas;
    public List<Diana> dianasActivas;
    public List<Diana> dianasDesactivadas;

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

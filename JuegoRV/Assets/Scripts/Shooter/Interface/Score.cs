using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Score : MonoBehaviour
{
    [SerializeField] TextMeshPro dianasAcertadas;
    public int score = 0;

    private void Awake()
    {
        //Reseteamos el contador de la tabla
        ResetJuego();
    }

    public void SubirContador()
    {
        //En casp de acertar metemos el siguiente texto
        score += 1;
        dianasAcertadas.text = "Aciertos: " + score.ToString();

    }
    public void ResetJuego()
    {
        //Reseteamos a 0 el contador por si se empieza una nueva ronda
        score = 0;
        dianasAcertadas.text = "Aciertos: " + score; 
    }

}

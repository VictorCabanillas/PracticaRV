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
        ResetJuego();
    }

    public void SubirContador()
    {
        score += 1;
        dianasAcertadas.text = "Aciertos: " + score.ToString();

    }
    public void ResetJuego()
    {
        score = 0;
        dianasAcertadas.text = "Aciertos: " + score; 
    }

}

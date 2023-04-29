using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Score : MonoBehaviour
{
    [SerializeField] TextMeshPro dianasAcertadas;
    int score = 0;

    private void Awake()
    {
        dianasAcertadas.text = "Dianas acertadas " + score;
    }

    public void SubirContador()
    {
        score += 1;
        dianasAcertadas.text = "Dianas acertadas " + score.ToString();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboBehaviour : MonoBehaviour
{
    //Creacion de variables
    ScoreBehaviour score;
    TextMeshProUGUI text;
    public GameObject multiplyerGO;
    private TextMeshProUGUI multiplyerText;
    int combo=0;
    int multiplyer=1;
    private void Start()
    {
        //Asignacion de variables
        PlayerPrefs.SetInt("highestCombo", 0);
        text = GetComponent<TextMeshProUGUI>();
        score = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreBehaviour>();
        multiplyerText = multiplyerGO.GetComponent<TextMeshProUGUI>();
    }

    public void addCombo() 
    {
        //Suma uno al combo al cortar un cubo, actualiza el multiplicador y suma la puntuacion
        combo += 1;
        calculateMultiplyer();
        multiplyerText.text = "x"+ multiplyer.ToString();
        score.UpdateScore(100*multiplyer);
        text.text = combo.ToString();
        if (PlayerPrefs.GetInt("highestCombo") < combo) { PlayerPrefs.SetInt("highestCombo", combo); }
    }
    public void resetCombo()
    {
        //Resetea el combo y el multiplicador
        combo = 0;
        multiplyer = 1;
        text.text = combo.ToString();
    }
    void calculateMultiplyer() 
    {
        //Asigna el valor del multiplicador
        switch (combo) 
        {
            case 2:
            case 3:
            case 4:
            case 5:
            case 10:
            case 20:
                multiplyer = combo;
                return;
        }
    }
}

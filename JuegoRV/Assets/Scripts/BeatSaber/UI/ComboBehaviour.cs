using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboBehaviour : MonoBehaviour
{
    ScoreBehaviour score;
    TextMeshProUGUI text;
    public GameObject multiplyerGO;
    private TextMeshProUGUI multiplyerText;
    int combo=0;
    int multiplyer=1;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        score = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreBehaviour>();
        multiplyerText = multiplyerGO.GetComponent<TextMeshProUGUI>();
    }

    public void addCombo() 
    {
        combo += 1;
        calculateMultiplyer();
        multiplyerText.text = "x"+ multiplyer.ToString();
        score.UpdateScore(100*multiplyer);
        text.text = combo.ToString();
    }
    public void resetCombo()
    {
        combo = 0;
        multiplyer = 1;
        text.text = combo.ToString();
    }
    void calculateMultiplyer() 
    {
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

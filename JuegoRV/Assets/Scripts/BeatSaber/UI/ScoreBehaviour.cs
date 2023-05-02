using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBehaviour : MonoBehaviour
{
    TextMeshProUGUI text;
    int score = 0;
    private void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        text = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateScore(int data)
    {
        score += data;
        text.text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
    }
}

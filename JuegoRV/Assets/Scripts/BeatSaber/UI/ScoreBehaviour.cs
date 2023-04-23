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
        text = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateScore(int data)
    {
        score += data;
        text.text = score.ToString();
    }
}

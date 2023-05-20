using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreenBehaviour : MonoBehaviour
{
    //Comportamiento de la pantalla final para mostrar los puntos y gestionar el funcionamiento de los botones
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
        comboText.text = PlayerPrefs.GetInt("highestCombo").ToString();
    }

    public void retry() 
    {
        SceneManager.LoadScene("BeatSaber");
    }

    public void mainMenu() 
    {
        SceneManager.LoadScene("SongSelector");
    }

}

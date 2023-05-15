using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreenBehaviour : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    // Start is called before the first frame update
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

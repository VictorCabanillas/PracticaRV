using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Globalization;

public class ButtonBehaviour : MonoBehaviour
{
    //Comportamiento de los botones para realizar cambios de escena y paso de informacion entre escenas mediante PlayerPrefs
    public TMP_InputField inputField;
    private void Start()
    {
        //Debug.Log(PlayerPrefs.GetFloat("Threshold").ToString(CultureInfo.InvariantCulture));
        inputField.text = PlayerPrefs.GetFloat("Threshold").ToString(CultureInfo.InvariantCulture); //Uso de invariant culture para interpretar los decimales siempre usando un .
    }
    public void Play() 
    {
        SceneManager.LoadScene("BeatSaber");
    }

    public void GrabarManual() 
    {
        SceneManager.LoadScene("BeatSaber");
    }

    public void GrabarProcedural() 
    {
        //Debug.Log(float.Parse(inputField.text, CultureInfo.InvariantCulture));
        PlayerPrefs.SetFloat("Threshold", float.Parse(inputField.text, CultureInfo.InvariantCulture)); //Convertimos el numero escrito a float y lo almacenamos
        SceneManager.LoadScene("BeatSaberProcedural");
    }
}

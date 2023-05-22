using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Records : MonoBehaviour
{
    [SerializeField] XROrigin XRUI;
    [SerializeField] UIShooter uiShooter;
    [SerializeField] Score score;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TimeManager timeManager;


    private List<int> scores;
    public int scoreNumbers = 5;
    public Canvas canvas;

    void Awake()
    {
        //Para evitar que los records salgan junto a la interfaz principal la primera vez que accedamos al juego, se cierra indicandolo en el Awake
        CerrarRecords();
        scores = new List<int>();
    }

    public void AbrirRecords() //Método que llamamos cuando finalize cada ronda
    {
        gameObject.SetActive(true); //Aquí referenciamos la interfaz de records 

        textScore.text = "Aciertos \n " + score.score; //Llamamos a los records almacenados en la clase score que también usamos para el contador

        //el siguiente codigo nos ordena el texto de los records y la añade si ha sido mejor que los 3 últimos en caso de encontrarse en la 4 ronda o más, o simplemente
        //los ordena si se encuentra en la primera, segunda o tercera ya que se van añadiendo a medida que avanzan las rondas
        scores.Add(score.score);
        scores.Sort(delegate (int A, int B)
        {
            if (A > B) { return -1; }
            return 1;
        });

        textMesh.text = "";

        for (int i = 0; i < scoreNumbers; i++)
        {
            if (i == scores.Count)
            {
                break;
            }
            textMesh.text += (i + 1) + "º: " + scores[i] + "\n";
        }
        //Tambíen activamos el XR origin para el control de las interfaces
        XRUI.gameObject.SetActive(true);
    }
    public void CerrarRecords()
    {
        //Cierra la interfaz de records
        gameObject.SetActive(false);

    }

    public void VolverAJugar()
    {
        //Cuando volvemos a jugar realiza las siguientes acciones reiniciando el campo de juego y el contador del record propio de la partida
        CerrarRecords();
        uiShooter.EmpezarJuego();
        score.ResetJuego();
        timeManager.Restart();
        
    }
    
    public void SalirAlMenu()
    {
        //Salimos al menú
        SceneManager.LoadScene("MainMenu");
    }

}

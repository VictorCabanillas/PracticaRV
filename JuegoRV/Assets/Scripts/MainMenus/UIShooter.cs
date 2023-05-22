using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class UIShooter : MonoBehaviour
{
    [SerializeField] XROrigin XRShooter;
    [SerializeField] XROrigin UIshooter;

    [SerializeField] Records record;
    [SerializeField] DianaManager diana;

    [SerializeField] GameObject pistola1;
    [SerializeField] GameObject pistola2;
    [SerializeField] GameObject mesa;

    
    //El siguiente método hace referencia al comienzo del juego/ronda
    public void EmpezarJuego()
    {
        //Activamos el XR Origin encargado de las interfaces y activamos el que contiene a las manos y movimiento por el mapa
        UIshooter.gameObject.SetActive(false);
        XRShooter.gameObject.SetActive(true);
        diana.gameObject.SetActive(true); //Activamos las dianas

        //Activamos todos los objetos del juego
        pistola1.gameObject.SetActive(true);
        pistola2.gameObject.SetActive(true);
        mesa.gameObject.SetActive(true);

        //Devolvemos a las pistolas y al jugador a la posición por defecto ya que, al desactivar el XR origin del movimiento, su posición se queda en la última, por ello
        //volvemos a donde aparece el jugador para resetearlo
        pistola1.gameObject.transform.position = new Vector3(2.38100004f, 3.2809999f, 4.58500004f);
        pistola2.gameObject.transform.position = new Vector3(2.38000011f, 3.2809999f, 4.25f);
        XRShooter.gameObject.transform.position = new Vector3(0f, 3.04999995f, 5.46999979f);
        
    }

    public void TerminarJuego()
    {
        //Si se termina el juego, se desactivan todos los gameobject referentes a la partida como las pistolas, dianas... y se llama al método de records encargado
        //de abrir la interfaz de records
        XRShooter.gameObject.SetActive(false);
        pistola1.gameObject.SetActive(false);
        pistola2.gameObject.SetActive(false);
        mesa.gameObject.SetActive(false);
        diana.gameObject.SetActive(false);


       

        record.AbrirRecords();
    }

}

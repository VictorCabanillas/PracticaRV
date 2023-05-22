using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Diana : MonoBehaviour
{
    public UnityEvent OnActivar;
    public UnityEvent OnDesactivar;
    public bool Activada;

    //Llamamos al activar las dianas
    public void Activar()
    {
        Activada = true;
        OnActivar.Invoke();
    }

    //Cuando se desactivan las dianas, vamos actualizando el contador y eliminando y a�adiendo cada una de ellas
    public void Desactivar()
    {
        Activada = false;
        OnDesactivar.Invoke();

        var dianaManger = FindObjectOfType<DianaManager>();
        dianaManger.dianasActivas.Remove(this);
        dianaManger.dianasDesactivadas.Add(this);

        var score = FindObjectOfType<Score>();
        score.SubirContador();
    }

    //M�todo para que si golpeamos una en el estado "activada" , se desactiva
    public void Golpear()
    {
        if(Activada)
        {
            Desactivar();
        }
    }

}

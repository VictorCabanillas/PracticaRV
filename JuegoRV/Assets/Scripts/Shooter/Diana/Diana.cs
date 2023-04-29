using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Diana : MonoBehaviour
{
    public UnityEvent OnActivar;
    public UnityEvent OnDesactivar;
    public bool Activada;

    public void Activar()
    {
        Activada = true;
        OnActivar.Invoke();
    }

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

    public void Golpear()
    {
        if(Activada)
        {
            Desactivar();
        }
    }

}

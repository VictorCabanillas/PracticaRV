using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


//Este script se encarga de esconder la mano o el objeto que se tenga por defecto en cada una de las manos para que solo se ve la pistola

public class EsconderMano : MonoBehaviour
{
    private MeshRenderer[] mallas;

    private void Awake()
    {
        mallas = GetComponentsInChildren<MeshRenderer>();
    }

    //Se muestra el objeto (mano)
    public void Mostrar()
    {
        foreach(var malla in mallas)
        {
            malla.enabled = true;
        }
    }

    //Se esconde el objeto (mano)
    public void Esconder()
    {
        foreach(var malla in mallas)
        {
            malla.enabled = false;
        }
    }


}

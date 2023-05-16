using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{
    protected Arma arma;

    public virtual void Init(Arma arma)
    {
        this.arma = arma;
    }

    public virtual void Launch()
    {

    }

    
    public void Golpear(Collider other)
    {
        var diana = other.GetComponent<Diana>();
        if (diana)
        {
            diana.Golpear();
        }
    }

}

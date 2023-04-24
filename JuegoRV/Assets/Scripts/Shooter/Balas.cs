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
}

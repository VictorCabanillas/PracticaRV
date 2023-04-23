using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistola : Arma
{

    [SerializeField] private GameObject balaPrefab;

    protected override void EmpezarDisparo(BaseInteractionEventArgs interactor)
    {
        base.EmpezarDisparo(interactor);
        Disparar();
    }

    protected override void Disparar()
    {
        base.Disparar();
        GameObject proyectil = Instantiate(balaPrefab, aparicionBalas.position, aparicionBalas.rotation);
    }

    protected override void PararDisparo(BaseInteractionEventArgs interactor)
    {
        base.PararDisparo(interactor);
    }
}

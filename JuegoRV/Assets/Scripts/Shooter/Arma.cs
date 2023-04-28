using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]

public class Arma : MonoBehaviour
{

    //Componentes del indepsctor referentes a los atributos de disparo
    [SerializeField] protected float fuerzaDisparo;
    [SerializeField] protected Transform aparicionBalas;
    [SerializeField] private float fuerzaRetroceso;
    [SerializeField] private float dano;

    private Rigidbody rigiBody;
    private XRGrabInteractable interactableWeapon;

    protected virtual void Awake()
    {
        rigiBody = GetComponent<Rigidbody>();
        interactableWeapon = GetComponent<XRGrabInteractable>();
        SetupInteractableWeaponEvents();
    }

    private void SetupInteractableWeaponEvents()
    {
        interactableWeapon.selectEntered.AddListener(PickUpWeapon);
        interactableWeapon.selectExited.AddListener(SoltarArma);
        interactableWeapon.activated.AddListener(EmpezarDisparo);
        interactableWeapon.deactivated.AddListener(PararDisparo);
    }

    private void PickUpWeapon(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRBaseControllerInteractor interactor)
        {
           //interactor.GetComponent<EsconderMano>().Esconder();
        }
    }

    private void SoltarArma(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRBaseControllerInteractor interactor)
        {
            //interactor.GetComponent<EsconderMano>().Mostrar();
        }
    }

    protected virtual void EmpezarDisparo(BaseInteractionEventArgs interactor)
    {
        //throw new NotImplementedException();
    }

    protected virtual void PararDisparo(BaseInteractionEventArgs interactor)
    {
        //throw new NotImplementedException();
    }

    protected virtual void Disparar()
    {
        AplicarRetroceso();
    }

    //M�todo en referencia al retroceso de cuando un arma es disparada, la siguiente formula nos indica como ser�a el retroceso en base al valor
    //que le hayamos asignado en el inspector
    private void AplicarRetroceso()
    {
        rigiBody.AddRelativeForce(Vector3.back * fuerzaRetroceso, ForceMode.Impulse);
    }

    public float GetFuerzaDisparo()
    {
        return fuerzaDisparo;
    }

    public float GetDano()
    {
        return dano;
    }

}

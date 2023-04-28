using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]

public class FisicasBalas : Balas
{

    [SerializeField] private float tiempoBalas; //Tiempo en que las balas van a estar (tras ello despawnean)
    private Rigidbody rigiBody;
    public UnityEvent OnGolpear;

    private void Awake()
    {
        rigiBody = GetComponent<Rigidbody>();
    }

    public override void Init(Arma arma)
    {
        base.Init(arma);
        Destroy(gameObject, tiempoBalas);
    }

    public override void Launch()
    {
        base.Launch();
        rigiBody.AddRelativeForce(Vector3.forward * arma.GetFuerzaDisparo(), ForceMode.Impulse);
    }

    //En caso de colision de la bala con algún collider establecido como pueden ser los enemigos, el objeto se destruye
    private void OnTriggerEnter(Collider other)
    {
        var bala = GetComponent<Balas>();
        bala.Golpear(other);
        Destroy(gameObject);
        IRecibeDano[] recibeDaño = other.GetComponentsInChildren<IRecibeDano>();

        foreach(var taker in recibeDaño)
        {
            taker.RecibeDano(arma, this, transform.position);
        }

    }
}

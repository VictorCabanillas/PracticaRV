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
        rigiBody.AddRelativeForce(Vector3.forward * arma.GetFuerzaDisparo(), ForceMode.Impulse); //Formula que aplica la fuerza a la bala con los parametros dados
    }

    //En caso de colision de la bala con algún collider establecido como pueden ser los enemigos, el objeto se destruye aunque realmente no se acabó utilizando ya 
    //que el tema de activar y desactivar con los coques lo llevan las dianas
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//SCRIPT DESECHADO YA QUE LAS DIANA FINALMENTE NO TIENEN VIDA

[RequireComponent(typeof(Rigidbody))]

public class RecibeDaño : MonoBehaviour, IRecibeDano
{
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void RecibeDano(Arma arma, Balas bala, Vector3 puntoContacto)
    {
        rb.AddForce(bala.transform.forward * arma.GetFuerzaDisparo(), ForceMode.Impulse);
    }

}

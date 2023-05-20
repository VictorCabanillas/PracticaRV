using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullBehaviour : MonoBehaviour
{
    //Destruye las mitades pasado unos segundos
    void Start()
    {
        StartCoroutine(destroyAfterTime());
    }

    IEnumerator destroyAfterTime() 
    {
        yield return new WaitForSecondsRealtime(2);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destroyAfterTime() 
    {
        yield return new WaitForSecondsRealtime(2);
        Destroy(gameObject);
    }
}

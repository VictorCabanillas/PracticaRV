using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCubeBehaviour : MonoBehaviour
{
    public int speed = 10;
    public Color color { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back*Time.deltaTime*speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FailZone"))
        {
                Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCubeBehaviour : MonoBehaviour,IPooledObject
{

    public int speed = 10;
    public bool Active { get { return gameObject.activeSelf; } set {gameObject.SetActive(value); } }
    public IPool pool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(Vector3.back*Time.fixedDeltaTime*speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Saber"))
        {
            pool?.Release(this);
        }
        if (other.CompareTag("FailZone"))
        {
            pool?.Release(this);
        }
    }

    public IPooledObject Clone()
    {
        GameObject newObject = Instantiate(gameObject);
        BeatCubeBehaviour beatCube = newObject.GetComponent<BeatCubeBehaviour>();
        return beatCube;
    }

    public void Reset()
    {
        transform.position = new Vector3(20,0,0);
    }
}

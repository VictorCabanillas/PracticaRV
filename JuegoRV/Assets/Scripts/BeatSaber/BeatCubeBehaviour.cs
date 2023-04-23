using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCubeBehaviour : MonoBehaviour,IPooledObject
{

    public float speed = 10;
    public bool Active { get { return gameObject.activeSelf; } set {gameObject.SetActive(value); } }
    public IPool pool;

    ComboBehaviour combo;

    // Start is called before the first frame update
    void Start()
    {
        combo = GameObject.FindGameObjectWithTag("combo").GetComponent<ComboBehaviour>();
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
            combo.addCombo();
            pool?.Release(this);
        }
        if (other.CompareTag("FailZone"))
        {
            combo.resetCombo();
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

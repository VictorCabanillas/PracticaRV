using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class BeatCubeBehaviour : MonoBehaviour,IPooledObject
{

    public float speed = 10;
    public float cutForce = 2000;
    public bool Active { get { return gameObject.activeSelf; } set {gameObject.SetActive(value); } }
    public IPool pool;

    ComboBehaviour combo;

    Vector3 endSlicePoint;
    Vector3 startSlicePoint;
    private VelocityEstimator velocityEstimator;

    public Material redMaterial;
    public Material blueMaterial;

    private XRDirectInteractor leftControler;
    private XRDirectInteractor rightControler;

    // Start is called before the first frame update
    void Start()
    {
        combo = GameObject.FindGameObjectWithTag("combo").GetComponent<ComboBehaviour>();

        leftControler = GameObject.FindGameObjectWithTag("leftControler").GetComponent<XRDirectInteractor>();
        rightControler = GameObject.FindGameObjectWithTag("rightControler").GetComponent<XRDirectInteractor>();
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
            startSlicePoint = other.GetComponent<SaberInfo>().startPoint.position;
            Debug.Log(startSlicePoint);
            //endSlicePoint = (-other.transform.right)*10;
            velocityEstimator = other.GetComponent<VelocityEstimator>();
        }
        if (other.CompareTag("FailZone"))
        {
            combo.resetCombo();
            pool?.Release(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Saber"))
        {
            endSlicePoint = other.GetComponent<SaberInfo>().endPoint.position;
            combo.addCombo();
            Slice(gameObject);
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

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint - startSlicePoint, velocity);
        planeNormal.Normalize();

        Material material = gameObject.GetComponent<Renderer>().material.color ==Color.red ? redMaterial : blueMaterial;
        XRBaseControllerInteractor interactor = gameObject.GetComponent<Renderer>().material.color == Color.red ? leftControler : rightControler;

        SlicedHull hull = target.Slice(endSlicePoint, planeNormal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target,material);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target,material);
            SetupSlicedComponent(lowerHull);

            interactor.SendHapticImpulse(0.5f, 0.5f);
            pool?.Release(this);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject) 
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        slicedObject.AddComponent<HullBehaviour>();
        slicedObject.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}

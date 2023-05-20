using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class BeatCubeBehaviour : MonoBehaviour,IPooledObject
{
    //Creacion de variables
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
        //Asignacion de objetos
        combo = GameObject.FindGameObjectWithTag("combo").GetComponent<ComboBehaviour>();
        leftControler = GameObject.FindGameObjectWithTag("leftControler").GetComponent<XRDirectInteractor>();
        rightControler = GameObject.FindGameObjectWithTag("rightControler").GetComponent<XRDirectInteractor>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Mover al cubo hacia el jugador
        transform.Translate(Vector3.back*Time.fixedDeltaTime*speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Si hay colision
        if (other.CompareTag("Saber"))
        {
            //Con un sable guardar la informacion del contacto
            startSlicePoint = other.GetComponent<SaberInfo>().startPoint.position;
            velocityEstimator = other.GetComponent<VelocityEstimator>();
        }
        if (other.CompareTag("FailZone"))
        {
            //Con el bloque de fallo reseta combo y devuelve a la pool el cubo
            combo.resetCombo();
            pool?.Release(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Al salir de la colision si la colision era con el sable cortar el cubo y añadir puntuacion
        if (other.CompareTag("Saber"))
        {
            endSlicePoint = other.GetComponent<SaberInfo>().endPoint.position;
            combo.addCombo();
            Slice(gameObject);
        }
    }

    public IPooledObject Clone()
    {
        //Clonacion de cubos para la pool
        GameObject newObject = Instantiate(gameObject);
        BeatCubeBehaviour beatCube = newObject.GetComponent<BeatCubeBehaviour>();
        return beatCube;
    }

    public void Reset()
    {
        //Reset de cubos para la pool
        transform.position = new Vector3(20,0,0);
    }

    public void Slice(GameObject target)
    {
        //Crea un plano de corte con los datos obtenidos al iniciar y finalizar el contacto con el cubo
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint - startSlicePoint, velocity);
        planeNormal.Normalize();

        Material material = gameObject.GetComponent<Renderer>().material.color ==Color.red ? redMaterial : blueMaterial;
        XRBaseControllerInteractor interactor = gameObject.GetComponent<Renderer>().material.color == Color.red ? leftControler : rightControler;

        //Corta el objeto
        SlicedHull hull = target.Slice(endSlicePoint, planeNormal);

        if (hull != null)
        {
            //Separa el cubo en 2 en base al corte
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
        //Creacion de las mitades con un sus componentes necesarios y uno extra para que se destruyan pasado unos segundos
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        slicedObject.AddComponent<HullBehaviour>();
        slicedObject.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
        collider.convex = true;
        //Aplicamos una fuerza de explosion para que las mitades salgan despedidas en distinta direccion
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}

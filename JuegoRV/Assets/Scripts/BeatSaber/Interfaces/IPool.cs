using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool
{
    //Interfaz IPool
    public IPooledObject Get();
    public void Release(IPooledObject obj);
}

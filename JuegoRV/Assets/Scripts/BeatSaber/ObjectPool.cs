using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : IPool
{
    //Patron object pool
    private IPooledObject _objectPrototype;
    private readonly bool _allowAddNew;
    private List<IPooledObject> _objects;
    private int _activeObjects;


    //Creador del object pool
    public ObjectPool(IPooledObject objectPrototype, int initialNumberOfElements, bool allowAddNew)
    {
        _objectPrototype = objectPrototype;
        _allowAddNew = allowAddNew;
        _objects = new List<IPooledObject>(initialNumberOfElements);
        _activeObjects = 0;

        for (int i = 0; i < initialNumberOfElements; i++)
        {
            _objects.Add(CreateObject());
        }
    }

    //Devuelve un objeto libre de la object pool en caso de no haber si se permite se añaden nuevos
    public IPooledObject Get()
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            if (!_objects[i].Active)
            {
                _objects[i].Active = true;
                _activeObjects += 1;
                return _objects[i];
            }
        }

        if (_allowAddNew)
        {
            IPooledObject newObj = CreateObject();
            newObj.Active = true;
            _objects.Add(newObj);

            _activeObjects += 1;
            return newObj;
        }

        return null;
    }

    //Retorna un objeto a la object pool
    public void Release(IPooledObject obj)
    {
        obj.Active = false;
        _activeObjects -= 1;
        obj.Reset();
    }

    //Crea objetos para la object pool
    private IPooledObject CreateObject()
    {
        IPooledObject newObj = _objectPrototype.Clone();
        newObj.Active = false;
        return newObj;
    }

    //Getter
    public int GetCount()
    {
        return _objects.Count;
    }

    public int GetActive()
    {
        return _activeObjects;
    }
}

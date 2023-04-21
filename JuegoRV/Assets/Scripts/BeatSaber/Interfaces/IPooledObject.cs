using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    public bool Active
    {
            get;
            set;
    }

    public IPooledObject Clone();
    public void Reset();
    }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScriptableObject<T> : ScriptableObject
{
    [SerializeField]
    public List<T> _data;
}

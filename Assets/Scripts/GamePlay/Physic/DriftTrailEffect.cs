using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftTrailEffect : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer tr;

    public DriftCheck dc;

    private bool _isActive = false;
    
    private void Start()
    {
        _isActive = dc != null;
        if(!_isActive) Debug.LogError("DriftCheck is null" , gameObject);
    }

    public void Update()
    {
        if (!_isActive) return;
        tr.emitting = dc.isDrifting;
    }
}

using System;
using UnityEngine;


public class DAxeView : MonoBehaviour
{
    private Animator _animator;


    private void Awake()
    {
        BakeReferences();
    }

    void BakeReferences()
    {
        _animator = GetComponent<Animator>();
    }
    
    
}

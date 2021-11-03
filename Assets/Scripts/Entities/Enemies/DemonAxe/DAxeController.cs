using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAxeController : MonoBehaviour
{

    private DAxeModel _d;

    private FSM<EnemyStatesEnum> _fsm;
    
    // Start is called before the first frame update
    private void Awake()
    {
        BakeReferences();
    }

    void BakeReferences()
    {
        _d = GetComponent<DAxeModel>();
        
    }

    void Start()
    {
        InitFSM();
    }

    void InitFSM()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{

    public Camera cam;
    private Transform _transform;
    private Vector2 startPos;
    
    public Transform subject;
    
    private float _startZ;
    private float _startY;

    private Vector2 Travel => (Vector2)cam.transform.position - startPos;

    private float DistFromSubject => _transform.position.z - subject.position.z;

    private float ClippingPlane =>
        (cam.transform.position.z + (DistFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
  
    private float ParallaxFactor => Mathf.Abs(DistFromSubject) / ClippingPlane;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        _startZ = _transform.position.z;    
        _startY = _transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = startPos + Travel * ParallaxFactor;
        _transform.position = new Vector3(newPos.x, _startY, _startZ);
    }
}

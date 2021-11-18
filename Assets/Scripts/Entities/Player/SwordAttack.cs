using System;
using TMPro.EditorUtilities;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private PlayerController _controller;
    private Collider _collider;
    private bool _active = false;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        SubscribeEvents();
        _collider.gameObject.SetActive(_active);
    }

    private void SubscribeEvents()
    {
        _controller.OnAttack += SwingSword;
    }

    private void SwingSword(int dmg)
    {
        _active = !_active;
        _collider.gameObject.SetActive(_active);
    }
    private void OnCollisionEnter(Collision other)
    {
        
        
    }
}


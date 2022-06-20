using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        _backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
     
        
    }
}

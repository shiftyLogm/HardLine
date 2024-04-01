using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private AudioSource musicMenu;
    void Start()
    { 
        musicMenu = GetComponent<AudioSource>();
        musicMenu.volume = 1;
        musicMenu.loop = true;
        musicMenu.playOnAwake = true;
        musicMenu.pitch = .85f;
    }

    void Update()
    {
        
    }
}

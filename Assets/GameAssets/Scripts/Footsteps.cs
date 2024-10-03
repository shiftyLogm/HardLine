using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private GameObject _player;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_player == null) return;

        if(_player.GetComponent<PlayerController>().mov != new Vector2(0, 0)) _audioSource.mute = false;
        else _audioSource.mute = true;
    }
}


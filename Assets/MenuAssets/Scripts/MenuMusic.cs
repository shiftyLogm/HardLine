using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private AudioSource musicMenu;
    void Start()
    { 
        musicMenu = GetComponent<AudioSource>();
        musicMenu.volume = 0;
        musicMenu.loop = true;
        musicMenu.playOnAwake = true;
        musicMenu.pitch = .85f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraStart : MonoBehaviour
{

    public CinemachineVirtualCamera cm;

    // Start is called before the first frame update
    void Start()
    {
        cm.Follow = PlayerClassesController.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

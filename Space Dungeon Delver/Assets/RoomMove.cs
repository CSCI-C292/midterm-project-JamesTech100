using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomMove : MonoBehaviour
{
    [SerializeField] Camera curRoomCam;
    [SerializeField] Camera rightRoomCam;

    private void Start()
    {
        curRoomCam.enabled = true;
        rightRoomCam.enabled = false;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter()
    {
        Debug.Log("it worked");
        curRoomCam.enabled = false;
        rightRoomCam.enabled = true;
    }
}

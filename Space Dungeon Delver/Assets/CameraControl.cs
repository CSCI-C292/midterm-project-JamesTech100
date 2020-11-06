using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public static CameraControl instance;
    public Room currRoom;

    public float cameraSpeedOnRoomChange;

    private void Awake()
    {
        instance = this;
    }

    
    void Update()
    {
        updatePos();
    }
    
    void updatePos()
    {
        if(currRoom == null)
        {
            return;
        }

        Vector3 targetPos = getCamTargetPos();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * cameraSpeedOnRoomChange);
    }

    Vector3 getCamTargetPos()
    {
        if(currRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = currRoom.GetRoomCenter();
        targetPos.z = transform.position.z;

        return targetPos;
    }
    
    public bool IsSwitchingScene()
    {
        return transform.position.Equals(getCamTargetPos()) == false;
    }
}

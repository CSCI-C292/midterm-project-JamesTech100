using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    [SerializeField] public Wall noDoorWall;
    [SerializeField] public int doorEnterDistance;
    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorType.Equals(DoorType.right)){
            collision.transform.position += new Vector3(0 + doorEnterDistance, 0, 0);
        }
        if (doorType.Equals(DoorType.left))
        {
            collision.transform.position += new Vector3(0 - doorEnterDistance, 0, 0);
        }
        if (doorType.Equals(DoorType.top))
        {
            collision.transform.position += new Vector3(0, 0 + doorEnterDistance, 0);
        }
        if (doorType.Equals(DoorType.bottom))
        {
            collision.transform.position += new Vector3(0, 0 - doorEnterDistance, 0);
        }

    }
}

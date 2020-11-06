using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public enum WallType
    {
        left, right, top, bottom
    }

    public WallType wallType;
    private BoxCollider2D selfBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        
    }
}

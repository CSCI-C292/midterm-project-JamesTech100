using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.UI;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int Width;

    public int Height;

    public int X;

    public int Y;

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;
    public List<Door> doors = new List<Door>();

    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Enemy5;
    public GameObject Enemy6;

    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("Played in wrong area");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }
        

        RoomController.instance.RegisterRoom(this);

    }

    public void RemoveUnconnectedDoors()
    {
        foreach(Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    if(GetRight() == null)
                    {
                        door.gameObject.SetActive(false);
                        door.noDoorWall.gameObject.SetActive(true);
                    }
                    else
                    {
                        RoomController.instance.FindRoom(X + 1, Y).leftDoor.noDoorWall.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                    {
                        door.gameObject.SetActive(false);
                        door.noDoorWall.gameObject.SetActive(true);
                    }
                    else
                    {
                        RoomController.instance.FindRoom(X - 1, Y).rightDoor.noDoorWall.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                    {
                        door.gameObject.SetActive(false);
                        door.noDoorWall.gameObject.SetActive(true);
                    }
                    else
                    {
                        RoomController.instance.FindRoom(X, Y + 1).bottomDoor.noDoorWall.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                    {
                        door.gameObject.SetActive(false);
                        door.noDoorWall.gameObject.SetActive(true);
                    }
                    else
                    {
                        RoomController.instance.FindRoom(X, Y - 1).topDoor.noDoorWall.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }

    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }

    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }

    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * Width, Y * Height);
    }

    void SpawnRandomEnemies()
    {
        int enemyNum = Random.Range(0, 6);
        if (enemyNum >= 1)
            Enemy1.SetActive(true);
        if (enemyNum >= 2)
            Enemy2.SetActive(true);
        if (enemyNum >= 3)
            Enemy3.SetActive(true);
        if (enemyNum >= 4)
            Enemy4.SetActive(true);
        if (enemyNum >= 5)
            Enemy5.SetActive(true);
        if (enemyNum >= 6)
            Enemy6.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
            //SpawnRandomEnemies();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInformation
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentFloorName = "Floor 1"; 

    RoomInformation currentLoadRoomData;

    Room currRoom;

    Queue<RoomInformation> loadRoomQueue = new Queue<RoomInformation>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadRoom("Start", 0, 0);
        LoadRoom("Empty", 1, 0);
        LoadRoom("Empty", 1, -1);
        LoadRoom("Empty", 1, 1);
        LoadRoom("Empty", 2, 1);
        LoadRoom("Empty", 2, -1);
        LoadRoom("Empty", 3, -1);
        LoadRoom("Empty", 3, 1);
        LoadRoom("Empty", 3, 0);
        LoadRoom("Empty", 4, 0);
        LoadRoom("Empty", 5, 0);
        LoadRoom("Empty", 5, 1);
        LoadRoom("Empty", 5, -1);
        LoadRoom("End", 6, -1);
    }

    void Update()
    {
        updateRoomQueue();
    }

    void updateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if(loadRoomQueue.Count == 0)
        {
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }

        RoomInformation newRoomData = new RoomInformation();
        newRoomData.name = name; 
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInformation info)
    {
        string roomName = currentFloorName + " " + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room) // sets room in the scene
    {
        if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        { 
        room.transform.position = new Vector3
        (
            currentLoadRoomData.X * room.Width,
            currentLoadRoomData.Y * room.Height,
            0
        );

        room.X = currentLoadRoomData.X;
        room.Y = currentLoadRoomData.Y;
        room.name = currentFloorName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
        room.transform.parent = transform;

        isLoadingRoom = false;

        if(loadedRooms.Count == 0)
        {
            CameraControl.instance.currRoom = room;
        }

            loadedRooms.Add(room);
            room.RemoveUnconnectedDoors();
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public void OnPlayerEnterRoom(Room room)
    {
        //Debug.Log("room entered");
        CameraControl.instance.currRoom = room;
        currRoom = room;
        //room.DoubleCheckWalls();
    }
}

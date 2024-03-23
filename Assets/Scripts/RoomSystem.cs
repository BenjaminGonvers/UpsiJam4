using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int _evenement;

    bool isDestroy = false;
    bool hasEvent = false;

    public bool GetHasEvent() { return hasEvent; }
    public bool IsDestroy() { return isDestroy; }

    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }


    void SetEvent(int evenement)
    {
        this._evenement = evenement;
        this.GetComponent<SpriteRenderer>().color = Color.red;
        this.hasEvent = true;
    }

    void ClearEvent()
    {
        this._evenement = -1;
        this.GetComponent<SpriteRenderer>().color = Color.white;
        this.hasEvent = false;
    }

    void Destroy()
    {
        this.isDestroy = true;
        this.GetComponent<SpriteRenderer>().color = Color.black;
    }

}
public class RoomSystem : MonoBehaviour
{
    List<Room> rooms;
    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<Room>();
        GameObject[] items = GameObject.FindGameObjectsWithTag("Room");

        foreach (var item in items)
        {
            item.AddComponent<Room>();
            //rooms.Add(new Room(item));
        }
    }

    // Update is called once per frame
    public Room GetRoom(int id)
    {
        return this.rooms[id];
    }

    public Room GetRandomRoom()
    {
        List<Room> roomsTmp = rooms;
        Room room = this.rooms[UnityEngine.Random.Range(0, rooms.Count)];
        if (room.GetHasEvent())
        {
            roomsTmp.Remove(room);
            room = GetRandomRoom(rooms);
        }
        return this.rooms.First();
    }

    private Room GetRandomRoom(List<Room> rooms)
    {
        Room room = this.rooms[UnityEngine.Random.Range(0, rooms.Count)];
        if (room.GetHasEvent())
        {
            rooms.Remove(room);
            room = GetRandomRoom();
        }
        return this.rooms.First();
    }
}

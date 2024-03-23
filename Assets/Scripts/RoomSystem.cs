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
    public Evenement _evenement;

    bool isDestroy = false;
    bool hasEvent = false;
    
    //Ressource
    private RessourceSystem.ResourceType _typeResource;
    private int _quantity;

    public bool GetHasEvent() { return hasEvent; }
    public bool IsDestroy() { return isDestroy; }

    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }


    void SetEvent(Evenement evenement)
    {
        this._evenement = evenement;
        this.GetComponent<SpriteRenderer>().color = Color.red;
        this.hasEvent = true;
    }

    void SetConfining()
    {
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    void ClearEvent()
    {
        this._evenement = null;
        this.GetComponent<SpriteRenderer>().color = Color.white;
        this.hasEvent = false;
    }

    void Destroy()
    {
        this.isDestroy = true;
        this.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void GiveResource(RessourceSystem.ResourceType type, int quantity)
    {
        string resourceType = "Quantity of entity of ";
        switch (type)
        {
            case RessourceSystem.ResourceType.Swat:
                resourceType += "Swat";
                break;
            case RessourceSystem.ResourceType.Firefighter:
                resourceType += "Firefighter";
                break;
            case RessourceSystem.ResourceType.Police:
                resourceType += "Police";
                break;
        }
        Debug.Log(resourceType + " send: " + quantity);
        this._typeResource = type;
        this._quantity = quantity;

        if (this._evenement != null)
            this._evenement.AddUnitToEvent((int)_typeResource, quantity);
    }


    void Update()
    {
        if (this._evenement != null)
        {
            if (_evenement.eventIsAlive)
            {
                switch (_evenement.GetEventState())
                {
                    case EventState.EventConfining:
                        this.SetConfining();
                        break;
                    case EventState.EventBreach:
                        this.Destroy();
                        break;
                }
            }
        }
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
        Room room = this.rooms[UnityEngine.Random.Range(0, rooms.Count-1)];
        if (room.GetHasEvent())
        {
            roomsTmp.Remove(room);
            room = GetRandomRoom(rooms);
        }
        return this.rooms.First();
    }

    private Room GetRandomRoom(List<Room> rooms)
    {
        Room room = this.rooms[UnityEngine.Random.Range(0, rooms.Count-1)];
        if (room.GetHasEvent())
        {
            rooms.Remove(room);
            room = GetRandomRoom();
        }
        return this.rooms.First();
    }
}

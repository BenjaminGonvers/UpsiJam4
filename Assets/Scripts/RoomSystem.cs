using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;

public class Room : MonoBehaviour
{
    List<int> quantities;
    List<RessourceSystem.ResourceType>  resourcesTypes;

    private RoomSystem _system;
    private GameObject _logoEvent;
    private Evenement _evenement;



    private bool isDestroy = false;
    private bool hasEvent = false;

    public void SetSystem(RoomSystem system)
    {
        this._system = system;
    }

    //Ressource
    private RessourceSystem.ResourceType _typeResource;
    private int _quantity;

    public bool GetHasEvent() { return hasEvent; }
    public bool IsDestroy() { return isDestroy; }

    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;

        this.quantities = new List<int>();
        this.resourcesTypes = new List<RessourceSystem.ResourceType>();
    }

    public void SetEvenement(Evenement evenement)
    {
        this._evenement = evenement;
        this.hasEvent = true;

        _logoEvent = Instantiate(this._system.GetPrefabLogo(evenement.eventType), this.gameObject.transform, false);
    }

    void EventBreaching()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    void EventConfining()
    {
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    void SetDead()
    {
        this.isDestroy = true;
        this.GetComponent<SpriteRenderer>().color = Color.grey;
        Destroy(_logoEvent);
        _logoEvent = Instantiate(this._system.GetPrefabLabotaryBroken(), this.gameObject.transform, false);
    }


    void ClearEvent()
    {
        this._evenement = null;
        this.GetComponent<SpriteRenderer>().color = Color.white;
        this.hasEvent = false;
        Destroy(_logoEvent);
        _logoEvent = null;

        RessourceSystem resourceSystem = this._system.GetResourceSystem();
        //Give Resource
        for (int i = 0; i < quantities.Count; i++)
        {
            switch (resourcesTypes[i])
            {
                case RessourceSystem.ResourceType.Police:
                    resourceSystem.AddResourcePolice(quantities[i]);
                    break;
                case RessourceSystem.ResourceType.Firefighter:
                    resourceSystem.AddResourceFirefighter(quantities[i]);
                    break;
                case RessourceSystem.ResourceType.Swat:
                    resourceSystem.AddResourceSwat(quantities[i]);
                    break;
            }  
        }

        this.quantities.Clear();
        this.resourcesTypes.Clear();
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
        
        this.quantities.Add(quantity);
        this.resourcesTypes.Add(type);

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
                        this.EventConfining();
                        break;
                    case EventState.EventBreach:
                        this.EventBreaching();
                        break;
                }
            }

            if (!_evenement.eventIsAlive)
            {
                switch (_evenement.GetEventState())
                {
                    case EventState.EventConfining:
                        this.ClearEvent();
                        break;
                    case EventState.EventBreach:
                        this.SetDead();
                        break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (this._evenement != null)
        {
            this._evenement.EvenementUpdate();
        }
    }

    public bool CanReceiveResource()
    {
        return this._evenement != null && this._evenement.eventIsAlive;
    }
} 
public class RoomSystem : MonoBehaviour
{
    [SerializeField] private GameObject _prefab_logo_manifestion;
    [SerializeField] private GameObject _prefab_logo_biological;
    [SerializeField] private GameObject _prefab_logo_fire;
    [SerializeField] private GameObject _prefab_logo_labotaryBroken;

    List<Room> rooms;

    private RessourceSystem _resourceSystem;
    // Start is called before the first frame update
    void Start()
    {
        _resourceSystem = GameObject.Find("RessourceSystem").GetComponent<RessourceSystem>();
        rooms = new List<Room>();
        GameObject[] items = GameObject.FindGameObjectsWithTag("Room");

        foreach (var item in items)
        {
            item.AddComponent<Room>();
            item.GetComponent<Room>().SetSystem(this);
            rooms.Add(item.GetComponent<Room>());
        }
    }

    public RessourceSystem GetResourceSystem()
    {
        return this._resourceSystem;
    }

    public int RoomNumber()
    {
        return rooms.Count();
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
        return room;
    }

    private Room GetRandomRoom(List<Room> rooms)
    {
        Room room = this.rooms[UnityEngine.Random.Range(0, rooms.Count)];
        if (room.GetHasEvent())
        {
            rooms.Remove(room);
            room = GetRandomRoom();
        }
        return room;
    }

    public GameObject GetPrefabLabotaryBroken()
    {
        return _prefab_logo_labotaryBroken;
    }
    public GameObject GetPrefabLogo(EventList eventType)
    {
        GameObject prefab = null;
        switch (eventType)
        {
            case EventList.EventPolice:
                prefab = _prefab_logo_manifestion;
                break;
            case EventList.EventSwat:
                prefab = _prefab_logo_biological;
                break;
            case EventList.EventFireFighter:
                prefab = _prefab_logo_fire;
                break;
        }
        return prefab;
    }
}

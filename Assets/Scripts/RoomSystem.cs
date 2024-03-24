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
using static UnityEditor.Progress;

using TMPro;

public class Room : MonoBehaviour
{

 

    List<int> quantities;
    List<RessourceSystem.ResourceType>  resourcesTypes;

    private RoomSystem _system;
    private GameObject _logoEvent;
    private Evenement _evenement;

    private int _id;
    UIBar _bars;

    public void SetID(int id) {  this._id = id; }


    private bool isDestroy = false;
    private bool hasEvent = false;

    public bool GetIsDestroy()
    {
        return isDestroy;
    }
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
        _bars = GameObject.Find("Canvas_Bar").GetComponent<UIBar>();
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
        this.GetComponent<SpriteRenderer>().color = Color.green;
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
        if (!_system.GetPause())
        {
            if (!_system.GetIsFinish())
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
        }
    }

    private void FixedUpdate()
    {
        if (!_system.GetPause())
        {
            if(!_system.GetIsFinish())
            {
                if (this._evenement != null)
                {
                    this._evenement.EvenementUpdate();

                    float time = this._evenement.GetEventActualTimer() / this._evenement.GetEventMaxTime();
                    if (_bars != null)
                        _bars.SetBar(this._id, time);
                }
            }
        }
    }

    public bool CanReceiveResource()
    {
        return this._evenement != null && this._evenement.eventIsAlive;
    }

    void OnMouseOver()
        {
            float PourCent = 0.0f;
            if (_evenement != null && _evenement.eventIsAlive)
            {
                RessourceSystem resourceSystem = this._system.GetResourceSystem();

                if (resourceSystem.FirefighterTaken > 0)
                {
                    if (2 == (int) _evenement.eventType)
                    {
                        PourCent += resourceSystem.FirefighterTaken / _evenement.numberOfUnitNeeded;
                    }
                    else
                    {
                        PourCent += resourceSystem.FirefighterTaken * _evenement.badUnitModifier /
                                    _evenement.numberOfUnitNeeded;
                    }

                }
                else if (resourceSystem.PoliceTaken > 0)
                {
                    if (1 == (int) _evenement.eventType)
                    {
                        PourCent += resourceSystem.PoliceTaken / _evenement.numberOfUnitNeeded;
                    }
                    else
                    {
                        PourCent += resourceSystem.PoliceTaken * _evenement.badUnitModifier /
                                    _evenement.numberOfUnitNeeded;
                    }
                }
                else if (resourceSystem.SwatTaken > 0)
                {
                    if (0 == (int) _evenement.eventType)
                    {
                        PourCent += resourceSystem.SwatTaken / _evenement.numberOfUnitNeeded;
                    }
                    else
                    {
                        PourCent += resourceSystem.SwatTaken * _evenement.badUnitModifier /
                                    _evenement.numberOfUnitNeeded;
                    }
                }

                if (PourCent > 0.0f)
                {
                     GetComponentInChildren<TextMeshPro>().enabled = true;
                     GetComponentInChildren<TextMeshPro>().text = (PourCent * 100).ToString() + "%";
                }
                else
                {
                     GetComponentInChildren<TextMeshPro>().enabled = false;
                }
               
            }else
            {
                 GetComponentInChildren<TextMeshPro>().enabled = false;
            }
        }

    void OnMouseExit()
    {
         GetComponentInChildren<TextMeshPro>().enabled = false; 
    }
} 
public class RoomSystem : MonoBehaviour
{
    private bool _isFinish = false;
    public void SetIsFinish(bool isFinish)
    {
        this._isFinish = isFinish;
    }

    public bool GetIsFinish()
    {
        return this._isFinish;
    }

    [SerializeField] private GameObject _prefab_logo_manifestion;
    [SerializeField] private GameObject _prefab_logo_biological;
    [SerializeField] private GameObject _prefab_logo_fire;
    [SerializeField] private GameObject _prefab_logo_labotaryBroken;

    List<Room> rooms;

    private RessourceSystem _resourceSystem;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _resourceSystem = GameObject.Find("RessourceSystem").GetComponent<RessourceSystem>();
        rooms = new List<Room>();
        GameObject[] items = GameObject.FindGameObjectsWithTag("Room");

        for (int i = items.Count()-1; i > -1; i--)
        {
            items[i].AddComponent<Room>();
            items[i].GetComponent<Room>().SetSystem(this);
            items[i].GetComponent<Room>().SetID(rooms.Count);
            rooms.Add(items[i].GetComponent<Room>());
        }
    }

    public bool GetPause() {
        return _gameManager.GetPause();    
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

    public bool AllIsFinish()
    {
        bool isFinish = true;
        foreach (Room room in this.rooms)
        {
            if (room.GetHasEvent())
            {
                if(!room.IsDestroy())
                    isFinish = false;
            }
        }

        return isFinish;
    }

    public int NumberRoomIsAlive()
    {
        int counter = 0;
        foreach (Room room in this.rooms)
        {
            if (!room.GetIsDestroy())
            {
                counter++;
            }
        }

        return counter;
    }

    public List<Transform> GetListRoomIsAlive()
    {
        List<Room> roomsAlive = new List<Room>();
        foreach (Room room in this.rooms)
        {
            if (!room.GetIsDestroy())
                roomsAlive.Add(room);
        }
        
        List<Transform> list = new List<Transform>();
        foreach (Room room in roomsAlive)
        {
            list.Add(room.gameObject.transform);
        }

        return list;
    }
}

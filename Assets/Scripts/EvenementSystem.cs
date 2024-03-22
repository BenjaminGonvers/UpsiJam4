using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Rooms
{
    public bool _hasEvent = false;
    public int _eventID = 0;
    public float _eventMaxTime = 0.0f;  
}

public class RoomSystemProto
{
    public Rooms getRandomRoom()
    {
        return null;
    }
}

public class Evenement
{
    public float _eventMaxTime = 0.0f;
    public Evenement(float eventMaxTime)
    {
        _eventMaxTime = eventMaxTime;
    }
}

public class EvenementSystem : MonoBehaviour
{
    [SerializeField] private RoomSystemProto _roomSystem;

    [SerializeField] private int maxEvenements = 0;
    private int totalEvents = 0;

    [SerializeField] private float tickTime = 0.0f;
    private float tickTimer = 0.0f;

    private List<Rooms> _gameRooms;

    private List<float> _timers;

    Evenement _eventType1 = new Evenement(1.0f);



    void Update()
    {
        if (totalEvents < maxEvenements)
        {
            Rooms selectedRoom = _roomSystem.getRandomRoom();
            addEvents(selectedRoom);
        }
    }

    void addEvents(Rooms room)
    {
        AddEventTypetoRoom(room, _eventType1);
    }
    void AddEventTypetoRoom(Rooms room, Evenement evenement)
    {
        room._eventMaxTime = evenement._eventMaxTime; 
    }
}

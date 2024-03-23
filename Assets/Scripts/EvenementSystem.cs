using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;



public class Evenement
{
    public int eventID = 0;
    public float eventMaxTime = 0.0f;
    public Evenement(int eventid, float eventnaxtime)
    {
        eventID = eventid;
        eventMaxTime = eventnaxtime;
    }
}

public class EvenementSystem : MonoBehaviour
{
    [SerializeField] private RoomSystem _roomSystem;

    [SerializeField] private int maxEvenements = 0;
    private int totalEvents = 0;

    [SerializeField] private float tickTime = 0.0f;
    private float tickTimer = 0.0f;

    Evenement _eventType1 = new Evenement(1, 1.0f);

    private List<float> _timersList = new();

    [SerializeField] private int _gracePeriods = 0;
    [SerializeField] private float _graceTimeModifier = 0.0f;

    private float _timeTilNextEvent = 0.0f;

    void Update()
    {
        
        tickTimer += Time.deltaTime;
        if(tickTimer > tickTime) 
        { 
            tickTimer = 0.0f;
            if (totalEvents < maxEvenements)
            {
                // TODO ALGORYTHM TO RANDOMIZE if event spawns
                Room selectedRoom = _roomSystem.GetRandomRoom();
                addEvents(selectedRoom);
            }
        }
    }

    void addEvents(Room room)
    {
        AddEventTypetoRoom(room, _eventType1);
    }
    void AddEventTypetoRoom(Room room, Evenement evenement)
    {
        room._evenement = evenement.eventID; 
    }
}

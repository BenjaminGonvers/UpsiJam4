using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;


public enum EventList : int
{
    EventSwat = 0,
    EventPolice = 1,
    EventFireFighter = 2
}
public class Evenement
{
    public EventList eventType = 0;
    public float eventMaxTime = 0.0f;
    private float timer = 0.0f;

    public float numberOfUnitNeeded = 0.0f;
    public float unitOnSite = 0.0f;

    [SerializeField] public float badUnitModifier = 0.5f;
    public Evenement(EventList theirEventType)
    {
        eventType = theirEventType;
        if (theirEventType == EventList.EventSwat)
        {
            eventMaxTime = 1.0f;
        }
        if (theirEventType == EventList.EventPolice)
        {
            eventMaxTime = 2.0f;
        }
        if (theirEventType == EventList.EventFireFighter)
        {
            eventMaxTime = 3.0f;
        }
    }

    public void AddUnitToEvent(int unitType, int UnitNumber)
    {
        if(unitType == (int)eventType) 
        {
            unitOnSite += UnitNumber;
        }
        else
        {
            unitOnSite += UnitNumber * badUnitModifier;
        }
    }

}

public class EvenementSystem : MonoBehaviour
{
    [SerializeField] private RoomSystem _roomSystem;

    [SerializeField] private int maxEvenements = 0;
    private int totalEvents = 0;

    [SerializeField] private float tickTime = 0.0f;
    private float tickTimer = 0.0f;

    Evenement _eventType1 = new Evenement(EventList.EventFireFighter);

    private List<float> _timersList = new();

    [SerializeField] private int _gracePeriods = 0;
    [SerializeField] private float _graceTimeModifier = 0.0f;

    [SerializeField] private float _timeTilNextEvent = 0.0f;

    void Update()
    {

        tickTimer += Time.deltaTime;
        //Activate every tick
        if (tickTimer > tickTime)
        {
            tickTimer = 0.0f;
            //If max events in game, do nothing
            if (totalEvents < maxEvenements)
            {
                //if room for one event, check if one timer has ran out
                for (int i = 0; i < _timersList.Count; i++)
                {
                    if (_timersList[i] < 0)
                    {
                        Room selectedRoom = _roomSystem.GetRandomRoom();
                        addEvents(selectedRoom);
                        _timersList[i] = _graceTimeModifier + _timeTilNextEvent;
                    }
                }
            }
        }
    }

    void addEvents(Room room)
    {
        AddEventTypetoRoom(room, _eventType1);
    }
    void AddEventTypetoRoom(Room room, Evenement evenement)
    {
        //TODO
    }
}

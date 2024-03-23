using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEditor;
using UnityEngine;


public enum EventList : int
{
    EventSwat = 0,
    EventPolice = 1,
    EventFireFighter = 2
}

public enum EventState
{
    EventConfining = 0,
    EventBreach = 1
}

public class Evenement
{
    public bool eventIsAlive = true;
    public EventList eventType = 0;
    public EventState eventState = EventState.EventBreach;


    public float eventMaxTime = 100.0f;
    private float timer = 0.0f;

    public float numberOfUnitNeeded = 2.0f;
    public float unitOnSite = 0.0f;

    [SerializeField] public float badUnitModifier = 0.5f;

    public Evenement(EventList theirEventType)
    {
        eventType = theirEventType;
        if (theirEventType == EventList.EventSwat)
        {
            eventMaxTime = 5.0f;
        }
        if (theirEventType == EventList.EventPolice)
        {
            eventMaxTime = 20.0f;
        }
        if (theirEventType == EventList.EventFireFighter)
        {
            eventMaxTime = 30.0f;
        }
    }

    public void AddUnitToEvent(int unitType, int UnitNumber)
    {
        if (unitType == (int)eventType)
        {
            unitOnSite += UnitNumber;
        }
        else
        {
            unitOnSite += UnitNumber * badUnitModifier;
        }
    }

    public void EvenementUpdate()
    {
        //Check if this room is being resolved or not
        if (unitOnSite >= numberOfUnitNeeded)
        {
            eventState = EventState.EventConfining;
        }
        else
        {
            eventState = EventState.EventBreach;
        }

        //Increments or decrements the timer
        if (eventState == EventState.EventBreach)
        {
            timer += Time.deltaTime;
        }
        if (eventState == EventState.EventConfining)
        {
            timer -= Time.deltaTime;
        }

        //Check if event got resolved or breached containment
        if (timer < 0 || timer > eventMaxTime)
        {
            eventIsAlive = false;
        }
        if (eventIsAlive == false)
        {
        }
    }


    public bool IsAlive() { return eventIsAlive; }
    public EventState GetEventState() { return eventState; }
    public float GetEventMaxTime() { return eventMaxTime; }
    public float GetEventActualTimer() { return timer; }

}

public class EvenementSystem : MonoBehaviour
{
    [SerializeField] private RoomSystem _roomSystem;
    [SerializeField] List<float> _timersList;
    [SerializeField] private int maxEvenements = 0;
    private int totalEvents = 0;

    [SerializeField] private float tickTime = 0.0f;
    private float tickTimer = 0.0f;

    Evenement _eventType1 = new Evenement(EventList.EventFireFighter);

    [SerializeField] private int _gracePeriods = 0;
    [SerializeField] private float _graceTimeModifier = 0.0f;

    [SerializeField] private float _timeTilNextEvent = 0.0f;

    void Update()
    {
        Debug.Log(totalEvents);

        tickTimer += Time.deltaTime;
        //Activate every tick
        if (tickTimer > tickTime)
        {
            //If max events in game, do nothing
            if (totalEvents < maxEvenements)
            {
                //if room for one event, check if one timer has ran out
                for (int i = 0; i < _timersList.Count; i++)
                {
                    _timersList[i] -= tickTimer;
                    if (_timersList[i] <= 0)
                    {
                        Room myRoom = _roomSystem.GetRandomRoom();
                        if (myRoom.GetHasEvent() == false)
                        {

                            addEvents(_roomSystem.GetRandomRoom());
                            _timersList[i] = _graceTimeModifier + _timeTilNextEvent;
                        }
                    }
                }
                for (int i = 0; i < _timersList.Count; i++)
                {
                    if (_timersList[i] <= 0)
                    {
                        _timersList[i] += 50;
                    }
                }
            }
            //totalEvents = 0;
            //for (int i = 0; i < 10; i++)
            //{
            //    if (_roomSystem.GetRoom(i).GetHasEvent())
            //    {
            //        totalEvents++;
            //    }
            //}
            tickTimer = 0.0f;
        }
    }

    void DeleteEvent()
    {
        totalEvents--;
    }

    void addEvents(Room room)
    {
        AddEventTypetoRoom(room, new Evenement(EventList.EventPolice));
    }
    void AddEventTypetoRoom(Room room, Evenement evenement)
    {
        room.SetEvenement(evenement);
    }
}

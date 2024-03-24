using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{

    private RoomSystem _roomSystem;
    private RessourceSystem _resourceSystem;
    private EvenementSystem _evenementSystem;

    int number_day = 0;

    const float TURN_TIME = 30.0f;
    float counterTime = 0.0f;

    bool isPause = false;
    bool isFinish = false;

    public bool GetPause()
    {
        return isPause;
    }

    public bool GetFinish()
    {
        return isFinish;
    }
    // Start is called before the first frame update
    void Start()
    {
        _roomSystem = GameObject.Find("RoomSystem").GetComponent<RoomSystem>();
        _resourceSystem = GameObject.Find("RessourceSystem").GetComponent<RessourceSystem>();
        _evenementSystem = GameObject.Find("EvenementSystem").GetComponent<EvenementSystem>();
        nextTurn();
    }

    void nextTurn()
    {
        counterTime = TURN_TIME;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isPause)
        {
            if (!isFinish)
            {
                counterTime -= Time.deltaTime;
                if (counterTime <= 0.0f)
                {
                    // End Turn
                    isFinish = true;
                    _evenementSystem.SetIsFinish(true);
                }
            }
            else
            {
                if (_roomSystem.AllIsFinish())
                {
                    _evenementSystem.SetIsFinish(true);
                    _resourceSystem.SetIsFinish(true);
                    _roomSystem.SetIsFinish(true);
                }
                else
                {

                }
            }
        }
    }
}

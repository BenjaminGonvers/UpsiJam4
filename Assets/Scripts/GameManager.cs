using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{
    public RessourceSystem RessourceSystem_;

    int number_day = 0;

    const float TIME_FOR_ONE_DAY = 30.0f;
    float counterTime = 0.0f;

    bool isPause = false;
    public bool GetPause()
    {
        return isPause;
    }
    // Start is called before the first frame update
    void Start()
    {
        newDay();
    }

    void newDay()
    {
        counterTime = TIME_FOR_ONE_DAY;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isPause)
        {
            counterTime -= Time.deltaTime;
            if(counterTime <= 0.0f)
            {
                // End Day
                isPause = true;
            }
        }
    }
}

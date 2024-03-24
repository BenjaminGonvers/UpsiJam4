using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketSystem : MonoBehaviour
{
    public int SwatPrice = 20;
    public int PolicePrice = 20;
    public int FirefighterPrice = 20;

    private ScoreSystem _score;
    private RessourceSystem _resource;

    private void Start()
    {
        _resource = GameObject.Find("RessourceSystem").GetComponent<RessourceSystem>();
    }

    private void Update()
    {
        if(_score == null)
            _score = GameObject.Find("ScoreSystem(Clone)").GetComponent<ScoreSystem>();
    }

    public void BuySwat()
    {
        if (_score._credits >= SwatPrice)
        {
            _resource.RessourceSwat++;
            _score._credits -= SwatPrice;
            _score.LeaveCredit(SwatPrice);
        }
        else
        {
            Debug.Log("Not enough credit to hire Swat");
        }
    }

    public void SellSwat()
    {
        if (_resource.RessourceSwat > 0)
        {
            _resource.RessourceSwat--;
            _score._credits += SwatPrice;
            _score.GainCredit(SwatPrice);
        }
        else
        {
            Debug.Log("No Swat to dismiss");
        }
    }

    public void BuyPolice()
    {
        if (_score._credits >= PolicePrice)
        {
            _resource.RessourcePolice++;
            _score._credits -= PolicePrice;
            _score.LeaveCredit(PolicePrice);
        }
        else
        {
            Debug.Log("Not enough credit to hire Police");
        }
    }

    public void SellPolice()
    {
        if (_resource.RessourcePolice > 0)
        {
            _resource.RessourcePolice--;
            _score._credits += PolicePrice;
            _score.GainCredit(PolicePrice);
        }
        else
        {
            Debug.Log("No Police to dismiss");
        }
    }

    public void BuyFirefighter()
    {
        if (_score._credits >= FirefighterPrice)
        {
            _resource.RessourceFirefighter++;
            _score._credits -= FirefighterPrice;
            _score.LeaveCredit(FirefighterPrice);
        }
        else
        {
            Debug.Log("Not enough credit to hire Firefighter");
        }
    }

    public void SellFirefighter()
    {
        if (_resource.RessourceFirefighter > 0)
        {
            _resource.RessourceFirefighter--;
            _score._credits += FirefighterPrice;
            _score.GainCredit(FirefighterPrice);
        }
        else
        {
            Debug.Log("No Firefighter to dismiss");
        }
    }
}

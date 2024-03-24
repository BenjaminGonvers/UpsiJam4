using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketSystem : MonoBehaviour
{
    public int SwatPrice = 300;
    public int PolicePrice = 300;
    public int FirefighterPrice = 300;

    private ScoreSystem _score;
    private RessourceSystem _resource;
    private GameManager _gameManager;

    [SerializeField] private GameObject _textCredit;



    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _resource = GameObject.Find("RessourceSystem").GetComponent<RessourceSystem>();
    }

    private void Update()
    {
        if (_score == null)
            _score = GameObject.Find("ScoreSystem(Clone)").GetComponent<ScoreSystem>();
    }

    public void BuySwat()
    {
        if (_score._credits >= SwatPrice)
        {
            if (_resource.RessourceSwat < _resource.MaxRessourceSwat)
            {
                _resource.RessourceSwat++;
                _score._credits -= SwatPrice;
                _score.LeaveCredit(SwatPrice);
            }
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
            if (_resource.RessourcePolice < _resource.MaxRessourcePolice)
            {
                _resource.RessourcePolice++;
                _score._credits -= PolicePrice;
                _score.LeaveCredit(PolicePrice);
            }
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
            if (_resource.RessourceFirefighter < _resource.MaxRessourceFirefighter)
            {
                _resource.RessourceFirefighter++;
                _score._credits -= FirefighterPrice;
                _score.LeaveCredit(FirefighterPrice);
            }
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

    public void ShowMarket()
    {
        GetComponent<Canvas>().enabled = true;
        SetTextCredit(_score._credits);
    }

    public void HideMarket() {
        GetComponent<Canvas>().enabled = false;
    }

    public void SetTextCredit(int credits)
    {
        if(_textCredit != null)
        {
            _textCredit.GetComponent<TMP_Text>().text = "Credits: " + credits;
        }
    }

    public void EndPurchase()
    {
        _gameManager.nextTurn();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{

    private RoomSystem _roomSystem;
    private RessourceSystem _resourceSystem;
    private EvenementSystem _evenementSystem;

    [SerializeField] private GameObject _prefabScore;
    private ScoreSystem _score;

    private MarketSystem _market; 

    int number_turn = 0;

    const float TURN_TIME = 30.0f;
    float counterTime = 0.0f;

   private bool _isPause = false;
   bool isFinish = false;
    bool _isCalculate = false;

    public bool GetPause()
    {
        return _isPause;
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

        _score = Instantiate(_prefabScore, null).GetComponent<ScoreSystem>();
        _market = GameObject.Find("Market").GetComponent<MarketSystem>();
        nextTurn();
    }

    public void nextTurn()
    {
        _evenementSystem.SetIsFinish(false);
        _resourceSystem.SetIsFinish(false);
        _roomSystem.SetIsFinish(false);
        number_turn++;
        counterTime = TURN_TIME;

        _isCalculate = false;
        isFinish = false;
        _market.HideMarket();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!_isPause)
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


                    if (!_isCalculate)
                    {
                        _score.Calculate();
                        _isCalculate = true;
                    }

                    if (_score.hasResult && !_score._gameOver)
                    {
                        _market.ShowMarket();
                    }
                }
                
            }
        }
    }

    public void SetPause(bool ispause)
    {
        _isPause = ispause;
    }

    public void EndParty()
    {
        Destroy(_score.gameObject);
    }
}

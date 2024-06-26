using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int _maxTurn = 0;
    [SerializeField] public int _credits = 0;
    [SerializeField] private int _eventConfined = 0;
    [SerializeField] private int score = 0;

    private RoomSystem _roomSystem;
    private List<Transform> _roomsAlive;
    private GameManager _gameManager;

    private Animator _animator;

    bool isCalculate = false;
    bool inAnimation = false;
    public bool hasResult = false;
    public bool _gameOver = false;

    public GameObject GameOverCanvas;
    public GameObject GameOverScoreText;
    public void Calculate()
    {
        hasResult = false;
        isCalculate = true;
        _roomsAlive = _roomSystem.GetListRoomIsAlive();
        inAnimation = false;
        if (_roomsAlive.Count <= 5)
        {
            _gameOver = true;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _roomSystem = GameObject.Find("RoomSystem").GetComponent<RoomSystem>();
        _animator = GetComponent<Animator>();
    }

    public void GainCredit(int credits)
    {
        _credits += credits;
    }
    public void SpendCredit(int price)
    {
        _credits -= price;
    }

    public void TurnSurvive()
    {
        _maxTurn++;
    }

    public void EventConfined()
    {
        _eventConfined++;
    }

    private void Update()
    {
        if (isCalculate)
        {
            if (!inAnimation)
            {
                if (_roomsAlive.Count != 0)
                {
                    Transform room = _roomsAlive.First();
                    _credits += 50;
                    this.transform.position = room.position;
                    SetText("+ " + _credits);
                    _animator.Play("GainCredit");
                    _roomsAlive.Remove(room);
                    inAnimation = true;

                    score += 50;
                }
                else
                {
                    isCalculate = false;
                    SetText("");
                    hasResult = true;

                    if (_gameOver)
                    {
                        GameOver();
                    }
                }
                
            }
        }
    }

    public void SetText(string msg)
    {
        TextMeshPro text = GetComponentInChildren<TextMeshPro>();
        text.text = msg;
    }

    public void NextRoom()
    {
        inAnimation = false;
    }

    private void GameOver()
    {
        GameOverCanvas.GetComponent<Canvas>().enabled = true;
        GameOverScoreText.GetComponent<TextMeshProUGUI>().text = "Score : " + score.ToString();
        Debug.Log("Game Over with a score of " + score.ToString());
    }
}

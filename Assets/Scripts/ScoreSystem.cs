using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int _maxTurn = 0;
    [SerializeField] private int _credits = 0;
    [SerializeField] private int _eventConfined = 0;
    [SerializeField] private int score = 0;

    private RoomSystem _roomSystem;
    private List<Transform> _roomsAlive;

    private Animator _animator;

    bool isCalculate = false;
    bool inAnimation = false;
    public void Calculate()
    {
        isCalculate = true;
        _roomsAlive = _roomSystem.GetListRoomIsAlive();
        inAnimation = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        _roomSystem = GameObject.Find("RoomSystem").GetComponent<RoomSystem>();
        _animator = GetComponent<Animator>();
    }

    public void GainCredit(int credits)
    {
        _credits += credits;
    }
    public void LeaveCredit(int price)
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
                }
                else
                {
                    isCalculate = false;
                    SetText("");
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

}

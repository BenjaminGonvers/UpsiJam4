using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class RessourceSystem : MonoBehaviour
{
    private bool _isFinish = false;
    public void SetIsFinish(bool isFinish)
    {
        this._isFinish = isFinish;
    }

    public enum ResourceType
    {
        Swat,
        Police,
        Firefighter
    }

    public int RessourceSwat = 0;
    public int MaxRessourceSwat = 0;
    public int RessourcePolice = 0;
    public int MaxRessourcePolice = 0;
    public int RessourceFirefighter = 0;
    public int MaxRessourceFirefighter = 0;

    public int SwatTaken = 0;
    public int PoliceTaken = 0;
    public int FirefighterTaken = 0;

    public ResourceContainer SwatContainer;
    public ResourceContainer PoliceContainer;
    public ResourceContainer FirefighterContainer;
    public ResourceCursor ResourceCursor;

    private bool MouseRightDown = false;

    private GameManager _gameManager;
    public void AddResourceSwat(int ressource)
    {
        RessourceSwat += ressource;
        if (RessourceSwat > MaxRessourceSwat)
        {
            RessourceSwat = MaxRessourceSwat;
        }
    }

    public void AddResourcePolice(int ressource)
    {
        RessourcePolice += ressource;
        if (RessourcePolice > MaxRessourcePolice)
        {
            RessourcePolice = MaxRessourcePolice;
        }
    }

    public void AddResourceFirefighter(int ressource)
    {
        RessourceFirefighter += ressource;
        if (RessourceFirefighter > MaxRessourceFirefighter)
        {
            RessourceFirefighter = MaxRessourceFirefighter;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager.GetPause())
        {
            if (!_isFinish)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit.collider != null)
                    {
                        Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                        Room room = hit.collider.GetComponent<Room>();
                        if (room != null)
                        {
                            Debug.Log("Room found");
                            if (room.CanReceiveResource())
                            {
                                if (SwatTaken > 0)
                                {
                                    room.GiveResource(ResourceType.Swat, SwatTaken);
                                    RessourceSwat -= SwatTaken;
                                    SwatTaken = 0;
                                }
                                else if (PoliceTaken > 0)
                                {
                                    room.GiveResource(ResourceType.Police, PoliceTaken);
                                    RessourcePolice -= PoliceTaken;
                                    PoliceTaken = 0;
                                }
                                else if (FirefighterTaken > 0)
                                {
                                    room.GiveResource(ResourceType.Firefighter, FirefighterTaken);
                                    RessourceFirefighter -= FirefighterTaken;
                                    FirefighterTaken = 0;
                                }
                            }
                        }
                    }
                    MouseRightDown = false;
                }

                if (SwatContainer.IsTaken && SwatTaken == 0 && RessourceSwat > 0)
                {
                    SwatTaken = 1;
                    ResourceCursor.IsVisible = true;

                }
                else if (!SwatContainer.IsTaken)
                {
                    SwatTaken = 0;
                }

                if (PoliceContainer.IsTaken && PoliceTaken == 0 && RessourcePolice > 0)
                {
                    PoliceTaken = 1;
                    ResourceCursor.IsVisible = true;
                }
                else if (!PoliceContainer.IsTaken)
                {
                    PoliceTaken = 0;
                }

                if (FirefighterContainer.IsTaken && FirefighterTaken == 0 && RessourceFirefighter > 0)
                {
                    FirefighterTaken = 1;
                    ResourceCursor.IsVisible = true;
                }
                else if (!FirefighterContainer.IsTaken)
                {
                    FirefighterTaken = 0;
                }

                if (PoliceTaken == 0 && SwatTaken == 0 && FirefighterTaken == 0)
                {
                    ResourceCursor.IsVisible = false;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (SwatTaken > 0 && SwatTaken < RessourceSwat)
                    {
                        SwatTaken++;
                    }

                    if (PoliceTaken > 0 && PoliceTaken < RessourcePolice)
                    {
                        PoliceTaken++;
                    }

                    if (FirefighterTaken > 0 && FirefighterTaken < RessourceFirefighter)
                    {
                        FirefighterTaken++;
                    }
                }                
            }

            SwatContainer.VisibleRessource = RessourceSwat - SwatTaken;
            PoliceContainer.VisibleRessource = RessourcePolice - PoliceTaken;
            FirefighterContainer.VisibleRessource = RessourceFirefighter - FirefighterTaken;
        }
    }
}

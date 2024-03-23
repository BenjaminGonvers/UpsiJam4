using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class RessourceSystem : MonoBehaviour
{


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
    

    public void AddRessourceSwat(int ressource)
    {
        RessourceSwat += ressource;
        if (RessourceSwat > MaxRessourceSwat)
        {
            RessourceSwat = MaxRessourceSwat;
        }
    }

    public void AddRessourcePolice(int ressource)
    {
        RessourcePolice += ressource;
        if (RessourcePolice > MaxRessourcePolice)
        {
            RessourcePolice = MaxRessourcePolice;
        }
    }

    public void AddRessourceFirefighter(int ressource)
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SwatContainer.IsTaken && SwatTaken == 0)
        {
            SwatTaken = 1;
            ResourceCursor.IsVisible = true;

        }else if(!SwatContainer.IsTaken)
        {
            SwatTaken = 0;
        }

        if (PoliceContainer.IsTaken && PoliceTaken == 0)
        {
            PoliceTaken = 1;
            ResourceCursor.IsVisible = true;
        }else if(!PoliceContainer.IsTaken)
        {
            PoliceTaken = 0;
        }

        if (FirefighterContainer.IsTaken && FirefighterTaken == 0)
        {
            FirefighterTaken = 1;
            ResourceCursor.IsVisible = true;
        }
        else if(!FirefighterContainer.IsTaken)
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
                SwatTaken ++;
            }

            if (PoliceTaken > 0 && PoliceTaken < RessourcePolice)
            {
                PoliceTaken ++;
            }

            if (FirefighterTaken > 0 && FirefighterTaken < RessourceFirefighter)
            {
                FirefighterTaken ++;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            MouseRightDown = true;
        }else if (MouseRightDown)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Room room = hit.collider.GetComponent<Room>();
                if (room != null)
                {
                    room.AddRessource(VisibleRessource);
                }
            }
            MouseRightDown = false;
        }else
        {
           
        }

        SwatContainer.VisibleRessource = RessourceSwat - SwatTaken;
        PoliceContainer.VisibleRessource = RessourcePolice - PoliceTaken;
        FirefighterContainer.VisibleRessource = RessourceFirefighter - FirefighterTaken;
    }
}

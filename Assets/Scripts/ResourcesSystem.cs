using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
public class RessourceSystem : MonoBehaviour
{
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
        }else if(!SwatContainer.IsTaken)
        {
            SwatTaken = 0;
        }

        if (PoliceContainer.IsTaken && PoliceTaken == 0)
        {
            PoliceTaken = 1;
        }else if(!PoliceContainer.IsTaken)
        {
            PoliceTaken = 0;
        }

        if (FirefighterContainer.IsTaken && FirefighterTaken == 0)
        {
            FirefighterTaken = 1;
        }
        else if(!FirefighterContainer.IsTaken)
        {
            FirefighterTaken = 0;
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

        

        SwatContainer.VisibleRessource = RessourceSwat - SwatTaken;
        PoliceContainer.VisibleRessource = RessourcePolice - PoliceTaken;
        FirefighterContainer.VisibleRessource = RessourceFirefighter - FirefighterTaken;
    }
}

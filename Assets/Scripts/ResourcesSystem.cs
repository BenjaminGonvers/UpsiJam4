using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceContainer : MonoBehaviour
{
    RessourceContainer()
    {
        
    }
    ~RessourceContainer() { }
}

public class RessourceSystem : MonoBehaviour
{
    public int RessourceSwat = 0;
    public int MaxRessourceSwat = 0;
    public int RessourcePolice = 0;
    public int MaxRessourcePolice = 0;
    public int RessourceFirefighter = 0;
    public int MaxRessourceFirefighter = 0;

    private int SwatTaken = 0;
    private int PoliceTaken = 0;
    private int FirefighterTaken = 0;

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
        
    }
}
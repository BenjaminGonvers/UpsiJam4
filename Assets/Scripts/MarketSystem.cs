using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketSystem : MonoBehaviour
{
    public int SwatQuantity;
    public int PoliceQuantity;
    public int FirefighterQuantity;

    public int SwatPrice = 20;
    public int PolicePrice = 20;
    public int FirefighterPrice = 20;

    public int Credit = 50;

    public void BuySwat()
    {
        if (Credit >= SwatPrice)
        {
            SwatQuantity++;
            Credit -= SwatPrice;
        }
        else
        {
            Debug.Log("Not enough credit to hire Swat");
        }
    }

    public void SellSwat()
    {
        if (SwatQuantity > 0)
        {
            SwatQuantity--;
            Credit += SwatPrice;
        }
        else
        {
            Debug.Log("No Swat to dismiss");
        }
    }

    public void BuyPolice()
    {
        if (Credit >= PolicePrice)
        {
            PoliceQuantity++;
            Credit -= PolicePrice;
        }
        else
        {
            Debug.Log("Not enough credit to hire Police");
        }
    }

    public void SellPolice()
    {
        if (PoliceQuantity > 0)
        {
            PoliceQuantity--;
            Credit += PolicePrice;
        }
        else
        {
            Debug.Log("No Police to dismiss");
        }
    }

    public void BuyFirefighter()
    {
        if (Credit >= FirefighterPrice)
        {
            FirefighterQuantity++;
            Credit -= FirefighterPrice;
        }
        else
        {
            Debug.Log("Not enough credit to hire Firefighter");
        }
    }

    public void SellFirefighter()
    {
        if (FirefighterQuantity > 0)
        {
            FirefighterQuantity--;
            Credit += FirefighterPrice;
        }
        else
        {
            Debug.Log("No Firefighter to dismiss");
        }
    }
}

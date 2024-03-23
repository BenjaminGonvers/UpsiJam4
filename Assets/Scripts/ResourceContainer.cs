using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceContainer : MonoBehaviour
{
    public List<SpriteRenderer> SpritesList;
    public int VisibleRessource = 0;
    public int OldVisibleRessource = 0;

    void OnMouseDown()
    {
        Debug.Log("Sprite Clicked");
    }

    void OnMouseDrag()
    {
        Debug.Log("Sprite Dragged");
    }

    void OnMouseUp()
    {
        Debug.Log("Sprite Released");
    }
    // Update is called once per frame
    void Update()
    {
        if (VisibleRessource != OldVisibleRessource)
        {
            if (VisibleRessource > SpritesList.Count)
            {
                VisibleRessource = SpritesList.Count;
            }
            else if (VisibleRessource < 0)
            {
                VisibleRessource = 0;
            }

            for (int i = 0; i < SpritesList.Count; i++)
            {
                if (i < VisibleRessource)
                {
                    SpritesList[i].enabled = true;
                }
                else
                {
                    SpritesList[i].enabled = false;
                }
            }

            OldVisibleRessource = VisibleRessource;
        }
    }
}

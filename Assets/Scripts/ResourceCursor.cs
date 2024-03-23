using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCursor : MonoBehaviour
{
   public bool IsVisible = false;

    // Update is called once per frame
    void Update()
    {


        if (IsVisible)
        {
            if (!GetComponent<SpriteRenderer>().enabled)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, -9);
        }
        else if (GetComponent<SpriteRenderer>().enabled)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

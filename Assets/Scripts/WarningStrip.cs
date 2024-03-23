using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningStrip : MonoBehaviour
{
    private float DeltaTime_= 0.0f;
    private float TimeLastWarning = 0.0f;

    public float Speed = 1.0f;
    public float TimeBetweenWarning = 20.0f;
    
    public List<string> WarningList = new List<string>();

    void Start()
    {
        transform.position = new Vector3(9, -(float)4.5, -1);
    }
    private string GetRandomWarning()
    {
        return WarningList[Random.Range(0, WarningList.Count)];
    }

    public void SendWarning(string Warning)
    {
        TimeLastWarning = 0.0f;
        transform.position = new Vector3(9, -(float) 4.5, -1);
        GetComponent<TextMeshPro>().text = Warning;
    }
        // Update is called once per frame
    void Update()
    {
        DeltaTime_ = Time.deltaTime;
        TimeLastWarning += DeltaTime_;

        if (TimeLastWarning >= TimeBetweenWarning)
        {
            TimeLastWarning = 0.0f;
            transform.position = new Vector3(9, -(float) 4.5, -1);
            GetComponent<TextMeshPro>().text = GetRandomWarning();
        }
        else
        {
            transform.position -= new Vector3(Speed * DeltaTime_, 0,0);
        }
    }
}

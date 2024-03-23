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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DeltaTime_ = Time.deltaTime;
        TimeLastWarning += DeltaTime_;

        if (TimeLastWarning >= TimeBetweenWarning)
        {
            TimeLastWarning = 0.0f;
            transform.localPosition = new Vector3(0, 0, 0);
            GetComponent<TextMeshPro>().text = "Warning";
        }
        else
        {
            transform.position -= new Vector3(Speed * DeltaTime_, 0,0);
        }
    }
}

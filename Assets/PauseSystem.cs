using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;


    public void Return()
    {
        _canvas.GetComponent<Canvas>().enabled = false;
    }

    public void MainMenu()
    {
        GameObject.Find("SceneSystem").GetComponent<LevelLoader>().LoadScene("MainMenu");
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            //Set GameManager in Pause

            _canvas.GetComponent<Canvas>().enabled = !_canvas.GetComponent<Canvas>().enabled;

        }
    }
}

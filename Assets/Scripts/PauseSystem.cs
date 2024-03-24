using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    private GameManager _gameManager;
    void Start()
    {
      
    }

    public void Return()
    {
        _canvas.GetComponent<Canvas>().enabled = false;
        _gameManager.SetPause(false);
    }

    public void MainMenu()
    {
        _gameManager.SetPause(false);
        GameObject.Find("SceneSystem").GetComponent<LevelLoader>().LoadScene("MainMenu");
        Destroy(this.gameObject);
        Destroy(GameObject.Find("SceneSystem"));
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            //Set GameManager in Pause

            _canvas.GetComponent<Canvas>().enabled = !_canvas.GetComponent<Canvas>().enabled;
            if(_gameManager == null)
                _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _gameManager.SetPause(true);
}
    }
}

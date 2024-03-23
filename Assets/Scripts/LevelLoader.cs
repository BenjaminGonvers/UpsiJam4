using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _canvasPausePrefab;
    GameObject _canvasPause;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ButtonLoadScene(string sceneName)
    {
        GameObject soundManager = GameObject.Find("SoundManager");

        if (soundManager)
        {
            soundManager.GetComponent<SoundManager>().PlaySound(SoundManager.Sound.Button);
        }

        LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            if (_canvasPause != null)
                Destroy(_canvasPause);
            Destroy(GameObject.Find("GameManager"));
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayMusic(SoundManager.Music.MainMenu);
        }

        if (sceneName == "Dylan" || sceneName == "EdouardScene" || sceneName == "BenjaminScene" || sceneName == "InGame")
        {
            _canvasPause = Instantiate(_canvasPausePrefab);
            DontDestroyOnLoad(_canvasPause);
             if (GameObject.Find("SoundManager"))
                GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayMusic(SoundManager.Music.InGame);
        }

        if (sceneName == "ScoreMenu")
        {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayMusic(SoundManager.Music.ScoreMenu);
        }

        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        GameObject soundManager = GameObject.Find("SoundManager");

        if (soundManager)
        {
            soundManager.GetComponent<SoundManager>().PlaySound(SoundManager.Sound.Button);
        }

        Application.Quit();
    }
}
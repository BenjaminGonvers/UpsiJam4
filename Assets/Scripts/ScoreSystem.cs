using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int _hitBasePoint;
    [SerializeField] private int _recyclingBasePoint;
    [SerializeField] private float _killModifier;
    [SerializeField] private int _score;

    private int _hitCombo;
    private float _headShotModifier;
    private float _recyclingComboModifier;

    public int Score => _score;

    private void Awake()
    {
        var scoreSystems = FindObjectsByType<ScoreSystem>(FindObjectsSortMode.None);
        if (scoreSystems.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ResetScore()
    {
        _score = 0;
    }

    public void OnEnemyShoot(bool kill)
    {
        float scoreReturn = _hitBasePoint;

        if (kill)
        {
            scoreReturn *= _killModifier;
        }

        _score += Mathf.CeilToInt(scoreReturn);
    }

    public void OnRecycle(int bodyCount)
    {
        float scoreReturn = _recyclingBasePoint * bodyCount;

        _score += Mathf.CeilToInt(scoreReturn);
    }
}
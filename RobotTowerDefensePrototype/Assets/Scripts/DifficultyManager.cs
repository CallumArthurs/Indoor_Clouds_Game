using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public SaucerSpawner[] spawner = null;
    public Text timerText = null;
    public float DifficultyScale;
    public static float DifficultyScore;

    private static float _timeScore;
    void Start()
    {
        _timeScore = 0.0f;
    }

    void Update()
    {
        _timeScore += Time.deltaTime;
        UpdateTimer();
        CalculateDifficulty();
    }
    void UpdateTimer()
    {
        timerText.text = _timeScore.ToString();
    }
    void CalculateDifficulty()
    {
        DifficultyScore = _timeScore / (DifficultyScale * 100);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour {
    public SaucerSpawner[] spawner = null;
    public Text timerText = null;
    public static float _timeScore;

    void Start () {
        _timeScore = 0.0f;
    }
	
	void Update () {
        _timeScore += Time.deltaTime;
        UpdateTimer();
    }
    void UpdateTimer()
    {
        timerText.text = _timeScore.ToString("F2");
    }
}

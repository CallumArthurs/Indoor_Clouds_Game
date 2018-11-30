using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour {
    public SaucerSpawner[] spawner = null;
    public SceneSwitcher sceneManager;
    public Text timerText = null;
    public float DifficultyScale;
    public static float DifficultyScore;
    public List<GameObject> HQs;

    private static float _timeScore;
    void Start () {
        HQs.AddRange(GameObject.FindGameObjectsWithTag("HeadQuaters"));
        _timeScore = 0.0f;
    }
	
	void Update () {
        _timeScore += Time.deltaTime;
        UpdateTimer();
        CalculateDifficulty();

        if(HQs.Count == 0)
        {
            sceneManager.LostTheGame();
        }
    }
    void UpdateTimer()
    {
        timerText.text = _timeScore.ToString("F2");
    }
    void CalculateDifficulty()
    {
        DifficultyScore = _timeScore / (DifficultyScale * 100);
    }
}

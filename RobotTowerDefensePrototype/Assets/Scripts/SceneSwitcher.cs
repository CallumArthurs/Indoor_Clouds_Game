using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

	void Start () {
        Cursor.lockState = CursorLockMode.Confined;
	}
	
	void Update () {

    }

    public void PlayTheGameAlready()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void LostTheGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

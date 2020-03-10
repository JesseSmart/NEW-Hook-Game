using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

	public GameObject pnlPause;

	private bool audioOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	public void Resume()
	{
		Time.timeScale = 1f;
		pnlPause.SetActive(false);
	}

	public void BackToMenu()
	{
		Time.timeScale = 1f;
		pnlPause.SetActive(false);
		SceneManager.LoadScene("JesseMainMenu");
	}

	public void AudioPressed()
	{
		print("audio");
		audioOn = !audioOn;
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainManager : MonoBehaviour
{

	private GameObject playerObj;

	//public GameObject playerSpawnedObj;
	//private GameObject spawnPoint;
	private CameraController cameraCon;

	[HideInInspector]
	public GameObject pnlPause;
	[HideInInspector]
	public GameObject pnlWin;

	[HideInInspector]
	public float bronzeTime;
	[HideInInspector]
	public float silverTime;
	[HideInInspector]
	public float goldTime;
	private float levelTimer;
	private float introTimer = 3;
	private bool hasStarted;
	private bool hasFinished;

	public bool isNight;
	public Sprite[] dayHookPointSprites;
	public Sprite[] nightHookPointSprites;

	//UI
	private Text introTimerDisplay;
	private Text levelTimerDisplay;
	private Text coinDisplay;
	private Text medalDisplay;
	private Text highscoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
		playerObj = FindObjectOfType<PlayerMaster>().gameObject;
		cameraCon = FindObjectOfType<CameraController>();

		pnlPause = GameObject.Find("PausePanel");
		pnlWin = GameObject.Find("Win Panel");

		introTimerDisplay = GameObject.Find("Intro Timer").GetComponent<Text>();
		levelTimerDisplay = GameObject.Find("Level Timer Display").GetComponent<Text>();
		coinDisplay = GameObject.Find("Coin Display").GetComponent<Text>();
		medalDisplay = GameObject.Find("Medal Text").GetComponent<Text>();
		highscoreDisplay = GameObject.Find("Highscore Display").GetComponent<Text>();


		highscoreDisplay.text = "Highscore: " + PlayerPrefs.GetFloat("LocalHighscore").ToString("F2");


		if (isNight)
		{
			playerObj.GetComponent<PlayerMaster>().hookPointSprites = nightHookPointSprites;
		}
		else
		{
			playerObj.GetComponent<PlayerMaster>().hookPointSprites = dayHookPointSprites;
		}
		//pnlPause = GameObject.Find("PausePanel");
	}

	// Update is called once per frame
	void Update()
    {
		//debug
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			ClearCoin();
		}

		//debug
		if (Input.GetKeyDown(KeyCode.Plus))
		{
			PlayerPrefs.SetFloat("LocalHighscore", 0);
		}


		if (Input.GetKeyDown(KeyCode.P))
		{
			PauseBehaviour();
		}
		if (!hasFinished)
		{
			if (!hasStarted)
			{
				introTimer -= Time.deltaTime;
				introTimerDisplay.text = introTimer.ToString("0");
			}
			else
			{			
				levelTimer += Time.deltaTime;
				levelTimerDisplay.text = levelTimer.ToString("00:00");

				

			}

			if (introTimer < 0)
			{
				//Start Sequence
				hasStarted = true;
				introTimerDisplay.enabled = false;
				playerObj.GetComponent<PlayerMaster>().haveStarted = true;
			}
		}

		
	}

	public void PlayerDeath()
	{
		pnlWin.SetActive(true);
	}



	void PauseBehaviour()
	{
		if (Time.timeScale > 0)
		{
			Time.timeScale = 0f;
			pnlPause.SetActive(true);
		}
		else
		{
			Time.timeScale = 1f;
			pnlPause.SetActive(false);
		}
	}

	public void CollectCoin()
	{
		PlayerPrefs.SetInt("GerkiCoinCount", PlayerPrefs.GetInt("GerkiCoinCount") + 1);
		//print("Coin: " + PlayerPrefs.GetInt("GerkiCoinCount"));
		coinDisplay.text = PlayerPrefs.GetInt("GerkiCoinCount").ToString();
	}

	public void ClearCoin()
	{
		PlayerPrefs.SetInt("GerkiCoinCount", 0);
	}

	public void CrossFinish()
	{
		hasFinished = true;

		medalDisplay.enabled = true;

		if (levelTimer > 0 && levelTimer <= goldTime)
		{
			//gold
			medalDisplay.text = "GOLD";
		}
		else if (levelTimer > goldTime && levelTimer <= silverTime)
		{
			//silver
			medalDisplay.text = "SILVER";

		}
		else if (levelTimer > silverTime)
		{
			//bronze
			medalDisplay.text = "BRONZE";

		}


		if (levelTimer < PlayerPrefs.GetFloat("LocalHighscore"))
		{
			PlayerPrefs.SetFloat("LocalHighscore", levelTimer);
			highscoreDisplay.text = "Highscore: " + levelTimer.ToString("F2");
		}


		//Do End Stuff
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

	private GameObject playerObj;

	public GameObject playerSpawnedObj;
	private GameObject spawnPoint;
	private CameraController cameraCon;

	public GameObject pnlPause;
    // Start is called before the first frame update
    void Start()
    {
		playerObj = FindObjectOfType<PlayerMaster>().gameObject;
		cameraCon = FindObjectOfType<CameraController>();
		spawnPoint = GameObject.Find("EGO SpawnPoint");
		//pnlPause = GameObject.Find("PausePanel");
	}

	// Update is called once per frame
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.P))
		{
			PauseBehaviour();
		}
	}

	public void PlayerDeath()
	{
		StartCoroutine(DelayedPlayerDeath(2f));

	}

	IEnumerator DelayedPlayerDeath(float delay)
	{
		yield return new WaitForSeconds(delay);
		GameObject spawnedPlayer = Instantiate(playerSpawnedObj, spawnPoint.transform.position, transform.rotation);
		cameraCon.targetObject = spawnedPlayer;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleParralax : MonoBehaviour
{

	public GameObject playerObj;
	[HideInInspector]
	public Vector3 playerStartPos;
	private int startX;

	public GameObject spriteHolder;
	public Sprite sprite;

	private int nextStartX;

	private int timesRan;
	private int totalPlacements;

	public float floorOffset;
	private float closestX = 0;

	private float spawnWaitDur = 1;
	private float waitTimer;

	// Start is called before the first frame update
	void Start()
	{
		playerObj = FindObjectOfType<PlayerMaster>().gameObject;
		nextStartX = Mathf.RoundToInt(transform.position.x);

		SpawnLoop(sprite, 10, 1, 1, 0);
	}

	// Update is called once per frame
	void Update()
	{
		float dist = Mathf.Abs(closestX - playerObj.transform.position.x);
		waitTimer -= Time.deltaTime;
		print("Closestx " + closestX);
		if (waitTimer <= 0) //this is where issue is. 'dist' needs to be calculated better/correctly
		{
			SpawnLoop(sprite, 10, 1, 1, 0);


			waitTimer = spawnWaitDur;
		}
	}


	void SpawnLoop(Sprite mySprite, int amount, float layer, int sort, float yOffset)
	{
		for (int i = 0; i < amount; i++)
		{
			Vector3 spawnVec = new Vector3(nextStartX + (mySprite.bounds.extents.x * 2 * i) , transform.position.y + yOffset, transform.position.z);
			GameObject currentSpawn = Instantiate(spriteHolder, spawnVec, transform.rotation);
			currentSpawn.GetComponentInChildren<SpriteRenderer>().sprite = mySprite;
			currentSpawn.GetComponentInChildren<SpriteRenderer>().sortingOrder = sort;
			currentSpawn.GetComponent<ParralaxObject>().parralaxLayer = layer;

			currentSpawn.GetComponent<ParralaxObject>().playerStart = playerStartPos; //newaddition
			totalPlacements++;
			print("totalPlace " + totalPlacements);
		}
		timesRan++;
		nextStartX = Mathf.RoundToInt(playerStartPos.x + (mySprite.bounds.extents.x * 2 * totalPlacements));


		//closestX = nextStartX;
		//imagePosXSave += Mathf.RoundToInt(mySprite.bounds.extents.x * 2 * amount * timesRan);

		//if (mySprite.bounds.extents.x * 2 * amount * timesRan < closestX)
		//{
		//	closestX = mySprite.bounds.extents.x * 2 * amount * timesRan;
		//}
	}
}

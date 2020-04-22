using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxSpawner : MonoBehaviour
{

	//public GameObject foreGroundObj;
	public GameObject playerObj;
	[HideInInspector]
	public Vector3 playerStartPos;
	private int startX;

	public GameObject spriteHolder;
	public Sprite foreGroundImage;
	public Sprite midGroundImage;
	public Sprite backGroundImage;
	public Sprite floorImage;
	private int[] imagePosXSave = new int[4];
	private int[] timesRan = new int[4];

	private int[] spawnIndexer = new int[4];

	public float forGroundNum;
	public float midGroundNum;
	public float backGroundNum;


	public float floorOffset;

	private float closestX = 0;

	private float spawnWaitDur = 1;
	private float waitTimer;
	
    // Start is called before the first frame update
    void Start()
    {
		playerObj = FindObjectOfType<PlayerMaster>().gameObject;
		playerStartPos = playerObj.transform.position;

		SpawnLoop(foreGroundImage, 10, forGroundNum, 1, 0);
		SpawnLoop(midGroundImage, 10, midGroundNum, 2, 0);
		SpawnLoop(backGroundImage, 10, backGroundNum, 3, 0);
		SpawnLoop(floorImage, 10, 0, 4, floorOffset);

		startX = Mathf.RoundToInt(transform.position.x); //make sure rounding isnt issue incase u just need first bit
		//StartCoroutine(DoSpawn(foreGroundImage, forGroundNum, 1, 0, 0));
		//StartCoroutine(DoSpawn(midGroundImage, midGroundNum, 2, 0, 0));
		//StartCoroutine(DoSpawn(backGroundImage, backGroundNum, 3, 0, 0));
		//StartCoroutine(DoSpawn(floorImage, 0, 4, floorOffset, 0));

	}

    // Update is called once per frame
    void Update()
    {
		float dist = Mathf.Abs(closestX - playerObj.transform.position.x);
		waitTimer -= Time.deltaTime;


		if (dist <= 10 && waitTimer <= 0)
		{
			SpawnLoop(foreGroundImage, 10, forGroundNum, 1, 0);
			SpawnLoop(midGroundImage, 10, midGroundNum, 2, 0);
			SpawnLoop(backGroundImage, 10, backGroundNum, 3, 0);
			SpawnLoop(floorImage, 10, 0, 4, floorOffset);

			waitTimer = spawnWaitDur;
		}

		//DoTimeSpawn(foreGroundImage, forGroundNum, 1, 0, 0);

		//transform.Translate(Vector2.right * Time.deltaTime * 2);

	}

	void SpawnWait(Sprite mySprite, float layer, int sort, float yOffset)
	{
		float gap = mySprite.bounds.extents.x * 2;

		if ((transform.position.x - startX) % gap == 0)
		{
			Vector3 spawnVec = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

			GameObject currentSpawn = Instantiate(spriteHolder, spawnVec, transform.rotation);
			currentSpawn.GetComponentInChildren<SpriteRenderer>().sprite = mySprite;
			currentSpawn.GetComponentInChildren<SpriteRenderer>().sortingOrder = sort;
			currentSpawn.GetComponent<ParralaxObject>().parralaxLayer = layer;

			currentSpawn.GetComponent<ParralaxObject>().playerStart = playerStartPos;
		}


	}

	void SpawnLoop(Sprite mySprite, int amount, float layer, int sort, float yOffset)
	{
		for (int i = 0; i < amount; i++)
		{
			Vector3 spawnVec = new Vector3(transform.position.x + (mySprite.bounds.extents.x * 2 * i) + imagePosXSave[sort - 1], transform.position.y + yOffset, transform.position.z);
			GameObject currentSpawn = Instantiate(spriteHolder, spawnVec, transform.rotation);
			currentSpawn.GetComponentInChildren<SpriteRenderer>().sprite = mySprite;
			currentSpawn.GetComponentInChildren<SpriteRenderer>().sortingOrder = sort;
			currentSpawn.GetComponent<ParralaxObject>().parralaxLayer = layer;

			currentSpawn.GetComponent<ParralaxObject>().playerStart = playerStartPos; //newaddition

		}
		timesRan[sort - 1]++;
		imagePosXSave[sort - 1] += Mathf.RoundToInt(mySprite.bounds.extents.x * 2 * amount * timesRan[sort - 1]);

		if (mySprite.bounds.extents.x * 2 * amount * timesRan[sort - 1] < closestX)
		{
			closestX = mySprite.bounds.extents.x * 2 * amount * timesRan[sort - 1];
		}
	}

	IEnumerator DoSpawn(Sprite mySprite, float layer, int sort, float yOffset, int ind)
	{
		yield return new WaitForSeconds(1f);
		//Vector3 spawnVec = new Vector3(transform.position.x + (mySprite.bounds.extents.x * 2 * ind), transform.position.y + yOffset, transform.position.z);
		Vector3 spawnVec = new Vector3(transform.position.x + (mySprite.bounds.extents.x * 2 * ind), transform.position.y + yOffset, transform.position.z);
		
		GameObject currentSpawn = Instantiate(spriteHolder, spawnVec, transform.rotation);
		currentSpawn.GetComponent<ParralaxObject>().playerObj = FindObjectOfType<PlayerMaster>().gameObject;
		currentSpawn.GetComponentInChildren<SpriteRenderer>().sprite = mySprite;
		currentSpawn.GetComponentInChildren<SpriteRenderer>().sortingOrder = sort;
		currentSpawn.GetComponent<ParralaxObject>().parralaxLayer = layer;
		currentSpawn.GetComponent<ParralaxObject>().playerStart = playerStartPos;
		print("AAAA");
		Gap(mySprite, layer, sort, yOffset, ind);
	}

	void Gap(Sprite mySprite, float layer, int sort, float yOffset, int ind)
	{
		StartCoroutine(DoSpawn(mySprite, layer, sort, yOffset, ind));

	}


	void DoTimeSpawn(Sprite mySprite, float layer, int sort, float yOffset, int indexNum)
	{

		StartCoroutine(TimeSpawn(mySprite, layer, sort, yOffset, indexNum));

	}

	IEnumerator TimeSpawn(Sprite mySprite, float layer, int sort, float yOffset, int indexNum)
	{
		yield return new WaitForSeconds(1f);


		float gap = mySprite.bounds.extents.x * 2;

		
		Vector3 spawnVec = new Vector3(transform.position.x + (gap * spawnIndexer[indexNum]), transform.position.y + yOffset, transform.position.z);

		GameObject currentSpawn = Instantiate(spriteHolder, spawnVec, transform.rotation);
		currentSpawn.GetComponentInChildren<SpriteRenderer>().sprite = mySprite;
		currentSpawn.GetComponentInChildren<SpriteRenderer>().sortingOrder = sort;
		currentSpawn.GetComponent<ParralaxObject>().parralaxLayer = layer;

		currentSpawn.GetComponent<ParralaxObject>().playerStart = playerStartPos;

		DoTimeSpawn(mySprite, layer, sort, yOffset, indexNum);

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxSpawner : MonoBehaviour
{

	//public GameObject foreGroundObj;
	private GameObject playerObj;
	[HideInInspector]
	public Vector3 playerStartPos;

	public GameObject spriteHolder;
	public Sprite foreGroundImage;
	public Sprite midGroundImage;
	public Sprite backGroundImage;
	public Sprite floorImage;

	public float forGroundNum;
	public float midGroundNum;
	public float backGroundNum;


	public float floorOffset;

	private float furthestX = 0;

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

		//StartCoroutine(DoSpawn(foreGroundImage, forGroundNum, 1, 0, 0));
		//StartCoroutine(DoSpawn(midGroundImage, midGroundNum, 2, 0, 0));
		//StartCoroutine(DoSpawn(backGroundImage, backGroundNum, 3, 0, 0));
		//StartCoroutine(DoSpawn(floorImage, 0, 4, floorOffset, 0));

	}

    // Update is called once per frame
    void Update()
    {
		//float dist = Mathf.Abs(furthestX - playerObj.transform.position.x);
		//waitTimer -= Time.deltaTime;


		//if (dist <= 10 && waitTimer <= 0)
		//{
		//	SpawnLoop(foreGroundImage, 10, forGroundNum, 1, 0);
		//	SpawnLoop(midGroundImage, 10, midGroundNum, 2, 0);
		//	SpawnLoop(backGroundImage, 10, backGroundNum, 3, 0);
		//	SpawnLoop(floorImage, 10, 0, 4, floorOffset);

		//	waitTimer = spawnWaitDur;
		//}
	}

	void SpawnLoop(Sprite mySprite, int amount, float layer, int sort, float yOffset)
	{
		for (int i = 0; i < amount; i++)
		{
			Vector3 spawnVec = new Vector3(transform.position.x + (mySprite.bounds.extents.x * 2 * i), transform.position.y + yOffset, transform.position.z);
			GameObject currentSpawn = Instantiate(spriteHolder, spawnVec, transform.rotation);
			currentSpawn.GetComponentInChildren<SpriteRenderer>().sprite = mySprite;
			currentSpawn.GetComponentInChildren<SpriteRenderer>().sortingOrder = sort;
			currentSpawn.GetComponent<ParralaxObject>().parralaxLayer = layer;

			currentSpawn.GetComponent<ParralaxObject>().playerStart = playerStartPos; //newaddition

		}
	}

	IEnumerator DoSpawn(Sprite mySprite, float layer, int sort, float yOffset, int ind)
	{
		yield return new WaitForSeconds(1f);

		//Vector3 spawnVec = new Vector3(transform.position.x + (mySprite.bounds.extents.x * 2 * ind), transform.position.y + yOffset, transform.position.z);
		Vector3 spawnVec = new Vector3(transform.position.x + (mySprite.bounds.extents.x * 2 * ind), transform.position.y + yOffset, transform.position.z);
		GameObject currentSpawn = Instantiate(spriteHolder, spawnVec, transform.rotation);
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

}

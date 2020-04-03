using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxSpawner : MonoBehaviour
{

	//public GameObject foreGroundObj;

	public GameObject spriteHolder;
	public Sprite foreGroundImage;
	public Sprite midGroundImage;
	public Sprite backGroundImage;
	public Sprite floorImage;

	public float forGroundNum;
	public float midGroundNum;
	public float backGroundNum;


	public float floorOffset;

    // Start is called before the first frame update
    void Start()
    {


		SpawnLoop(foreGroundImage, 10, forGroundNum, 1, 0);
		SpawnLoop(midGroundImage, 10, midGroundNum, 2, 0);
		SpawnLoop(backGroundImage, 10, backGroundNum, 3, 0);
		SpawnLoop(floorImage, 10, 0, 4, floorOffset);

    }

    // Update is called once per frame
    void Update()
    {

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
		}
	}
}

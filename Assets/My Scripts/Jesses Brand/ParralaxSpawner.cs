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

	public float forGroundNum;
	public float midGroundNum;
	public float backGroundNum;


    // Start is called before the first frame update
    void Start()
    {


		SpawnLoop(foreGroundImage, 10, forGroundNum, 1);
		SpawnLoop(midGroundImage, 10, midGroundNum, 2);
		SpawnLoop(backGroundImage, 10, backGroundNum, 3);

    }

    // Update is called once per frame
    void Update()
    {

	}

	void SpawnLoop(Sprite mySprite, int amount, float layer, int sort)
	{
		for (int i = 0; i < amount; i++)
		{
			Vector3 spawnVec = new Vector3(transform.position.x + (mySprite.bounds.extents.x * 2 * i), transform.position.y, transform.position.z);
			GameObject currentSpawn = Instantiate(spriteHolder, spawnVec, transform.rotation);
			currentSpawn.GetComponentInChildren<SpriteRenderer>().sprite = mySprite;
			currentSpawn.GetComponentInChildren<SpriteRenderer>().sortingOrder = sort;
			currentSpawn.GetComponent<ParralaxObject>().parralaxLayer = layer;
		}
	}
}

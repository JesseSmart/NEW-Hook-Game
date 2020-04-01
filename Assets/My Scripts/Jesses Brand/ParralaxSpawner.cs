using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxSpawner : MonoBehaviour
{

	
	public Sprite foreGroundImage;
	public Sprite midGroundImage;
	public Sprite backGroundImage;


    // Start is called before the first frame update
    void Start()
    {
		Instantiate(foreGroundImage, Vector3.zero, transform.rotation);
		SpawnLoop(foreGroundImage, 10);
    }

    // Update is called once per frame
    void Update()
    {

	}

	void SpawnLoop(Sprite mySprite, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			Vector3 spawnVec = new Vector3(transform.position.x + (mySprite.rect.x * i), transform.position.y, transform.position.z);
			Instantiate(mySprite, spawnVec, transform.rotation);
			print("spawned");
		}
	}
}

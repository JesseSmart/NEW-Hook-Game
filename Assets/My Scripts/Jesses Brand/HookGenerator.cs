﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookGenerator : MonoBehaviour
{

	public GameObject hookPointObj;
	public GameObject hookLanternPointObj;

	public int hookGap;

	[HideInInspector]
	public GameObject playerObj;
	private Vector3 startPos;

	public float verticalVar;
    // Start is called before the first frame update
    void Start()
    {
		startPos = transform.position;

		playerObj = GameObject.FindGameObjectWithTag("Player");

		if (FindObjectOfType<MainManager>().isNight)
		{
			hookPointObj = hookLanternPointObj;
		}

		GenerateHooks(50);
    }

    // Update is called once per frame
    void Update()
    {
		if (playerObj != null)
		{
			float dist = Vector2.Distance(playerObj.transform.position, transform.position);
			if (dist < 50)
			{
				GenerateHooks(50);
			}

		}
		else if (FindObjectOfType<PlayerMaster>())
		{
			playerObj = FindObjectOfType<PlayerMaster>().gameObject;
		}
    }

	void GenerateHooks(int hookAmount)
	{
		for (int i = 0; i < hookAmount; i++)
		{

			Vector3 nextHookPos = new Vector3(transform.position.x + hookGap, transform.position.y + Random.Range(-verticalVar, verticalVar), transform.position.z);
			Instantiate(hookPointObj, nextHookPos, transform.rotation);
			transform.position = new Vector3 (nextHookPos.x, transform.position.y, transform.position.z);

		}
	}

}

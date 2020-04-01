﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxObject : MonoBehaviour
{

	private GameObject playerObj;
	private Vector3 playerStart;
	private Vector3 myStartPos;
	public float parralaxLayer;

	private float parralaxLayerSpeed;

    // Start is called before the first frame update
    void Start()
    {
		playerObj = FindObjectOfType<PlayerMaster>().gameObject;
		playerStart = playerObj.transform.position;
		myStartPos = transform.position;
		parralaxLayerSpeed = 1 + (0.4f * parralaxLayer);

		//switch (parralaxLayer)
		//{
		//	case 0:
		//		parralaxLayerSpeed = 0;
		//		break;
		//	case 1:
		//		parralaxLayerSpeed = 1.2f;
		//		break;
		//}
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector3(myStartPos.x - (playerObj.transform.position.x * parralaxLayerSpeed), transform.position.y, transform.position.z);
    }
}

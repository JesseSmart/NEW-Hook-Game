using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxObject : MonoBehaviour
{

	public GameObject playerObj;
	[HideInInspector]
	public Vector3 playerStart;
	private Vector3 myStartPos;
	public float parralaxLayer;

	private float parralaxLayerSpeed;

    // Start is called before the first frame update
    void Start()
    {
		playerObj = FindObjectOfType<PlayerMaster>().gameObject;
		//playerStart = playerObj.transform.position;
		myStartPos = transform.position;
		parralaxLayerSpeed = 1 + (0.4f * parralaxLayer);

	
    }

    // Update is called once per frame
    void Update()
    {
		if (playerObj != null)
		{
			transform.position = new Vector3(myStartPos.x - (playerObj.transform.position.x * parralaxLayerSpeed), transform.position.y, transform.position.z);

		}
    }
}

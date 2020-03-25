using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
	public enum PlayerState
	{
		Swinging,
		Running,
		Flying

	}

	[Header("Movement")]
	public PlayerState myState;

	public KeyCode hookButton;
	public bool doMovement;
	public float speedMod;

	[Header("Hook Variables")]
	public int jumpForce;
	public int hookForce;
	public int verticalHookForce;
	public int hookRange;

	private DistanceJoint2D dJoint;
	private Rigidbody2D rb;
	private Animator anim;

	public GameObject deathObj;

	private bool unHook;
	private MainManager mainManager;

	

	private bool swingBoolHold;
	private bool cancelSwing;

	private float yValLastFrame;
	[HideInInspector]
	public Sprite[] hookPointSprites;
	private GameObject nextHookObj;

	[HideInInspector]
	public bool haveStarted;


	//Audio
	[Header("Audio")]
	public AudioClip fallDeathAudio;
	public AudioClip collideDeathAudio;


	//Line Renderer
	#region
	//public Transform position1; //Player

	[Header("Line Rendering")]
	public Color lineColour; //line colour 
	private Transform position2; //hookpoint
	private LineRenderer lineR; //the line render
	public Material myMaterial; 
	private string hookPointTag; //Tag of hookpoints 
	public int tileAmount;
	private GameObject lastHookPoint;
	#endregion

	// Start is called before the first frame update
	void Start()
    {

		rb = GetComponent<Rigidbody2D>();
		dJoint = GetComponent<DistanceJoint2D>();
		lineR = GetComponent<LineRenderer>();
		hookPointTag = "Grapple Point";
		//sRenderer = GetComponent<SpriteRenderer>();
		//sRenderer.sprite = sprRightSwing;
		anim = GetComponentInChildren<Animator>();
		mainManager = FindObjectOfType<MainManager>();
		yValLastFrame = gameObject.transform.position.y;

		//Line Rendering
		lineR.material.color = lineColour;
		lineR.material = myMaterial;
		lineR.SetColors(lineColour, lineColour);

		if (tileAmount == 0)
		{
			tileAmount = 1;
		}

	}

    // Update is called once per frame
    void Update()
    {
		if (!haveStarted)
		{
			return;
		}


		

		transform.Translate(Vector3.right * speedMod * Time.deltaTime, Space.World);






		if (transform.position.y != yValLastFrame && myState == PlayerState.Running)
		{
			myState = PlayerState.Flying;
			print("set to fly");
		}

		yValLastFrame = transform.position.y;

		switch (myState)
		{
			case PlayerState.Swinging:

				break;

			case PlayerState.Running:
				anim.SetInteger("PlayerState", 2);
				break;

			case PlayerState.Flying:
				anim.SetInteger("PlayerState", 3);

				break;
		}

		

		if (Input.GetKeyDown(hookButton))
		{
			if (!swingBoolHold)
			{
				cancelSwing = false;


				if (myState == PlayerState.Running)
				{
					//Jump();

					StartCoroutine(DelayedJumpAndHook(1f));
				}

				if (myState == PlayerState.Flying)
				{
					rb.AddForce(Vector2.up * verticalHookForce);
					StartSwing();
				}				
			}
		}


		//Make white
		foreach (GameObject hookP in GameObject.FindGameObjectsWithTag("Grapple Point"))
		{
			if (hookP.transform.position.x < gameObject.transform.position.x)
			{
				nextHookObj.GetComponent<SpriteRenderer>().sprite = hookPointSprites[0];

			}
		}

		//Make blue
		GameObject[] hookPointsObj = GameObject.FindGameObjectsWithTag(hookPointTag);
		Transform[] hookPointTrans = new Transform[hookPointsObj.Length];
		for (int i = 0; i < hookPointsObj.Length; i++)
		{
			hookPointTrans[i] = hookPointsObj[i].transform;
		}
		Transform tempPos = GetClosestPoint(hookPointTrans);

		foreach (GameObject go in hookPointsObj)
		{
			if (go.transform.position == tempPos.position)
			{
				//HERE is where you would put a "you will be going to this hook next but its currently out of range (maybe like a white with dotted blue)

				float dist = Vector3.Distance(transform.position, go.transform.position); //need to check if has fallen out of range incase they go in and out of range. Maybe an ELSE just below and set to white
				if (dist < hookRange)
				{
					nextHookObj = go;
					nextHookObj.GetComponent<SpriteRenderer>().sprite = hookPointSprites[1];

				}


				break;
			}
		}

		if (Input.GetKey(hookButton))
		{
			if (!unHook && !swingBoolHold) //THIS WAS AN 'OR' BEFORE. THIS 'AND' HAS NOT BEEN TESTED MUCH. 'OR' HAD SOME ISSUES AND THIS WAS A SOLUTION
			{
				LineRendering();
				SpriteSetter();

				GameObject[] PointsObj = GameObject.FindGameObjectsWithTag(hookPointTag);

				foreach (GameObject go in PointsObj)
				{
					if (go.transform.position == position2.position)
					{
						nextHookObj = go;
						nextHookObj.GetComponent<SpriteRenderer>().sprite = hookPointSprites[2];

						break;
					}
				}

				//nextHookObj.GetComponent<SpriteRenderer>().sprite = hookPointSprites[2];
			}


		}

		if (Input.GetKeyUp(hookButton))
		{
			UnHook();
			cancelSwing = true;
		}

		

	}

	void StartSwing()
	{
		
		SelectHook();

		float dist = Vector3.Distance(transform.position, position2.position);
		if (dist < hookRange)
		{
			myState = PlayerState.Swinging;

			dJoint.enabled = true;

			//tangent force
			Vector2 dir = (transform.position - position2.transform.position).normalized;
			dir = Vector2.Perpendicular(dir);
			rb.AddForce(dir * hookForce);

			nextHookObj.GetComponent<SpriteRenderer>().sprite = hookPointSprites[2];


			unHook = false;

		}
	}

	void UnHook()
	{
		lineR.enabled = false;
		dJoint.enabled = false;
		//sRenderer.sprite = sprIdleAir;
		myState = PlayerState.Running;
		//myState = PlayerState.Flying;

		//nextHookObj.GetComponent<SpriteRenderer>().sprite = hookPointSprites[0];

		

		unHook = true;
	}

	void SpriteSetter()
	{
		//if (transform.position.x < position2.position.x && sRenderer.sprite != sprRightSwing)
		if (transform.position.x < position2.position.x)
		{
			print("moving right");
			//sRenderer.sprite = sprRightSwing;
			anim.SetInteger("PlayerState", 0);

		}
		else if (transform.position.x >= position2.position.x)
		{
			print("moving left");
			//sRenderer.sprite = sprLeftSwing;

			anim.SetInteger("PlayerState", 1);


		}
		else
		{
			print("Swing Sprite Error");
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{

			GameObject myDeathObj = Instantiate(deathObj, transform.position, transform.rotation);
			DeathScript myScript = myDeathObj.GetComponent<DeathScript>();

			myScript.deathAudio = collideDeathAudio;

			mainManager.PlayerDeath();

			Destroy(gameObject);
		}

		if (other.CompareTag("Death Area Tag"))
		{


			GameObject myDeathObj = Instantiate(deathObj, transform.position, transform.rotation);
			DeathScript myScript = myDeathObj.GetComponent<DeathScript>();

			myScript.deathAudio = fallDeathAudio;

			mainManager.PlayerDeath();

			Destroy(gameObject);
			//StartCoroutine(DelayedRespawn(2f));
		}

		if (other.gameObject.CompareTag("Finish Line"))
		{
			CrossFinish();
		}

		if (other.gameObject.CompareTag("Coin"))
		{
			mainManager.CollectCoin();
			Destroy(other.gameObject);
		}

		if (other.gameObject.CompareTag("Finish Line"))
		{
			mainManager.CrossFinish();
		}
	}

	#region COLLISION
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("GROUND"))
		{
			if (myState == PlayerState.Swinging)
			{
				UnHook();
			}
			
			myState = PlayerState.Running;
		}

		if (other.gameObject.CompareTag("Enemy"))
		{

			GameObject myDeathObj = Instantiate(deathObj, transform.position, transform.rotation);
			DeathScript myScript = myDeathObj.GetComponent<DeathScript>();

			myScript.deathAudio = collideDeathAudio;

			mainManager.PlayerDeath();

			Destroy(gameObject);
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		print(other);
		if (other.gameObject.CompareTag("GROUND"))
		{
			myState = PlayerState.Running;
			print("set to running");

		}

	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("GROUND"))
		{
			if (myState == PlayerState.Running)
			{
				myState = PlayerState.Flying;
			}

		}
	}
	#endregion

	IEnumerator DelayedJumpAndHook(float delay)
	{
		swingBoolHold = true;
		Jump();
		yield return new WaitForSeconds(delay);
		if (!cancelSwing)
		{
			StartSwing();
		}
		swingBoolHold = false;
	}

	void Jump() //make more for time held
	{
		rb.AddForce(Vector2.up * jumpForce);
	}

	
	//HOOK BEHAVIOUR
	#region 
	void SelectHook()
	{
		lineR.enabled = true;

		GameObject[] hookPointsObj = GameObject.FindGameObjectsWithTag(hookPointTag);
		Transform[] hookPointTrans = new Transform[hookPointsObj.Length];

		for (int i = 0; i < hookPointsObj.Length; i++)
		{
			hookPointTrans[i] = hookPointsObj[i].transform;
		}


		
		position2 = GetClosestPoint(hookPointTrans);

		
	}
	void LineRendering()
	{

		//lineR.SetPosition(0, position1.position);

		Vector3 pos1 = new Vector3(transform.position.x, transform.position.y, -20);
		position2.position = new Vector3(position2.position.x, position2.position.y, -20);

		lineR.SetPosition(0, pos1);
		lineR.SetPosition(1, position2.position);

		dJoint.connectedAnchor = position2.position;

		//float dist = Vector3.Distance(position1.position, position2.position);
		float dist = Vector3.Distance(transform.position, position2.position);
		lineR.material.mainTextureScale = new Vector2(dist * 1, 1);
		
	}

	Transform GetClosestPoint(Transform[] points)
	{
		Transform tMin = null;
		float minDist = Mathf.Infinity;
		//Vector3 currentPos = position1.position;
		Vector3 currentPos = transform.position;
		foreach (Transform t in points)
		{
			//if (t.position.x > position1.position.x) //make it not do ones behind it
			if (t.position.x > transform.position.x) //make it not do ones behind it
			{
				float dist = Vector3.Distance(t.position, currentPos);
				if (dist < minDist)
				{
					tMin = t;
					minDist = dist;
				}
			}
		}
		return tMin;
	}
	#endregion


	void FallDeath()
	{
		Destroy(gameObject);
	}

	void PlayerDeath()
	{
		Destroy(gameObject);

	}

	IEnumerator DelayedRespawn(float delay)
	{
		yield return new WaitForSeconds(delay);
		//transform.position = spawnPointObj.transform.position;
	}

	void CrossFinish()
	{


	}

}


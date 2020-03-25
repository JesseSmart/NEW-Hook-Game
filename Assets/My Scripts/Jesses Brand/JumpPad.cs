using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

	public int verticalLaunchForce;
	public int horizontalLaunchForce;

	private Vector2 launchVec;
	private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
		launchVec = new Vector2(horizontalLaunchForce, verticalLaunchForce);
		GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.GetComponent<Rigidbody2D>().AddForce(launchVec);
			anim.SetTrigger("DoJump");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanelManager : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DoRetry()
	{
		gameObject.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


	}

	public void DoContinue()
	{
		gameObject.SetActive(false);
		//Continue
	}

	public void DoQuit()
	{
		gameObject.SetActive(false);
		SceneManager.LoadScene("JesseMainMenu");
	}
}

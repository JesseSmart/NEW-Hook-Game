using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

	public GameObject pnlMain;
	public GameObject pnlLevelSel;
	public GameObject pnlCharSel;
	public GameObject pnlSettings;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void ToLevelSelClicked()
	{
		PanelManager(false, true, false, false);
	}

	public void ToCharSelClicked()
	{
		PanelManager(false, false, true, false);
	}

	public void ToSettingsClicked()
	{
		PanelManager(false, false, false, true);
	}

	public void ToInfinite()
	{
		SceneManager.LoadScene("JesseNewInfinite");
	}



	public void BackToMenuClicked()
	{
		PanelManager(true, false, false, false);
	}

	public void PanelManager(bool mainB, bool levelB, bool charB, bool settingB)
	{
		pnlMain.SetActive(mainB);
		pnlLevelSel.SetActive(levelB);
		pnlCharSel.SetActive(charB);
		pnlSettings.SetActive(settingB);
	}

	public void SelectLevel(int levelNum)
	{
		SceneManager.LoadScene("JesseLevel" + levelNum);
	}

	public void SetCharacter(int i)
	{
		PlayerPrefs.SetInt("CharacterNum", i);
	}

}

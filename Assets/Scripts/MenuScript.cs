using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	public void OnClickMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	
    public void OnClickPong()
	{
		SceneManager.LoadScene("PongSplitScreen");
	}
	
	public void OnClickVoid()
	{
		SceneManager.LoadScene("VoidMenu");
	}
	
	public void OnClickExit()
	{
		Application.Quit();
	}
	
	public void OnClickDebug()
	{
		SceneManager.LoadScene("DebugPSS");
	}
	
	public void OnClickChoiceGame()
	{
		SceneManager.LoadScene("GamSel");
	}
	
	public void OnClickBreakOut()
	{
		SceneManager.LoadScene("lvlSel");
	}
	public void OnClickOptions()
	{
		SceneManager.LoadScene("Options");
	}
	public void OnClickVK()
	{
		Application.OpenURL("https://vk.com/uralfansoft");
	}
	public void OnClickDeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}
	void Update()
	{
		if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
		{
			switch (SceneManager.GetActiveScene().buildIndex)
			{
			  case 0:
				  Application.Quit();
				  break;
			  case 8:
				  SceneManager.LoadScene("GamSel");
				  break;
			  default:
				  SceneManager.LoadScene("MainMenu");
				  break;
			}
		}
	}
}

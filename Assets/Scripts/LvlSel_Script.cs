using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlSel_Script : MonoBehaviour
{
	public GameObject lvl1;
	public GameObject lvl2;
	public GameObject lvl3;
	public GameObject lvl4;
    // Start is called before the first frame update
    void Start()
    {
		lvl1.SetActive(true);
		lvl2.SetActive(false);
		lvl3.SetActive(false);
		lvl4.SetActive(false);
		if(PlayerPrefs.HasKey("curlvl"))
		{
			if(PlayerPrefs.GetInt("curlvl")>=1)
			{
				lvl2.SetActive(true);
				if(PlayerPrefs.GetInt("curlvl")>=2)
				{
					lvl3.SetActive(true);
					if(PlayerPrefs.GetInt("curlvl")>=3)
					{
						lvl4.SetActive(true);
					}
				}
			}
		}
    }

    public void OnClicklvl1()
	{
		SceneManager.LoadScene("lvl1");
	}
	public void OnClicklvl2()
	{
		SceneManager.LoadScene("lvl2");
	}
	public void OnClicklvl3()
	{
		SceneManager.LoadScene("lvl3");
	}
}

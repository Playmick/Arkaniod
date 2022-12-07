using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Acceleration : MonoBehaviour
{
	public Text X;
	public Text Y;
	public Text Z;
	public Toggle acl;
	
	void Start()
	{
		if(!PlayerPrefs.HasKey("accel"))
		{
			PlayerPrefs.SetInt("accel", 0);
		}
		if (PlayerPrefs.GetInt("accel")==1)
		{
			acl.isOn = true;
		}
		else
		{
			acl.isOn = false;
		}
	}
	
	public void AccelerateIsChange()
	{
		if (acl.isOn)
		{
			//Debug.Log("вкл");
			PlayerPrefs.SetInt("accel", 1);
		}else if (!acl.isOn)
			{
				PlayerPrefs.SetInt("accel", 0);
			}
	}
	
    // Update is called once per frame
    void Update()
    {
		
		if (PlayerPrefs.GetInt("accel")==1)
		{
			X.text = "X " + (float)Math.Round((float)Input.acceleration.x, 2, MidpointRounding.AwayFromZero);
			Y.text = "Y " + (float)Math.Round((float)Input.acceleration.y, 2, MidpointRounding.AwayFromZero);
			Z.text = "Z " + (float)Math.Round((float)Input.acceleration.z, 2, MidpointRounding.AwayFromZero);
		}
		else
		{
			X.text = "X";
			Y.text = "Y";
			Z.text = "Z";
		}
		
    }
}

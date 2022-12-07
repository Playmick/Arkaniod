using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TouchControl : MonoBehaviour
{
    public Text X1;
	public Text X0;
	public Text Y1;
	public Text Y0;
	public Text Con;
	
	float px0, px1, py0, py1;
	
	void Start()
	{
		/*Vector2 p0 = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
		Vector2 p1 = new Vector2(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y);*/
		Debug.Log(Camera.main.pixelWidth/2f);
		Debug.Log(Camera.main.pixelHeight/2f);
	}
    void Update()
    {
		
		Con.text = "" + Input.touchCount;
		if (Input.touchCount>0)
		{
			Touch p0 = Input.GetTouch(0);
			
			//Update the Text on the screen depending on current position of the touch each frame
			X0.text = "X0 " + ((p0.position.x - Camera.main.pixelWidth/2f)/(((Camera.main.pixelWidth/2f)-(Camera.main.pixelWidth/4f))/100f));
			Y0.text = "Y0 " + (p0.position.y - Camera.main.pixelHeight/2f);
			
			if (Input.touchCount>1)
			{
				Touch p1 = Input.GetTouch(1);
				X1.text = "X1 " + ((p1.position.x - Camera.main.pixelWidth/2f)/(((Camera.main.pixelWidth/2f)-(Camera.main.pixelWidth/4f))/100f));;
				Y1.text = "Y1 " + (p1.position.y - Camera.main.pixelHeight/2f);
			}
			
		}
		
		/*//Update the Text on the screen depending on current position of the touch each frame
		X0.text = "X0 " + p0.x;
		Y0.text = "Y0 " + p0.y;
		
		X1.text = "X1 " + p1.x;
		Y1.text = "Y1 " + p1.y;*/
		//http://unity3d.ru/distribution/viewtopic.php?f=105&t=21845
    }
}
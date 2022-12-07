using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class GamManag : MonoBehaviour
{
	int i = 0;
    float fps;
	
	public Text FPS;
    
    void Update() 
	{
        fps = (float)Math.Round(1/Time.deltaTime, 0, MidpointRounding.AwayFromZero);
    }
	
	void FixedUpdate()
	{
		i +=1;
		if (i>=25)
		{
			FPS.text = "FPS: " + fps;
			i=0;
		}
	}
	/*public Text scoreH;
	public Text scoreL;
	public byte scL, scH;
    // Start is called before the first frame update
    void Start()
    {
		scH = 0;
		scL = 0;
		
        scoreH.text = "0";
		scoreL.text = "0";
    }*/

}

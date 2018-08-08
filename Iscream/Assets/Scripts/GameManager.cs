using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtility;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
//------------------------------------------------------------------------CONSTANTS:

//---------------------------------------------------------------------------FIELDS:
	public float InputWait = 2, MovementTimer = 3f, WaitTimer = 1f;
	public Text text;

	[System.NonSerialized]
	public float InputTemp, MovementTempTimer, WaitTimerTemp;
	public bool Initiate, End, MovementCompleted = true;
	
	private ParamCube[] direction = new ParamCube[4];
//---------------------------------------------------------------------MONO METHODS:

	void Start() 
	{
		InputTemp = Time.time + InputWait;
		MovementTempTimer =  Time.time + MovementTimer;
		direction = FindObjectsOfType<ParamCube>();
	}
		
	void Update()
    {
		if(Initiate && End)
		{
			Initiate = false;
		}

		if(Initiate && !End)
		{
			text.text = "MAKE NOISE!";
			if(Time.time >= InputTemp)
			{
				End = true;
				MovementTempTimer = Time.time + MovementTimer;
				Initiate = false;
			}
		}
		if(End)
		{
			text.text = "Moving";
			if(Time.time >= MovementTempTimer)
			{
				foreach(ParamCube directions in direction)
				{
					directions.transform.localScale =new Vector3(1,
									1,
									1);
				}
				WaitTimerTemp = Time.time + WaitTimer;
				MovementCompleted = true;
				MakeDecision();
				End = false;
			}
		}
		if(!End && !Initiate)
			text.text = "";
    }

//--------------------------------------------------------------------------METHODS:

//--------------------------------------------------------------------------HELPERS:
	private void MakeDecision()
	{
		float Highestval = 1.3f;
		ParamCube highestDirection = direction[0];
		foreach(ParamCube directions in direction)
		{
			if(Highestval < directions.yAxis)
			{
				Highestval = directions.yAxis;
				highestDirection = directions;
			}
		}
		
		//Move to that direction. Maybe make an enum or something
		if(highestDirection.gameObject.name == "Up" )
		{
			Debug.Log("UP");
		}
		else if(highestDirection.gameObject.name == "Right" )
		{
			Debug.Log("Down");
		}
		else if(highestDirection.gameObject.name == "Left" )
		{
			Debug.Log("Left");
		}
		else//If there's a tie, it goes down
		{
			Debug.Log("Down");
		}
	}
}
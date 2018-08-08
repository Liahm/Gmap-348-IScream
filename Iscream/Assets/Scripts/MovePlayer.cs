using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour 
{
//------------------------------------------------------------------------CONSTANTS:

//---------------------------------------------------------------------------FIELDS:
	public float Speed;
	public bool AllowToMove = false;
	public Rigidbody2D rb;
//---------------------------------------------------------------------MONO METHODS:

	void Start() 
	{

	}
		
	void Update()
    {

    }

//--------------------------------------------------------------------------METHODS:
	public void Movement(string direction)
	{
		if(direction == "Up")
		{
			rb.velocity = new Vector2(0, Speed);

		}
		else if(direction == "Right")
		{
			rb.velocity = new Vector2(Speed, 0);
			
		}
		else if(direction == "Left")
		{
			rb.velocity = new Vector2(-Speed, 0);
			
		}
		else if(direction == "Down")
		{
			rb.velocity = new Vector2(0, -Speed);
			
		}
	}

	public void EndMovement()
	{
		rb.velocity = new Vector2(0, 0);
	}
//--------------------------------------------------------------------------HELPERS:
	
}

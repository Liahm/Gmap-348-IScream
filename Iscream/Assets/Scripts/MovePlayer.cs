using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour 
{
//------------------------------------------------------------------------CONSTANTS:

//---------------------------------------------------------------------------FIELDS:
	public float Speed;
	public Rigidbody2D rb;
	public bool CanMove, CheckOnce;
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
			CheckTile(Vector2.up);
			if(CanMove)
			{
				rb.velocity = new Vector2(0, Speed);
				GameManager.Instance.text.text = "Moving " + direction;
			}
		}
		else if(direction == "Right")
		{
			CheckTile(Vector2.right);
			if(CanMove)
			{
				rb.velocity = new Vector2(Speed, 0);		
				GameManager.Instance.text.text = "Moving " + direction;
			}
		}
		else if(direction == "Left")
		{
			CheckTile(Vector2.left);
			if(CanMove)
			{
				rb.velocity = new Vector2(-Speed, 0);		
				GameManager.Instance.text.text = "Moving " + direction;
			}
		}
		else if(direction == "Down")
		{
			CheckTile(Vector2.down);
			if(CanMove)
			{
				rb.velocity = new Vector2(0, -Speed);		
				GameManager.Instance.text.text = "Moving " + direction;
			}
		}
	}

	public void EndMovement()
	{
		rb.velocity = new Vector2(0, 0);
		CanMove = false;
		CheckOnce = false;
	}

	public void CheckTile(Vector2 direction)
	{
		if(!CheckOnce)
		{
			RaycastHit2D[] allHits;
			allHits = Physics2D.RaycastAll(transform.position, direction);
			
			Debug.Log(allHits.Length);
			if(allHits.Length > 2)
			{
				CanMove = true;
				CheckOnce = true;
			}
			else
			{
				GameManager.Instance.text.text = "Can't go that direction";
				CanMove = false;
				CheckOnce = true;
			}
		}
	}
//--------------------------------------------------------------------------HELPERS:
	
}

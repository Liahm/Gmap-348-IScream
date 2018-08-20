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

    public Animator anim;
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
                anim.SetInteger("stage", 1);
            }
		}
		else if(direction == "Right")
		{
			CheckTile(Vector2.right);
			if(CanMove)
			{
				rb.velocity = new Vector2(Speed, 0);		
				GameManager.Instance.text.text = "Moving " + direction;
                anim.SetInteger("stage", 2);
            }
		}
		else if(direction == "Left")
		{
			CheckTile(Vector2.left);
			if(CanMove)
			{
				rb.velocity = new Vector2(-Speed, 0);		
				GameManager.Instance.text.text = "Moving " + direction;
                anim.SetInteger("stage", 4);
            }
		}
		else if(direction == "Down")
		{
			CheckTile(Vector2.down);
			if(CanMove)
			{
				rb.velocity = new Vector2(0, -Speed);		
				GameManager.Instance.text.text = "Moving " + direction;
                anim.SetInteger("stage", 3);
            }
		}
        else
        {
            anim.SetInteger("stage", 0);

        }
	}

	public void EndMovement()
	{
		rb.velocity = new Vector2(0, 0);
        anim.SetInteger("stage", 0);
        CanMove = false;
		CheckOnce = false;
		GameManager.Instance.directionWinner = "Down";
	}

	public void CheckTile(Vector2 direction)
	{
		if(!CheckOnce)
		{
			RaycastHit2D[] allHits;
			allHits = Physics2D.RaycastAll(transform.position, direction,1f);
			
			Debug.DrawRay(transform.position, direction);
			Debug.Log(allHits.Length);

			foreach(var hits in allHits)
			{
				Debug.Log(hits.transform.gameObject.name);
			}

			if(allHits.Length > 2)
			{
				CanMove = true;
				CheckOnce = true;
				GameManager.Instance.Moves++;
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

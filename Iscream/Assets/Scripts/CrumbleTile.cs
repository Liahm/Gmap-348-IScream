using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbleTile : MonoBehaviour 
{
//------------------------------------------------------------------------CONSTANTS:

//---------------------------------------------------------------------------FIELDS:
	public float Timer;
	public Sprite CrumbledTile;
//---------------------------------------------------------------------MONO METHODS:

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			//play animation
			StartCoroutine(Crumbling());
		}
	}

//--------------------------------------------------------------------------METHODS:

//--------------------------------------------------------------------------HELPERS:
	IEnumerator Crumbling()
	{
		yield return new WaitForSeconds(Timer);	
		//transform.gameObject.SetActive(false);
		this.gameObject.GetComponent<SpriteRenderer>().sprite = CrumbledTile;
	}
}
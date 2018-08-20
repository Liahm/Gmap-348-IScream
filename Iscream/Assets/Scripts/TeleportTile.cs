using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTile : MonoBehaviour 
{
//------------------------------------------------------------------------CONSTANTS:

//---------------------------------------------------------------------------FIELDS:
	public GameObject EndTilePosition;
	public float Timer = 2;
	public bool Active;
	private GameObject player;
//---------------------------------------------------------------------MONO METHODS:

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			if(Active)
			{
				player = col.gameObject;
				StartCoroutine(Teleport());
			}
		}
	}

//--------------------------------------------------------------------------METHODS:

//--------------------------------------------------------------------------HELPERS:
	IEnumerator Teleport()
	{
		yield return new WaitForSeconds(Timer);
		if(player != null)
		{
			player.transform.position = EndTilePosition.transform.position;
		}
	}
}
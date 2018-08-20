using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour 
{
//------------------------------------------------------------------------CONSTANTS:

//---------------------------------------------------------------------------FIELDS:
	public TeleportTile ActivateTeleporter;
//---------------------------------------------------------------------MONO METHODS:

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			ActivateTeleporter.Active = true;
		}
	}

//--------------------------------------------------------------------------METHODS:

//--------------------------------------------------------------------------HELPERS:
	
}
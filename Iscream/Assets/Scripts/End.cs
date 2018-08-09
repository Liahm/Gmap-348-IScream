using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class End : MonoBehaviour 
{
//------------------------------------------------------------------------CONSTANTS:

//---------------------------------------------------------------------------FIELDS:
	public Image Victory;
	public Button NextLevel;
//---------------------------------------------------------------------MONO METHODS:

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			if(Victory != null)	
				Victory.gameObject.SetActive(true);
			NextLevel.gameObject.SetActive(true);
		}
	}

//--------------------------------------------------------------------------METHODS:

//--------------------------------------------------------------------------HELPERS:
	
}
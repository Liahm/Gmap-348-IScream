using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParamCube : MonoBehaviour 
{
//------------------------------------------------------------------------CONSTANTS:

//---------------------------------------------------------------------------FIELDS:
	public int Band;
	public float StartScale, ScaleMultiplier, MaxValue;
	public bool Buffer;
	public Material InitialMaterial, ActiveColor, SelectedColor;

	[System.NonSerialized]
	public float yAxis, yAxis2;
	private AudioManager aM;
	private GameObject child;
	private Renderer rend;
//---------------------------------------------------------------------MONO METHODS:

	void Start() 
	{
		aM = FindObjectOfType<AudioManager>();
		child = transform.GetChild(0).gameObject;
		rend = child.GetComponent<Renderer>();
	}
		
	void Update()
    {
		if(float.IsNaN(aM.AudioBandBuffer[Band]))	aM.AudioBandBuffer[Band] = 0;
		if(Buffer)
		{
			yAxis = (aM.AudioBandBuffer[Band] * ScaleMultiplier) + StartScale;
			if(yAxis > MaxValue)
				yAxis = MaxValue;
			transform.localScale = new Vector3(transform.localScale.x,
									yAxis,
									transform.localScale.z);
		}
		else //Technically speaking, we never use the audioband without buffer
		{
			yAxis2 = (aM.AudioBand[Band] * ScaleMultiplier) + StartScale;
			if(yAxis2 > MaxValue)
				yAxis2 = MaxValue;
			transform.localScale = new Vector3(transform.localScale.x,
									yAxis2,
									transform.localScale.z);
		}

		if(transform.localScale.y >= 1.3 && GameManager.Instance.MovementCompleted)
		{
			if(Time.time >= GameManager.Instance.WaitTimerTemp)
			{
				GameManager.Instance.InputTemp = Time.time  
												+ GameManager.Instance.InputWait;
				GameManager.Instance.Initiate = true;
				GameManager.Instance.MovementCompleted = false;								
			}
		}

		if(transform.localScale.y >= 2 && transform.localScale.y < 3)
		{
			rend.material = ActiveColor;
		}
		else if(transform.localScale.y >= 3)
		{
			rend.material = SelectedColor;

		}
		else
		{
			rend.material = InitialMaterial;
		}
    }

//--------------------------------------------------------------------------METHODS:

//--------------------------------------------------------------------------HELPERS:
	
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : MonoBehaviour {

    public float startScale;
    public float bigScale;
    public float loopTime;

    private bool grow = true;
    private float half;

	// Use this for initialization
	void Start () {
        half = loopTime / 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

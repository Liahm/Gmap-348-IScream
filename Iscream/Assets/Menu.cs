using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public int levelOneBuildIndex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(levelOneBuildIndex);

    }

    public void Quit()
    {
        Application.Quit();
    }
}

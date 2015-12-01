using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	static LevelManager instance = null;
	private AIDirector aid = null;
	public float startTime = 0f;
	public float timeElapsed = 0f;
	public int agentsAttempted = 0;
	public string agentName = "";
	
	// Use this for initialization
	void Start () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}

		startTime = Time.time;
		aid = GameObject.FindObjectOfType<AIDirector>();
	}

	public void LoadLevel(string name){
		Application.LoadLevel (name);
	}

	public void LoadNextLevel(string nameOfAgent){
		agentName = nameOfAgent;
		agentsAttempted = aid.getAgentAttempts();
		timeElapsed = Time.time - startTime;
		Application.LoadLevel (Application.loadedLevel + 1);
	}
}

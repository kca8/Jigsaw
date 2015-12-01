using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFiller : MonoBehaviour {

	private LevelManager levelManager = null;
	public Text timeText;
	public Text attemptText;
	public Text nameText;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();

		timeText.text = levelManager.timeElapsed.ToString();
		attemptText.text = levelManager.agentsAttempted.ToString();
		string name = levelManager.agentName.ToString();
		nameText.text = name.Split('(')[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

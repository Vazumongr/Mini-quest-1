using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour {

	private UIManagerScript _uiManagerScript;
	private GameManagerScript _gameManagerScript;

	// Use this for initialization
	void Start () {

		_uiManagerScript = GameObject.Find("Canvas").GetComponent<UIManagerScript>();
		_gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

		NullCheck(_uiManagerScript,"_uiManagerScript");
		NullCheck(_gameManagerScript,"_gameManagerScript");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*	In hindsight, I should probably make a seperate script for my zones and
		a seperate script for my MazeWalls. I don't remember why I decided
		to do it this way, but if you're reading this, I never got around to fixing it.
		This doesn't cause any problems, it's just unecessary to share the script
		between the goals and the walls since they don't really do much of the same.
	*/
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			switch (tag)
			{
				case "StartPlatform":
					_uiManagerScript.StartCoroutine("Timer");
					break;
				case "EndPlatform":
					_gameManagerScript.SetGameOver();
					break;
				case "MazeWall":
				
					PlayerScript playerScript = other.GetComponent<PlayerScript>();
					NullCheck(playerScript,"playerScript");
					playerScript.ReduceHealth();
					break;
				default:
					Debug.Log("THIS OBJECT IS UNTAGGED");
					break;
			}
		}
	}

	void NullCheck<T>(T t, string str)
	{
		if(t.Equals(null))
		{
			Debug.LogError(this.GetType().Name + "::" + str + " is NULL");
		}
	}
}

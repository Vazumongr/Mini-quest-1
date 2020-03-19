using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This class is meant to control the state of my game.
public class GameManagerScript : MonoBehaviour {


	PlayerScript _playerScript;
	UIManagerScript _uiManagerScript;
	ZoneScript _zoneScript;
	void Start()
	{
		_playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
		_uiManagerScript = GameObject.Find("Canvas").GetComponent<UIManagerScript>();
		_zoneScript = GameObject.Find("End").GetComponent<ZoneScript>();

		NullCheck(_playerScript,"_playerScript");
		NullCheck(_uiManagerScript,"_uiManagerScript");
		NullCheck(_zoneScript,"_zoneScript");

		
		
	}

	public void SetGameOver()
	{
		//Disable character movement
		_playerScript.SetGameOver();
		//Display gameOver text
		_uiManagerScript.SetGameOver();
	}

	void NullCheck<T>(T t, string str)
	{
		if(t.Equals(null))
		{
			Debug.LogError(this.GetType().Name + "::" + str + " is NULL");
		}
	}

	public void ResetGame()
	{
		SceneManager.LoadScene(0);
	}
}

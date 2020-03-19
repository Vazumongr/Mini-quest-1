using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {

	[SerializeField]
	private Text _playerHealthText,_timerText,_gameOverText;
	private int _seconds = 0;
	private int _minutes = 0;


	// Use this for initialization
	void Start () {

		/* See the NullCheck<T> method for explanation
		if(_playerHealthText == null)
			Debug.LogError("UIManagerScript::_playerHealthText is NULL");
		if(_timerText == null)
			Debug.LogError("UIManagerScript::_timerText is NULL");
		*/
		NullCheck(_playerHealthText,"_playerHealthText");
		NullCheck(_timerText,"_timerText");
		NullCheck(_gameOverText,"_gameOverText");
	}

	/*	I'm happy with this. So as you can see above, that was my method
		of nullchecking any thing I needed to reference. I got tired of typing
		that for every single object I need to call. For a small project like this,
		it isn't too bad doing it three or four times, but in larger projects,
		I can easily be trying to call dozens of objects and don't want to have 
		to do nullChecking that way. So I created this Generic method to do it
		for me. Thank goodness for Dr. Golshan covering generics. I hope
		to learn how to add this as a namespace so I can use it across all my C# scripts
		without having to have this method inside the script. (like using UnityEngine).
		---Developers Note: Need to find a more effecient means of calling this method.
		---Develoeprs SideNote: Remember to use 'nameof' in C# versions >= C#6
	*/
	void NullCheck<T>(T t, string str)
	{
		if(t.Equals(null))
		{
			Debug.LogError(this.GetType().Name + "::" + str + " is NULL");
		}
	}
	
	// Update is called once per frame
	public void UpdateHealth (int count) {
		_playerHealthText.text = "Player Health: " + count;
	}

	public void DisplayGameOverText()
	{
		_gameOverText.enabled = true;
	}

	public void SetGameOver()
	{
		DisplayGameOverText();
		StopCoroutine("Timer");
	}

	public IEnumerator Timer()
	{
		while(true)
		{
			yield return new WaitForSeconds(1f);
			if(++_seconds >= 60)
			{
				_minutes++;
				_seconds = 0;
			}
			if(_seconds<10)
				_timerText.text = "Time: "+_minutes+":0"+_seconds;
			else
				_timerText.text = "Time: "+_minutes+":"+_seconds;
		}
	}
}

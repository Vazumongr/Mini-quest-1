using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	private CharacterController _playerController;
	[SerializeField]
	private ParticleSystem _particleSystem;
	[SerializeField]
	private float _speed = 10f;
	[SerializeField]
	private int _health = 5;
	private UIManagerScript _uiManagerScript;
	private GameManagerScript _gameManagerScript;
	private Vector3 _velocity;
	private Vector3 _oldVel;
	private bool _playerCanMove = true;

	// Use this for initialization
	void Start () {
		
		_playerController = GetComponent<CharacterController>();

		_uiManagerScript = GameObject.Find("Canvas").GetComponent<UIManagerScript>();
		_gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

		NullCheck(_playerController,"_playerController");
		NullCheck(_uiManagerScript,"_uiManagerScript");
		NullCheck(_gameManagerScript,"_gameManagerScript");
		NullCheck(_particleSystem,"_particleSystem");
	}
	
	// Update is called once per frame
	void Update () {
		if(_playerCanMove)
			CalculateMovement();
		if(!_playerCanMove && Input.GetKeyDown(KeyCode.R))
			_gameManagerScript.ResetGame();
	}

	void CalculateMovement()
	{
		//Simply gets the players input
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		//This is the vector that will move the player, by default we do not move
		_velocity = new Vector3(0,0,0);

		/*
		So the neat thing about CharacterControllers is that they can detect when there is an object directly
		underneath them. This isGrounded property lets me easily check if I need to fall or not.
		For the purpose of this game, it's strictly for visual purpose on the beginning in
		order to provice a cool "falling in" effect.
		The if statement checks if we are grounded and if we are, we do not want to move along the y-axis 
		at all, only along the x and z axis'.
		The else is for when we are not grounded and we only want to move along the y-axis, and NOT the 
		x and z axis'.
		*/
		if(_playerController.isGrounded)
		{
			_velocity = _speed * (new Vector3(horizontal,-.5f,vertical));
		}
		else
		{
			_velocity = _speed * (new Vector3(0,-.5f,0));
		}
		
		//The Time.deltaTime is used in the move function to help with smoother moving using characterController. Little trick I learned from GameDevHQ.
		_playerController.Move(_velocity * Time.deltaTime);

		_oldVel = _velocity;
	}
	
	//Reduces health and asks the UIManager to update the display
	public void ReduceHealth()
	{
		
		_particleSystem.Play();
		_uiManagerScript.UpdateHealth(--_health);
		BounceBack();
		if(_health == 0)
		{
			_uiManagerScript.DisplayGameOverText();
			_gameManagerScript.SetGameOver();
		}
	}

	/*	This is to bounce the player away from the wall when they bump into it.
		This is done by storing my velocity at the end of every frame, then 
		referencing that variable when I collide with a wall so I can bounce
		back the way I came.
	*/
	public void BounceBack()
	{
        Vector3 invVelocity = _oldVel * -1;
        invVelocity = invVelocity * _speed;

		//_velocity = _speed * -1f * _oldVel;
		invVelocity.y = -.5f;
		_playerController.Move(invVelocity * Time.deltaTime);
        Debug.Log(invVelocity);
	}

	//Used largely for debugging purposes
	public void PrintVelocity()
	{
		Debug.Log(_playerController.velocity);
	}

	public Vector3 GetVelocity()
	{
		return _velocity;
	}
	
	void NullCheck<T>(T t, string str)
	{
		if(t.Equals(null))
		{
			Debug.LogError(this.GetType().Name + "::" + str + " is NULL");
		}
	}
	
	public void SetGameOver()
	{
		_playerCanMove = false;
	}
}

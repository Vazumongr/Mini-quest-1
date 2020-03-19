using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeContainerScript : MonoBehaviour {

	[SerializeField]
	private Material material1;
	// Use this for initialization
	void Start () {
		FixChildren();
	}

	/*	So I am lazy. I didn't feel like adding a script, then a collider, and then setting
	*	that collider to a trigger on every maze wall I make (especially when changing the maze).
	*	So I use my MazeContainer (which was made to keep my Heirarchy tidy) to do it for me :D
	*	This method will cycle through all the children of the MazeContainer, give them the desired
	*	script, then give them a BoxCollider that is a trigger. 
	*/
	void FixChildren()
	{
		//Number of children
		int childCount = this.transform.childCount;
		for(int i = 0; i < childCount; i++)
		{
			//My designated child
			Transform child = this.transform.GetChild(i);
			child.gameObject.AddComponent<ZoneScript>();
			//Adds the BoxCollider to the child and stores the reference so I can make it a trigger.
			BoxCollider boxCol = child.gameObject.AddComponent<BoxCollider>() as BoxCollider;
            boxCol.isTrigger = true;
            child.gameObject.AddComponent<Rigidbody>();
            //Adds the rigidbody and constraints rotation and movement so they only fall
            Rigidbody rigidbody = child.GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            //Tag needs set for collision handling
            child.tag = "MazeWall";
            //Gives it the material I want.
			Renderer rend = child.GetComponent<Renderer>();
            if (rend.material.name == "Default-Material (Instance)")
                rend.material = material1;

		}
	}
}

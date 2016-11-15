using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 10.0F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HorizontalMovement ();
	}

	private void HorizontalMovement()
	{
		float translation = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		transform.Translate(translation, 0, 0);
	}
}

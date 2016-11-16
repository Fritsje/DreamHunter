using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float _speed = 10.0f;
	[SerializeField]
	private float _gravity = 0.35f;
	[SerializeField]
	private float _jumpHeight = 5.0f;

	private bool _canJump = true;
	private bool _jumping = false;
	float lerp = 0;
	Vector2 jumpTarget;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		HorizontalMovement ();
		Jumping ();
	}

	private void HorizontalMovement()
	{
		float translation = Input.GetAxis("Horizontal") * _speed;
		translation *= Time.deltaTime;
		transform.Translate(translation, 0, 0);
	}

	private void Jumping()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, -Vector2.up, 1f);
		if (hit.collider != null) {
			_canJump = true;
		} else if (hit.collider == null && !_jumping) {
			_canJump = false;
			Gravity ();
		}

		if (_canJump) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				_jumping = true;
				jumpTarget = new Vector2 (transform.position.x, transform.position.y + _jumpHeight);
			}
		}

		if (_jumping) {
			Vector2 tempPos = new Vector2 (transform.position.x, transform.position.y);

			if (tempPos.y < jumpTarget.y) {
				tempPos.y += _gravity;
				transform.position = tempPos;
			} else {
				_jumping = false;
			}
		}
	}

	private void Gravity()
	{
		Vector2 newPos = new Vector2 (transform.position.x, transform.position.y - _gravity);
		transform.position = newPos;
	}
}

using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private GameObject _player;
	private float _playerDistance;

	private bool isAttacking;

	void Start () {
		_player = new GameObject ();
		isAttacking = false;
	}

	void Update () {
		AnimationHandler ();
		CheckPlayerLocation ();


		if (_playerDistance < 9f && !isAttacking) {
			AttackPlayer ();
		} else {
			ChasePlayer ();
		}

	}

	void AttackPlayer(){
		if(_playerDistance <2f) Debug.Log ("Swings his mighty sword at player");
		if(_playerDistance <9f) Debug.Log ("Throws rock at player");
		isAttacking = true;
	}

	void ChasePlayer(){
		if(!isAttacking) transform.position += transform.forward*Time.deltaTime;
	}

	void AnimationHandler(){
		//when an attacking animation is not playing
		isAttacking = false;
	}

	void CheckPlayerLocation(){
		_player.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)) ;
		_playerDistance = Vector3.Distance(_player.transform.position, gameObject.transform.position);

		//if enemy looks left or right
		if (_player.transform.position.x > gameObject.transform.position.x) {
			gameObject.transform.localScale = new Vector3(0.5f,1f,1f);
			//looks right
		} else {
			gameObject.transform.localScale = new Vector3(1f,-1f,1f);
			//looks left
		}
	}
}

using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private GameObject _player;
	private float _playerDistance;

	private int _health;
	private bool _isAttacking; 		//if attack animation is playing
	private float _attackCooldown; 	//cooldown before attacking again after the animation is done playing
	private bool _playerInRange;

	public GameObject projectile;



	void Start () {
		_player = new GameObject ();
		_attackCooldown = 0f;
		_isAttacking = false;
		_health = 10;
		_playerInRange = false;
	}

	void Update () {
		AnimationHandler ();
		CheckPlayerLocation ();
		ChasePlayer ();

		if (_playerDistance < 9f && _attackCooldown <= 0f) {
			AttackPlayer ();
		} 

	}

	void AttackPlayer(){
		if (_playerDistance < 2f) {
			if (_playerInRange) {
				Debug.Log ("Swings his mighty sword at player and hits");
			} else {
				Debug.Log ("Swings his mighty sword at player and misses..");
			}
			_attackCooldown = 1f;
			_isAttacking = true;
		} else if (_playerDistance < 9f && _playerDistance >= 2f) { 
			Debug.Log ("Throws rock at player");
			_attackCooldown = 2f;
			_isAttacking = true;

			GameObject proj = Instantiate (projectile, transform.position+transform.up*2f, Quaternion.identity) as GameObject;
			proj.GetComponent<Rigidbody2D> ().velocity = (transform.right*5f) * transform.localScale.x;
		}
	}

	void ChasePlayer(){
		if (!_isAttacking) {
			Vector3 dir = new Vector3(_player.transform.position.x, 0,0) - new Vector3(transform.position.x,0,0);
			if (dir.magnitude > 1f) {
				transform.position += (dir).normalized * Time.deltaTime;
			}
		}
	}

	void AnimationHandler(){
		//when an attacking animationm is finished
		_isAttacking = false;
		_attackCooldown -= 1f*Time.deltaTime;
	}

	void CheckPlayerLocation(){
		_player.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)) ;
		_playerDistance = Vector3.Distance(_player.transform.position, gameObject.transform.position);

		if (!_isAttacking) {
			//if player is left or right of the enemy
			if (_player.transform.position.x > gameObject.transform.position.x) {
				gameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
				//looks right
			} else {
				gameObject.transform.localScale = new Vector3 (-1f, 1f, 1f);
				//looks left
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		//if (coll.gameObject == _player) {
			_playerInRange = true;
		//}
	}

	void OnTriggerExit2D(Collider2D coll){
		//if (coll.gameObject == _player) {
			_playerInRange = false;
		//}
	}

}

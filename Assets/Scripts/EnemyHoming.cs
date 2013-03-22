using UnityEngine;
using System.Collections;

public class EnemyHoming : Enemy{
	
	private Transform _target;
	private float _speed = 20; //Speed at which it will pursue. 
	
	// Use this for initialization
	protected  override void  Awake () {
		//initialize and hide.
		base.Awake();		
	}
	
	// Update is called once per frame
	protected override void  Update () {
		//move the enemy, if it's alive.
		if(IsAlive()){
			Vector3 dir = _target.position - transform.position;
			this.transform.position = Vector3.Lerp(this.transform.position,this.transform.position+(dir *_speed), Time.fixedDeltaTime);
		}
	}
	
	public void SetSpeed(float speed){
		_speed = speed;	
	}
	
	public float GetSpeed(){
		return _speed;	
	}
	
	public void Spawn(Vector3 startpos, Transform target){
		transform.position = startpos;
		_target = target;
		base.Spawn();
	}
	
	public void Spawn(Vector3 startpos, Transform target, float speed){
		transform.position = startpos;
		_target = target;
		_speed = speed;
		base.Spawn();
	}
}

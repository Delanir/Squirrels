using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour {
	
	
	protected float _speed;
	protected Vector3 _dir; 
	protected bool _alive;
	
	public event Action<Enemy> Spawned;
	public event Action<Enemy> Destroyed;
	
	// Use this for initialization
	protected virtual void Awake() {
		//initialize and hide.
		_alive = false;
		_dir = new Vector3(0,0,0); 
		_speed = 0;
		this.gameObject.renderer.enabled = false;
		this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, this.transform.position.z*-1);
		/* FOR TESTING*/
		//this.Spawn(new Vector3(this.transform.position.x,this.transform.position.y, this.transform.position.z*-1), new Vector3(-1,0,0),20);
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		//move the enemy, if it's alive.
		if(IsAlive()){
			//Linear movement.
			this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position+(_dir*_speed), Time.fixedDeltaTime);
		}
		
		//TODO: Check where enemy is. Behave accordingly.
	}
	
	
	//Spawn the Enemy
	public void Spawn(){
		_alive = true;
		this.gameObject.renderer.enabled = true;
	}
	
	//Spawn the Enemy
	public void Spawn(Vector3 startpos, Vector3 dir, float speed){
		_dir = dir;
		_speed = speed;
		_alive = true;
		this.gameObject.renderer.enabled = true;
		this.transform.position = startpos;
	}
	
	public bool IsAlive(){
		return _alive;	
	}
	
	public void OnCollisionEnter(Collision collision) {
		Acorn acorn = (Acorn) collision.gameObject.GetComponent<Acorn>();
		if(acorn != null){
			this.HitAcorn (acorn);
			return;
		}
		
		Allied_Entity enemy = (Allied_Entity) collision.gameObject.GetComponent<Allied_Entity>();
		if(enemy != null){
			this.HitEnemy (enemy);
			return;
		}
		
	}
	
	protected virtual void HitAcorn(Acorn collider){
		//TODO: Add Enemy only related stuff. Enemy will take care of itself.
		this.Destroy();
	}
	
	protected virtual void HitEnemy(Allied_Entity collider){
		//TODO: Add Enemy only related stuff. Enemy will take care of itself.
		this.Destroy();
	}
	
	public void Destroy(){
		//Don't actually destroy the gameobject, simply hide it for later use. 
		_alive = false;
		this.gameObject.renderer.enabled = false;
		this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, this.transform.position.x*-1);
	}
	
	protected void BeenSpawned(Enemy spawned){
		this.Spawned(spawned);
	}
	
	protected void BeenDestroyed(Enemy destroyed){
		this.Destroyed(destroyed);
	}
}

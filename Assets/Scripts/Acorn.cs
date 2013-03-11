using UnityEngine;
using System.Collections;
using System;

public class Acorn : MonoBehaviour {
		
	protected float _speed;
	protected Vector3 _dir; 
	protected bool _alive;
	
	public event Action<Acorn> Spawned;
	public event Action<Acorn> Destroyed;
	
	
	// Use this for initialization
	protected virtual void Awake () {
		//initialize and hide.
		_alive = false;
		_dir = new Vector3(0,0,0); 
		_speed = 0;
		this.gameObject.renderer.enabled = false;
		this.enabled = false;
		this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, this.transform.position.z*-1);	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		//move the acorn, if it's alive.
		if(IsAlive())
			this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position+(_dir*_speed), Time.fixedDeltaTime);
		
		foreach(GameObject bounds in GameObject.FindGameObjectsWithTag("MapBounds")){
			if(this.collider.bounds.Intersects(bounds.collider.bounds)){
				if(bounds.name.Contains("Top") || bounds.name.Contains("Bot")){
					_dir.y*=-1;
				}
				else{
					this.Destroy();	
				}
				break;
			}
		}
		
		//TODO: Check where acorn is. If it's outside the screen, Kill it. 
	}
	
	//Spawn the acorn
	public void Spawn(Vector3 startpos, Vector3 dir, float speed){
		_dir = dir;
		_speed = speed;
		_alive = true;
		this.gameObject.renderer.enabled = true;
		this.transform.position = startpos;
		this.BeenSpawned();
		this.enabled=true;
	}
	
	public bool IsAlive(){
		return _alive;	
	}
	
	protected virtual void OnCollisionEnter(Collision collision) {
		Enemy enemy = (Enemy) collision.gameObject.GetComponent<Enemy>();
		if(enemy != null){
			this.HitEnemy (enemy);
		}
	}
	
	protected virtual void HitEnemy(Enemy collider){
		//TODO: Add Acorn only related stuff. Enemy will take care of itself.
		this.Destroy();
	}
	
	public void Destroy(){
		//Don't actually destroy the gameobject, simply hide it for later use. 
		_alive = false;
		this.gameObject.renderer.enabled = false;
		this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, this.transform.position.x*-1);
		this.BeenDestroyed();
		this.enabled=false;
	}
	
	protected void BeenSpawned(){
		this.Spawned(this);
	}
	
	protected void BeenDestroyed(){
		this.Destroyed(this);
	}
}

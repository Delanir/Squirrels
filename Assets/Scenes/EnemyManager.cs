using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	LinkedList<Enemy> _enemiesAlive;
	LinkedList<Enemy> _enemiesDead;
	float _levelTime = 0;
	int _enemiesKilled;
	
	// Use this for initialization
	void Start () {
		_enemiesAlive = new LinkedList<Enemy>();
		_enemiesDead = new LinkedList<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
		_levelTime += UnityEngine.Time.fixedDeltaTime;
	}
	
	public void InitializeLevel(/*level parameters*/){
		//initialize everything necessary for this level
	}
	
	private Enemy SpawnEnemy(Vector3 startpos, Vector3 dir, float speed){
		Enemy toreturn = null;
		if(_enemiesDead.Count>0){
			return _enemiesDead.First.Value;
			return toreturn;
		}
		else{
			return CreateNewEnemy(startpos, dir,speed);	
		}
	}
	
	public Enemy GetEnemy(){
		//TODO: Figure this out.
		Enemy toreturn = null;
		return toreturn;
	}
	
	public int GetDeadEnemies(){
		return _enemiesKilled;
	}
	
	public int GetEnemiesOnScreen(){
		return _enemiesAlive.Count;
	}
	
	//Creation & Destruction services(Not working for as long as we don't have a prefab).
	public Enemy CreateNewEnemy(){
		//create from prefab. 
		GameObject newAcorn = (GameObject) Resources.Load("Prefabs/Enemy");
		Enemy toreturn = newAcorn.GetComponent<Enemy>();
		_enemiesAlive.AddLast(toreturn);
		toreturn.Spawned += EnemySpawned;
		toreturn.Destroyed += EnemyDestroyed;
		return toreturn;
	}
	public Enemy CreateNewEnemy(Vector3 startpos, Vector3 dir, float speed){
		//create from prefab. 
		GameObject newEnemy = (GameObject) Resources.Load("Prefabs/Enemy");
		Enemy toreturn = newEnemy.GetComponent<Enemy>();
		toreturn.Spawn(startpos,dir,speed);
		_enemiesAlive.AddLast(toreturn);
		toreturn.Spawned += EnemySpawned;
		toreturn.Destroyed += EnemyDestroyed;
		return toreturn;
	}
	
	private void EnemySpawned(Enemy spawned){
		_enemiesDead.Remove(spawned);
		_enemiesAlive.AddLast(spawned);
	}
	
	private void EnemyDestroyed(Enemy destroyed){
		_enemiesAlive.Remove(destroyed);
		_enemiesDead.AddLast(destroyed);
		_enemiesKilled++;
	}
}

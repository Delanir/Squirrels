using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType{
	Standard,
	Pattern,
	Homing
}

public class EnemyManager : MonoBehaviour {
	
	/// <summary>
	/// Dictionary referencing all lists of enemies. Each pair being the list of acorns in use and the list of acorns available. 0 and 1 respectively. 
	/// </summary>
	Dictionary<EnemyType,LinkedList<Enemy>[]> _enemiesList;
	float _levelTime = 0;
	int _enemiesKilled;
	
	// Use this for initialization
	void Start () {
		_enemiesList = new Dictionary<EnemyType, LinkedList<Enemy>[]>();
		LinkedList<Enemy> []enemies = new LinkedList<Enemy>[2];
		enemies[0] = new LinkedList<Enemy>();
		enemies[1] = new LinkedList<Enemy>();
		_enemiesList.Add(EnemyType.Standard, enemies);
		enemies = new LinkedList<Enemy>[2];
		enemies[0] = new LinkedList<Enemy>();
		enemies[1] = new LinkedList<Enemy>();
		_enemiesList.Add(EnemyType.Pattern, enemies);
		enemies = new LinkedList<Enemy>[2];
		enemies[0] = new LinkedList<Enemy>();
		enemies[1] = new LinkedList<Enemy>();
		_enemiesList.Add(EnemyType.Homing, enemies);
		InitializeLevel(); //currently for testing.	
		//TODO: Perhaps load level specification for an initial number of enemies available. To limit real-time generation.
	}
	
	// Update is called once per frame
	void Update () {
		_levelTime += UnityEngine.Time.fixedDeltaTime;
	}
	
	public void InitializeLevel(/*level parameters*/){
		//initialize everything necessary for this level
		
		/** Testing Enemy Pattern **/
		EnemyPattern enemypattern = (EnemyPattern) GetEnemyForUse(EnemyType.Pattern);
		List<Vector3> checkpoints = new List<Vector3>();
		checkpoints.Add(new Vector3(-76.94447f, -30.57951f,-5));
		checkpoints.Add(new Vector3(-76.94447f,65.17863f,-5));
		checkpoints.Add(new Vector3(102.9201f,65.17863f,-5));
		checkpoints.Add(new Vector3(102.9201f,-50.38702f,-5));
		checkpoints.Add(new Vector3(171.0704f,-50.38702f,-5));
		enemypattern.Spawn(new Vector3(-76.94447f, -30.57951f,-5),checkpoints,PatternCheckpointType.Positions,10);
		
		/** Testing Enemy Homing **/
		EnemyHoming enemyhoming = (EnemyHoming) GetEnemyForUse(EnemyType.Homing);
		enemyhoming.Spawn(new Vector3(141.2f,-57.6f,-5),GameObject.FindGameObjectWithTag("AlliedEntity").transform,0.4f);
	}
	
	private Enemy SpawnEnemy(Vector3 startpos, Vector3 dir, float speed, EnemyType type){
		Enemy toreturn = null;
		if(_enemiesList[type][1].Count>0){
			return _enemiesList[type][1].First.Value;
			return toreturn;
		}
		else{
			return CreateNewEnemy(startpos, dir,speed, type);	
		}
	}
	
	public Enemy GetEnemyForUse(EnemyType type){
		if(_enemiesList[type][1].Count>0){
			return _enemiesList[type][1].First.Value;
		}
		else{
			return CreateNewEnemy(type);	
		}
		
	}
	
	public int GetTotalKilledEnemies(){
		return _enemiesKilled;
	}
	
	public int GetEnemiesOnScreen(){
		int toreturn = 0;
		foreach(KeyValuePair<EnemyType,LinkedList<Enemy>[]> pair in _enemiesList){
			toreturn += pair.Value[1].Count;
		}
		return toreturn;
	}
	
	public int GetDeadEnemies(){
		int toreturn = 0;
		foreach(KeyValuePair<EnemyType,LinkedList<Enemy>[]> pair in _enemiesList){
			toreturn += pair.Value[0].Count;
		}
		return toreturn;
	}
	
	public int GetDeadEnemies(EnemyType type){
		return _enemiesList[type][1].Count;
	}
	
	public int GetEnemiesOnScreen(EnemyType type){
		return _enemiesList[type][0].Count;
	}
	
		/// <summary>
	/// Are any type of enemy available(Not in game ones)?
	/// </summary>
	/// <returns>
	/// The types of enemiess that are available in a continuous string (use .contains(type of acorn) to triage it).
	/// </returns>
	public string AreEnemiesAvailable(){
		string toreturn= "";
		foreach(KeyValuePair<EnemyType,LinkedList<Enemy>[]> pairvalue in _enemiesList){
			if(pairvalue.Value[1].Count>0)
				toreturn +=pairvalue.Key.ToString();
		}
		if(toreturn.Equals(""))
			return "none";
		else{
			return toreturn;	
		}
	}
	
		/// <summary>
	/// Are any type of enemy in game (spawned and physically alive)?
	/// </summary>
	/// <returns>
	/// The types of enemies that are in game in a continuous string (use .contains(type of acorn) to triage it).
	/// </returns>
	public string AreEnemiesInGame(){
		string toreturn= "";
		foreach(KeyValuePair<EnemyType,LinkedList<Enemy>[]> pairvalue in _enemiesList){
			if(pairvalue.Value[0].Count>0)
				toreturn +=pairvalue.Key.ToString();
		}
		if(toreturn.Equals(""))
			return "none";
		else{
			return toreturn;	
		}
	}
	
	//Creation & Destruction services(Not working for as long as we don't have a prefab).
	public Enemy CreateNewEnemy(EnemyType type){
		//create from prefab. 
		GameObject newEnemy = (GameObject) Instantiate(Resources.Load("Prefabs/Enemy"));
		string classname = "Enemy"+type.ToString();
		if(newEnemy.AddComponent(classname)){
			Enemy toreturn = (Enemy) newEnemy.GetComponent(classname);
			_enemiesList[type][1].AddLast(toreturn);
			toreturn.Spawned += EnemySpawned;
			toreturn.Destroyed += EnemyDestroyed;
			newEnemy.name = classname;
			return toreturn;
		}
		else{
			return null;	
		}
	}
	public Enemy CreateNewEnemy(Vector3 startpos, Vector3 dir, float speed, EnemyType type){
		//create from prefab. 
		GameObject newEnemy = (GameObject) Instantiate(Resources.Load("Prefabs/Enemy"));
		string classname = "Enemy"+type.ToString();
		if(newEnemy.AddComponent(classname )){
			Enemy toreturn = (Enemy) newEnemy.GetComponent(classname );
			toreturn.Spawned += EnemySpawned;
			toreturn.Destroyed += EnemyDestroyed;
			toreturn.Spawn(startpos,dir,speed);
			_enemiesList[type][1].AddLast(toreturn);
			newEnemy.name = classname ;
			return toreturn;
		}
		else{
			return null;	
		}
	}
	
	private void EnemySpawned(Enemy spawned){
		EnemyType type = (EnemyType) System.Enum.Parse(typeof(EnemyType),spawned.gameObject.name.Replace("Enemy",""));
		_enemiesList[type][1].Remove(spawned);
		_enemiesList[type][0].AddLast(spawned);
	}
	
	private void EnemyDestroyed(Enemy destroyed){
		EnemyType type = (EnemyType) System.Enum.Parse(typeof(EnemyType),destroyed.gameObject.name.Replace("Enemy",""));
		_enemiesList[type][0].Remove(destroyed);
		_enemiesList[type][1].AddLast(destroyed);
		_enemiesKilled++;
	}
}

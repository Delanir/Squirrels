using UnityEngine;
using System.Collections;

public class Allied_Entity : MonoBehaviour {
	
	//these are the internal states of each allied entity.
	public enum AState {
		//no enemies or acorns
		Idle,
		//enemies on range
		Attack,
		//either there are acorns onscreen to collect but no enemies, or this unit is upgraded as a collector
		Collect,
		//is deploying from base
		Spawning,
		//is dead, needs to spend some time refueling to come back to action
		Respawning
};
	//these are upgrades for the squirrels that will characterize their behaviour
	//for now, these upgrades are just suggestions
	public enum AUpgrade {
		Normal,
		Fast,
		Heavy,
		Kamikaze,
		Collector,
		Bruiser,
		Medic
};

	private int _ID;
	//for now, we use publics so we can define and test value ranges.
	public AState _state;
	public AUpgrade _uState;
	
	//general values.
	public int _HP;
	public int _upgradePoints;
	
	//idle values
	public float _speedIdle;
	public float _cooldownMovement;
	
	//attack values
	public int _damage;
	public int _maxAttackRange;
	public float _speedAttack;
	public float _cooldownAttack;
	
	//collect values
	public float _speedCollect;
	public float _cooldownCollect;
	public int _containerCapacity;
	public int _containerMaxCapacity;
	
	//spawning values
	public Vector3 _spawnIniPosition;
	public Vector3 _spawnFinalPosition;
	private float _startTime;
	public float _spawnTime;
	
	private Transform _myTransform;
	
	//coordinate limits
	private float _upperLimit = 89.0f;
	private float _lowerLimit = -89.0f;
	private float _leftLimit = -106.0f;
	private float _rightLimit = 159.0f;
	
	//describes movement direction
	private Vector3 _direction;
	//describes ally orientation (facing)
	private Vector3 _orientation;
	
	// Use this for initialization
	void Start () 
	{
		_myTransform = this.transform;
		_startTime = Time.time;
		
		StartCoroutine(spawnUnit(_spawnTime));
		this._uState = AUpgrade.Normal;
		
		//start the direction vector by randomizing between up and down
		int coinFlip = Random.Range(1,3);
		if(coinFlip == 1)
			_direction = new Vector3(0,1,0);
		else
			_direction = new Vector3(0,-1,0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//check the current state
		checkState();
		//check for upgrades
		checkUpgrade();
		
		//engage in movement, collect, attack actions
		switch (_state)
		{
			case AState.Spawning:
				_myTransform.position = Vector3.Lerp(_spawnIniPosition, _spawnFinalPosition, Mathf.Pow(((Time.time - _startTime) / _spawnTime), 2.0f));
	
			//print (_state);
				break;
			
			case AState.Attack:
				break;
			
			case AState.Collect:
				break;
			
			case AState.Idle:
				moveUnit();
				break;
		}
	}
	
	
	void checkState()
	{
	//for now, just returns Idle State	
		if(this._state != AState.Spawning)
			this._state = AState.Idle;	
	}
	
	void checkUpgrade()
	{
	//for now, assume normal upgraded unit
		this._uState = AUpgrade.Normal;
	}
	
	//this routine just handles regular movement while in iddle state
	//since they should be patrolling, they move vertically with a little horizontal variation
	void moveUnit()
	{
		moveVertically();

	}
	
	//just moves the units up and down while idle
	void moveVertically ()
	{
		float sixthTotalHeight = (_upperLimit -_lowerLimit)/6;
		float upperMovLimit = _upperLimit - sixthTotalHeight;
		float lowerMovLimit = _lowerLimit + sixthTotalHeight;
		
		if(_myTransform.position.y >= upperMovLimit || _myTransform.position.y <= lowerMovLimit)
		{
			_direction = new Vector3(_direction.x, -_direction.y, 0);
		}
		
		float ammountToMove = _speedIdle*Time.deltaTime;
		
		_myTransform.position += _direction*ammountToMove;	
	}
	
	//adds a little horizontal movement jitter to simulate floating physics
	void moveHorizontally()
	{

	}
	
	//this routine brings allied units to the screen
	//like they are deploying from the ship
	IEnumerator spawnUnit(float _spawnTime)
	{
		print("beginning Spawning");
		//_myTransform.position = Vector3.Lerp(_spawnIniPosition, _spawnFinalPosition, (Time.time - _startTime) / _spawnTime);
		yield return new WaitForSeconds(_spawnTime);
		print ("updating status");
		this._state = AState.Idle;
	}
	
	public int getID()
	{
		return _ID;	
	}
	
	public void setID(int newID)
	{
		this._ID = newID;	
	}
}

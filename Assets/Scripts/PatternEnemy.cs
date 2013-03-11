using System;
using System.Collections.Generic;
using UnityEngine;

public enum PatternCheckpointType{
	Positions,
	Directions
}

public class PatternEnemy : Enemy
{
	List<Vector3> _checkpointList; //list of checkpoints to cover, excluding the starting position!
	List<float> _runTimes; //list of how long each checkpoint will take (positions type)
	List<float> _runSpeeds; // list of how long each run will take (directions type)
	bool  _usingPositions; //dictates whether or not we're using positions, or directions.
	PatternCheckpointType _checkpointType;
	int _currentCheckpoint;
	Vector3 _currentDir;
	float _currentSpeed;
	float _counter = 0;

	
// Use this for initialization
	protected  override void  Awake () {
		//initialize and hide.
		base.Awake();		
	}
	
	private bool Checkpoint(){
		switch(_checkpointType){
			case PatternCheckpointType.Positions:
				if(_counter >= _runTimes[_currentCheckpoint]){
					_currentCheckpoint++;
					_currentDir = (_checkpointList[_currentCheckpoint] - this.transform.position).normalized;
					_currentSpeed = ((_checkpointList[_currentCheckpoint] - this.transform.position)/_runTimes[_currentCheckpoint]).sqrMagnitude;
					_counter = 0;
					return true;
				}
				return false;
			case PatternCheckpointType.Directions:
				if(_counter >= _runTimes[_currentCheckpoint]){
					_currentCheckpoint++;
					_currentDir = _checkpointList[_currentCheckpoint].normalized;
					_currentSpeed = _runSpeeds[_currentCheckpoint];
					_counter = 0;
					return true;
				}
				return false;
		default:
			return false;
		}
	}
	
	// Update is called once per frame
	protected override void  Update () {
		//move the acorn, if it's alive.
		if(IsAlive()){
			this.transform.position = Vector3.Lerp(this.transform.position, _currentDir *_currentSpeed, Time.fixedDeltaTime);
			_counter+=Time.fixedDeltaTime;
			this.Checkpoint();
		
		}
	}
	
	public void Spawn(Vector3 startpos, List<Vector3> checkpointlist, PatternCheckpointType positionsordirection){
		this.transform.position = startpos;
	    List<float> runtimes = new List<float>();
		float ad; 
		switch(positionsordirection){
			case PatternCheckpointType.Positions:
				ad= 10/_checkpointList.Count;
				for(int i=0; i<checkpointlist.Count; i++){
				runtimes.Add(ad);
				}
				_runTimes = runtimes;
				_runSpeeds = new List<float>();
				_currentDir = (checkpointlist[0]-startpos).normalized;
				_currentSpeed = ((checkpointlist[0]-startpos)/runtimes[0]).sqrMagnitude;
			break;
			case PatternCheckpointType.Directions:
				ad=10;
				for(int i=0; i<checkpointlist.Count; i++){
					runtimes.Add(ad);
				}
				_runSpeeds = runtimes;
				_runTimes = new List<float>();
				_currentDir = checkpointlist[0];
				_currentSpeed = runtimes[0];
			break;
		}
		_currentCheckpoint = 0;
	}
	
	public void Spawn(Vector3 startpos, List<Vector3> checkpointlist, PatternCheckpointType positionsordirection, List<float> runtimes){
		this.transform.position = startpos;
		_checkpointList = checkpointlist;
		_checkpointType = positionsordirection;
		float ad;
		switch(positionsordirection){
			case PatternCheckpointType.Positions:
				ad= 10/_checkpointList.Count;
				_runTimes = runtimes;
				_runSpeeds = new List<float>();
				_currentDir = (checkpointlist[0]-startpos).normalized;
				_currentSpeed = ((checkpointlist[0]-startpos)/runtimes[0]).sqrMagnitude;
			break;
			case PatternCheckpointType.Directions:
				ad=10;
				_runSpeeds = runtimes;
				_runTimes = new List<float>();
				_currentDir = checkpointlist[0];
				_currentSpeed = runtimes[0];
			break;
		}
		_currentCheckpoint = 0;
	}
	
	public void Spawn(Vector3 startpos, List<Vector3> checkpointlist, PatternCheckpointType positionsordirection, float globaltime){
		this.transform.position = startpos;
		_checkpointList = checkpointlist;
		List<float> runtimes = new List<float>();
		_checkpointType = positionsordirection;
		float ad;
		switch(positionsordirection){
			case PatternCheckpointType.Positions:
				ad= globaltime/_checkpointList.Count;
				for(int i=0; i<checkpointlist.Count; i++){
					runtimes.Add(ad);
				}
				_runTimes = runtimes;
				_runSpeeds = new List<float>();
				_currentDir = (checkpointlist[0]-startpos).normalized;
				_currentSpeed = ((checkpointlist[0]-startpos)/runtimes[0]).sqrMagnitude;
			break;
			case PatternCheckpointType.Directions:
				ad=10;
				_runSpeeds = runtimes;
				_runTimes = new List<float>();
				_currentDir = checkpointlist[0];
				_currentSpeed = runtimes[0];
			break;
		}
		_currentCheckpoint = 0;
	}
}



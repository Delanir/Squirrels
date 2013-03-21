using System;
using System.Collections.Generic;
using UnityEngine;

public enum PatternCheckpointType{
	Positions,
	Directions
}

public class EnemyPattern : Enemy
{
	List<Vector3> _checkpointList; //list of checkpoints to cover, including the starting position! Position 0 of runtimes is how long it'll take to from final position back to first (loop)
	List<float> _runTimes; //list of how long each checkpoint will take (positions type)
	List<float> _runSpeeds; // list of how long each run will take (directions type)
	bool  _usingPositions; //dictates whether or not we're using positions, or directions.
	PatternCheckpointType _checkpointType;
	int _currentCheckpoint;
	Vector3 _currentDir;
	float _currentSpeed;
	float _counter = 0;
	bool _loop = true;

	
// Use this for initialization
	protected  override void  Awake () {
		//initialize and hide.
		base.Awake();		
	}
	
	private bool Checkpoint(){
		if(_counter >= _runTimes[_currentCheckpoint]){
			_currentCheckpoint++;
			_counter = 0;
			if(_currentCheckpoint>=_checkpointList.Count)
				return false;
			switch(_checkpointType){
				case PatternCheckpointType.Positions:
					_currentDir = (_checkpointList[_currentCheckpoint] - this.transform.position).normalized;
					_currentSpeed = ((_checkpointList[_currentCheckpoint] - this.transform.position)/_runTimes[_currentCheckpoint]).magnitude;
					return true;
				case PatternCheckpointType.Directions:
					_currentDir = _checkpointList[_currentCheckpoint].normalized;
					_currentSpeed = _runSpeeds[_currentCheckpoint];
					return true;
				default:
					return false;
			}
		}
		return true;
	}
	
	// Update is called once per frame
	protected override void  Update () {
		//move the enemy, if it's alive.
		if(IsAlive()){
			this.transform.position = Vector3.Lerp(this.transform.position,this.transform.position+(_currentDir *_currentSpeed), Time.fixedDeltaTime);
			_counter+=Time.fixedDeltaTime;
			if(_currentCheckpoint<_checkpointList.Count)
				Checkpoint();
			else{
				if(_loop){
					_currentCheckpoint=0;
					Checkpoint();
				}
					
			}
		}
	}
	
	public void Spawn(Vector3 startpos, List<Vector3> checkpointlist, PatternCheckpointType positionsordirection){
		this.transform.position = startpos;
	    List<float> runtimes = new List<float>();
		float ad; 
		switch(positionsordirection){
			case PatternCheckpointType.Positions:
				ad= 10/(_checkpointList.Count-1);
				float temp = 0;
				for(int i=0; i<(checkpointlist.Count-1); i++){
					runtimes.Add(ad);
					temp+=ad;
				}
				runtimes.Insert(0,temp/(_checkpointList.Count-1)); //choose the average speed to loop.
				_runTimes = runtimes;
				_runSpeeds = new List<float>();
				_currentDir = (checkpointlist[1]-startpos).normalized;
				_currentSpeed = ((checkpointlist[1]-startpos)/runtimes[1]).magnitude;
				_currentCheckpoint = 1;
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
				_currentCheckpoint = 0;
			break;
		}
		
		base.Spawn();
	}
	
	public void Spawn(Vector3 startpos, List<Vector3> checkpointlist, PatternCheckpointType positionsordirection, List<float> runtimes){
		this.transform.position = startpos;
		_checkpointList = checkpointlist;
		_checkpointType = positionsordirection;
		float ad;
		switch(positionsordirection){
			case PatternCheckpointType.Positions:
				_runTimes = runtimes;
				_runSpeeds = new List<float>();
				_currentDir = (checkpointlist[1]-startpos).normalized;
				_currentSpeed = ((checkpointlist[1]-startpos)/runtimes[1]).magnitude;
				_currentCheckpoint = 1;
			break;
			case PatternCheckpointType.Directions:
				ad=10;
				_runSpeeds = runtimes;
				_runTimes = new List<float>();
				_currentDir = checkpointlist[0];
				_currentSpeed = runtimes[0];
				_currentCheckpoint = 0;
			break;
		}
		
		base.Spawn();
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
				float temp = 0;
				for(int i=0; i<(checkpointlist.Count-1); i++){
					runtimes.Add(ad);
					temp+=ad;
				}
				runtimes.Insert(0,temp/(_checkpointList.Count-1)); //choose the average speed to loop.
				_runTimes = runtimes;
				_runSpeeds = new List<float>();
				_currentDir = (checkpointlist[1]-startpos).normalized;
				_currentSpeed = ((checkpointlist[1]-startpos)/runtimes[1]).magnitude;
				_currentCheckpoint = 1;
			break;
			case PatternCheckpointType.Directions:
				ad=10;
				_runSpeeds = runtimes;
				_runTimes = new List<float>();
				_currentDir = checkpointlist[0];
				_currentSpeed = runtimes[0];
				_currentCheckpoint = 0;
			break;
		}
		base.Spawn();
	}
}



using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadronManager : MonoBehaviour 
{
	private List<Allied_Entity> _alliesAlive = new List<Allied_Entity>();
	private List<Allied_Entity> _alliesDead = new List<Allied_Entity>();
	
	private float _levelTime = 0;
	//to setups the number of allies we can use on the current level
	public int _maxUnitCount;
	public int _numAlliesDead;
	private List<int> _usedIds = new List<int>();

	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		_levelTime += UnityEngine.Time.fixedDeltaTime;
	}

	
	public int getDeadAlliesCount()
	{
		return _numAlliesDead;	
	}
	
	public List<Allied_Entity> getAllies()
	{
		return _alliesAlive;	
	}
	
	public List<Allied_Entity> getDeadAllies()
	{
		return _alliesDead;	
	}
	
	public void allySpawned(Allied_Entity spawned)
	{
		_alliesDead.Remove(spawned);
		_alliesAlive.Add(spawned);
	}
	
	private void allyDestroyed(Allied_Entity destroyed)
	{
		_alliesAlive.Remove(destroyed);
		_alliesDead.Add(destroyed);
		_numAlliesDead++;
	}
	
	private void createAlly()
	{
		if((_alliesDead.Count + _alliesAlive.Count) < _maxUnitCount)
		{
			GameObject newAlly = (GameObject) Resources.Load("Prefabs/Squad_Member");
		
			Allied_Entity returnedAlly = newAlly.GetComponent<Allied_Entity>();
		
			returnedAlly.setID(getNextAvailableID());
		
			allySpawned(returnedAlly);
		}
	}
	
	private int getNextAvailableID()
	{
		int nextID = 0;
		bool gotID = false;
		
		for(;gotID == false; nextID++)
		{
			if(!_usedIds.Contains(nextID))
			{
				gotID = true;
				break;
			}
		}
		return nextID;
	}
	
	//these next methods are just for debug purposes!
		
}

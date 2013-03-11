using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AcornManager : MonoBehaviour {
	
	
	LinkedList<Acorn> _standardAcornsInUse;
	LinkedList<Acorn> _standardAcornsAvailable;
	
	// Use this for initialization
	void Start () {
		_standardAcornsInUse = new LinkedList<Acorn>();
		_standardAcornsAvailable = new LinkedList<Acorn>();
		/* FOR TESTING*/
		Acorn testAcorn = GameObject.Find("Bolota(temp)").GetComponent<Acorn>() as Acorn;
		testAcorn.Spawned += StandardAcornSpawned;
		testAcorn.Destroyed += StandardAcornDestroyed;
		testAcorn.Spawn(new Vector3(testAcorn.transform.position.x,testAcorn.transform.position.y, testAcorn.transform.position.z*-1), new Vector3(1,0.7f,0),20);
	
	}
	
	// Update is called once per frame
	void Update () {
		//Is this necessary?
	}
	
	public Acorn SpawnStandardAcorn(Vector3 startpos, Vector3 dir, float speed){
		Acorn toreturn = null;
		if(_standardAcornsAvailable.Count>0){
			return _standardAcornsAvailable.First.Value;
			return toreturn;
		}
		else{
			return CreateNewStandardAcorn(startpos, dir,speed);	
		}
	}
	
	public Acorn GetStandardAcornForUse(){
		if(_standardAcornsAvailable.Count>0){
			return _standardAcornsAvailable.First.Value;
		}
		else{
			return CreateNewStandardAcorn();	
		}
		
	}
	
	public bool AreAcornAvailable(){
		return (_standardAcornsAvailable.Count>0);
	}
	
	public bool AreAcornsInGame(){
		return (_standardAcornsInUse.Count>0);
	}
	
	//Creation & Destruction services(Not working for as long as we don't have a prefab).
	public Acorn CreateNewStandardAcorn(){
		//create from prefab. 
		GameObject newAcorn = (GameObject) Resources.Load("Prefabs/StandardAcorn");
		Acorn toreturn = newAcorn.GetComponent<StandardAcorn>();
		_standardAcornsInUse.AddLast(toreturn);
		toreturn.Spawned += StandardAcornSpawned;
		toreturn.Destroyed += StandardAcornDestroyed;
		return toreturn;
	}
	public Acorn CreateNewStandardAcorn(Vector3 startpos, Vector3 dir, float speed){
		//create from prefab. 
		GameObject newAcorn = (GameObject) Resources.Load("Prefabs/StandardAcorn");
		Acorn toreturn = newAcorn.GetComponent<StandardAcorn>();
		toreturn.Spawn(startpos,dir,speed);
		_standardAcornsInUse.AddLast(toreturn);
		toreturn.Spawned += StandardAcornSpawned;
		toreturn.Destroyed += StandardAcornDestroyed;
		return toreturn;
	}
	
	private void StandardAcornSpawned(Acorn spawned){
		_standardAcornsAvailable.Remove(spawned);
		_standardAcornsInUse.AddLast(spawned);
	}
	
	private void StandardAcornDestroyed(Acorn destroyed){
		_standardAcornsInUse.Remove(destroyed);
		_standardAcornsAvailable.AddLast(destroyed);
	}
}

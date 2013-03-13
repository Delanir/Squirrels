using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AcornType{
	Standard
}


public class AcornManager : MonoBehaviour {
	
	/// <summary>
	/// Dictionary referencing all lists of acorns. Each pair being the list of acorns in use and the list of acorns available. 0 and 1 respectively. 
	/// </summary>
	Dictionary<AcornType,LinkedList<Acorn>[]> _acornsList;
	
	// Use this for initialization
	void Start () {
		_acornsList = new Dictionary<AcornType, LinkedList<Acorn>[]>();
		LinkedList<Acorn> []standardacorns = new LinkedList<Acorn>[2];
		standardacorns[0] = new LinkedList<Acorn>();
		standardacorns[1] = new LinkedList<Acorn>();
		_acornsList.Add(AcornType.Standard, standardacorns);
		//TODO: Perhaps create a fixed number of initial acorns to be available, just to limit realtime generation?
	}
	
	// Update is called once per frame
	void Update () {
		//Is this necessary?
	}
	
	/// <summary>
	/// Spawns the acorn.
	/// </summary>
	/// <returns>
	/// The acorn, already spawned.
	/// </returns>
	/// <param name='startpos'>
	/// Start position.
	/// </param>
	/// <param name='dir'>
	/// Desired Direction.
	/// </param>
	/// <param name='speed'>
	/// Desired Speed.
	/// </param>
	/// <param name='type'>
	/// Type of Acorn.
	/// </param>
	public Acorn SpawnAcorn(Vector3 startpos, Vector3 dir, float speed, AcornType type){
		Acorn toreturn = null;
		if(_acornsList[type][1].Count>0){
			return _acornsList[type][1].First.Value;
			return toreturn;
		}
		else{
			return CreateNewAcorn(startpos, dir,speed, type);	
		}
	}
	
	/// <summary>
	/// Gets the acorn for spawning and use.
	/// </summary>
	/// <returns>
	/// The acorn, yet un-spawned.
	/// </returns>
	/// <param name='type'>
	/// Type.
	/// </param>
	public Acorn GetAcornForUse(AcornType type){
		if(_acornsList[type][1].Count>0){
			return _acornsList[type][1].First.Value;
		}
		else{
			return CreateNewAcorn(type);	
		}
		
	}
	
	/// <summary>
	/// Are any type of acorn available(Not in game ones)?
	/// </summary>
	/// <returns>
	/// The types of acorns that are available in a continuous string (use .contains(type of acorn) to triage).
	/// </returns>
	public string AreAcornAvailable(){
		string toreturn= "";
		foreach(KeyValuePair<AcornType,LinkedList<Acorn>[]> pairvalue in _acornsList){
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
	/// Are any type of acorn in game (spawned and physically alive)?
	/// </summary>
	/// <returns>
	/// The types of acorns that are in game in a continuous string (use .contains(type of acorn) to triage).
	/// </returns>
	public string AreAcornsInGame(){
		string toreturn= "";
		foreach(KeyValuePair<AcornType,LinkedList<Acorn>[]> pairvalue in _acornsList){
			if(pairvalue.Value[0].Count>0)
				toreturn +=pairvalue.Key.ToString();
		}
		if(toreturn.Equals(""))
			return "none";
		else{
			return toreturn;	
		}
	}
	
	/// <summary>
	/// Are any acorns of type type available?
	/// </summary>
	/// <returns>
	/// The type of acorn available.
	/// </returns>
	/// <param name='type'>
	/// Type of acorn to check with.
	/// </param>/
	public bool AreAcornAvailable(AcornType type){
		string toreturn= "";
		if(_acornsList[type][1].Count>0)
			return true;
		else
			return false;	
	}
	
	/// <summary>
	/// Are any acorns of type type available?
	/// </summary>
	/// <returns>
	/// The type of acorn available.
	/// </returns>
	/// <param name='type'>
	/// Type of acorn to check with.
	/// </param>/
	public bool AreAcornInGame(AcornType type){
		string toreturn= "";
		if(_acornsList[type][0].Count>0)
			return true;
		else
			return false;	
	}
	
	//Creation & Destruction services.
	public Acorn CreateNewAcorn(AcornType type){
		//create from prefab. 
		GameObject newAcorn = (GameObject) Resources.Load("Prefabs/Acorn");
		if(newAcorn.AddComponent(type.ToString())){
			Acorn toreturn = (Acorn) newAcorn.GetComponent(type.ToString());
			_acornsList[type][1].AddLast(toreturn);
			toreturn.Spawned += AcornSpawned;
			toreturn.Destroyed += AcornDestroyed;
			newAcorn.name = type.ToString();
			return toreturn;
		}
		else{
			return null;	
		}
	}
	public Acorn CreateNewAcorn(Vector3 startpos, Vector3 dir, float speed, AcornType type){
		//create from prefab. 
		GameObject newAcorn = (GameObject) Resources.Load("Prefabs/Acorn");
		if(newAcorn.AddComponent(type.ToString())){
			Acorn toreturn = (Acorn) newAcorn.GetComponent(type.ToString());
			toreturn.Spawned += AcornSpawned;
			toreturn.Destroyed += AcornDestroyed;
			toreturn.Spawn(startpos,dir,speed);
			_acornsList[type][1].AddLast(toreturn);
			newAcorn.name = type.ToString();
			return toreturn;
		}
		else{
			return null;	
		}
	}
	
	private void AcornSpawned(Acorn spawned){
		AcornType type = (AcornType) System.Enum.Parse(typeof(AcornType),spawned.gameObject.name);
		_acornsList[type][1].Remove(spawned);
		_acornsList[type][0].AddLast(spawned);
	}
	
	private void AcornDestroyed(Acorn destroyed){
		AcornType type = (AcornType) System.Enum.Parse(typeof(AcornType),destroyed.gameObject.name);
		_acornsList[type][1].Remove(destroyed);
		_acornsList[type][0].AddLast(destroyed);
	}
}

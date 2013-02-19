using UnityEngine;
using System.Collections;

public class Allied_Entity : MonoBehaviour {
	
	//these are the internal states of each allied entity.
	public enum AState {
		Idle,
		Attack,
		Collect
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
	
	//for now, we use publics so we can define and test value ranges.
	public AState _state;
	
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
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

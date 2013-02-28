using UnityEngine;
using System.Collections;

public class BaseShooter : MonoBehaviour {

	//class variables
	// angle the player can change to point and shoot
	public float _angle=0.0f;

	public enum TypeOfBulletState {
		Normal
};
	
	public TypeOfBulletState _type = Normal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

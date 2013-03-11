using UnityEngine;
using System.Collections;

public class MovementEquations : MonoBehaviour {

	// Use this for initialization
	
	public enum MovementType 
	{
		Linear,
		Exponential,
		Parabola,
		Sine
	};
	
	public MovementType _movementType;
	
	public float _startTime;
	public float _duration;
	public Vector3 _orientation;
	public Vector3 _startPosition;
	public Vector3 _endPosition;
	
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float calculateAvgAcceleration(float initialVelocity, float finalVelocity, float totalTime)
	{
		return (finalVelocity - initialVelocity)/totalTime;	
	}
	
	public float calculateDisplacement(float initialVelocity, float finalVelocity, float totalTime)
	{
		return (finalVelocity - initialVelocity)*totalTime*0.5f;	
	}
	

	
}

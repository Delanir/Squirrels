using UnityEngine;
using System.Collections;

public class GUIScriptUtils : MonoBehaviour {
	
	private int screenHeightRes;
	private int screenWidthRes;
	private int screenWidth;
	private int screenHeight;
	
	public float originalHeight;
	public float originalWidth;
	
	private Vector3 vScale;
	private Vector2 aspectRatio;
	
	private Matrix4x4 originalMatrix;
	
	//this is a class with methods for GUI manipulation and shizzles
	//WHENEVER YOU GUYS WANT TO WORK WITH THIS, PUTH YOU CURRENT WINDOW SIZE IN
	//THE ORIGINAL WIDTH AND HEIGHT PARAMETERS
	//I DO NOT MEAN THE RESOLUTION
	// Use this for initialization
	void Start () 
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		aspectRatio.x = screenWidth/originalWidth;
		aspectRatio.y = screenHeight/originalHeight;
		
		vScale.x = screenWidth/originalWidth;
		vScale.y = screenHeight/originalHeight;
		vScale.z = 1;
		
	}
	
	//call this method when you need to resize the dimensions of a gui element
	public Rect resizeGUIElement(Rect rect)
	{
		float rectW = rect.width * aspectRatio.x;
		float rectH = rect.height * aspectRatio.y;
		
		return new Rect( rect.x, rect.y, rectW, rectH);
	}
	
	//this method changes the gui matrix so that every element position works
	//in any given resolution
	//call this during OnGUI()
	public void resizeGUIPositions()
	{
		originalMatrix = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, vScale);
	}
	
	//call this at the end ofOnGUI() to switch back the gui matrix
	public void revertGUIPositions()
	{
		GUI.matrix = originalMatrix;
	}
}

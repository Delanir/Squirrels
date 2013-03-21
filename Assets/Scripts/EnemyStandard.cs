
using System;


public class EnemyStandard : Enemy
{
// Use this for initialization
	protected  override void  Awake () {
		//initialize and hide.
		base.Awake();		
	}
	
	// Update is called once per frame
	protected override void  Update () {
		//move the acorn, if it's alive.
		base.Update();	
		//TODO: Check where acorn is. If it's outside the screen, Kill it. 
	}
}


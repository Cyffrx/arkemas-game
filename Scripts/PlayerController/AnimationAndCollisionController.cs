using Godot;
using System;
using System.Collections.Generic;

public class AnimationAndCollisionController : Node
{

	AnimatedSprite aSprite;
	public static Dictionary<Vector2, int> animationDirectionMapper = new Dictionary<Vector2, int>()
	{	
		{new Vector2(0,1),0},
		{new Vector2(1,1),1},
		{new Vector2(1,0),2},
		{new Vector2(1,-1),3},
		{new Vector2(0,-1),4},
		{new Vector2(-1,-1),5},
		{new Vector2(-1,0),6},
		{new Vector2(-1,1),7},	
	};

	// gets the last direction from the state machine
	public int FacingDirection { get { return animationDirectionMapper[(Vector2) GetNode<Node>("../PlayerControlStateMachine").Get("LastDirection")]; } }
	public string CurrentState { 
		get { return (string) GetNode<Node>("../PlayerControlStateMachine").Get("CurrentState"); }
		set { GetNode<Node>("../PlayerControlStateMachine").Call("ChangeState", value); }
	}
	public override void _Ready()
	{
		aSprite = GetNode<AnimatedSprite>("../AnimatedSprite");
	}

	// attacking
	public void _on_Attacking_StateStart()
	{
		GD.Print("Attack state started");
		aSprite.Play("attack_" + FacingDirection);
		aSprite.SpeedScale = 6;
	}

	public void _on_Dodging_StateStart()
	{
		aSprite.Play("dodge_" + FacingDirection);
		aSprite.SpeedScale = 10;
	}

	public void _on_Dodging_StateUpdated()
	{
		aSprite.Rotation += .25f * (FacingDirection>4?-1:1);
	}
	
	#region moving anim triggers
	public void _on_Moving_StateStart() { _on_Moving_StateUpdated(); }

	public void _on_Moving_StateUpdated() { aSprite.Play("run_" + FacingDirection); aSprite.SpeedScale = 6; }

	#endregion
	
	// idling
	public void _on_Idling_StateStart()
	{
		aSprite.Play("idle_" + FacingDirection);
		aSprite.SpeedScale = 2;
		aSprite.Rotation = 0;
	}
}

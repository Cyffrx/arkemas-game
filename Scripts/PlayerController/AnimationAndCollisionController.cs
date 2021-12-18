using Godot;
using System;

public class AnimationAndCollisionController : Node
{

	AnimatedSprite animatedSprite;
	
	public override void _Ready() { animatedSprite = GetNode<AnimatedSprite>("../KinematicBody2D/AnimatedSprite"); }

	public int Direction { get { return UpdateDirection(); } }
	private int UpdateDirection()
	{   
		Vector2 velocity = (Vector2) GetNode<Node>("../PlayerControlStateMachine").Get("Direction");
		
		Vector2 [] directionArray = 
		{
			new Vector2(0,1),
			new Vector2(1,1),
			new Vector2(1,0),
			new Vector2(1,-1),
			new Vector2(0,-1),
			new Vector2(-1,-1),
			new Vector2(-1,0),
			new Vector2(-1,1),
		};

		for (int i = 0; i < 8; i++)
			if (directionArray[i] == velocity)
					return i;
		return 0;
	}

	public void _on_IdleState_StateStart()
	{
		// play idle based on currentDirection
		animatedSprite.Play("idle_" + Direction);
		animatedSprite.SpeedScale = 2;
	}
	
	public void _on_MovementState_StateStart()
	{
		animatedSprite.Play("run_" + Direction );
		animatedSprite.SpeedScale = 6;
	}

	public void _on_MovementState_StateUpdated()
	{
		animatedSprite.Play("run_" + Direction );
	}

    public void _on_AttackState_StateStart()
    {
        animatedSprite.Play("attack_" + Direction);
        animatedSprite.SpeedScale = 6;
    }
}
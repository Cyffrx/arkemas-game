using Godot;
using System;
using System.Collections.Generic;

public class Moving : PlayerControlState
{
	[Signal] public delegate void StateUpdateOver();

	public float MovementSpeed = 400;

	public override void UpdateState(float delta)
	{
		Vector2 velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_up"))
			velocity.y -= 1;
		if (Input.IsActionPressed("move_down"))
			velocity.y += 1;
		if (Input.IsActionPressed("move_left"))
			velocity.x -= 1;
		if (Input.IsActionPressed("move_right"))
			velocity.x += 1;

		GetOwner<KinematicBody2D>().MoveAndSlide( velocity.Normalized() * MovementSpeed );

		if ( Input.IsActionJustPressed("action_attack") ) PCSM.ChangeState("Attacking");
		if ( Input.IsActionJustPressed("action_dodge") ) PCSM.ChangeState("Dodging");
		if ( velocity == Vector2.Zero ) PCSM.ChangeState("Idling");
		else PCSM.LastDirection = velocity;
		
		OnUpdate();
	}
}

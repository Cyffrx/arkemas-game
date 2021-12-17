using Godot;
using System;
using System.Collections.Generic;

public class MovementState : PlayerControlState
{
	public Vector2 Velocity;
	
	private float MaxMovementSpeed = 250;
	private float MovementSpeed = 250;
	
	private KinematicBody2D kb;
	
	public override void _Ready()
	{
		base._Ready();
		kb = GetNode<KinematicBody2D>("../../../KinematicBody2D");
	}

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		GD.Print("Movement state started");
	}

	public override void UpdateState(float delta)
	{
		Velocity = Vector2.Zero;

		// movement
		if (Input.IsActionPressed("movement_right"))
			Velocity.x += 1;
		if (Input.IsActionPressed("movement_left"))
			Velocity.x += -1;
		if (Input.IsActionPressed("movement_up"))
			Velocity.y += -1;
		if (Input.IsActionPressed("movement_down"))
			Velocity.y += 1;
				
		Velocity = Velocity.Normalized();
		
		kb.MoveAndSlide( Velocity * MovementSpeed );
		
		if (Velocity == Vector2.Zero)
			PCSM.ChangeState("IdleState");
		
		UpdateFacingDirection();
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
	}

	public void UpdateFacingDirection()
	{
		// need to change Actor's Direction

	}
}

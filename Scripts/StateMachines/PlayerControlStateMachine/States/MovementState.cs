using Godot;
using System;
using System.Collections.Generic;

public class MovementState : PlayerControlState
{
	[Signal] public delegate void StateUpdateOver();

	public Vector2 Direction;

	private float MovementSpeed = 400;
	
	private KinematicBody2D kb;
	
	public override void _Ready()
	{
		kb = GetNode<KinematicBody2D>("../../../KinematicBody2D");
		Direction = Vector2.Down;
	}

	public override void UpdateState(float delta)
	{
		Vector2 _velocity = Vector2.Zero;

		if (Input.IsActionPressed("movement_right"))
			_velocity.x += 1;
		if (Input.IsActionPressed("movement_left"))
			_velocity.x -= 1;
		if (Input.IsActionPressed("movement_up"))
			_velocity.y -= 1;
		if (Input.IsActionPressed("movement_down"))
			_velocity.y += 1;

		Direction = _velocity == Vector2.Zero ? Direction : _velocity;

		_velocity = _velocity.Normalized();
		kb.MoveAndSlide( _velocity * MovementSpeed );

		if (Input.IsActionJustPressed("attack_click")) PCSM.ChangeState("AttackState");
		if ( _velocity == Vector2.Zero) PCSM.ChangeState("IdleState");
		
		OnUpdate();
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);

		Direction = Vector2.Zero;
	}
}

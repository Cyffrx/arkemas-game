using Godot;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class PlayerControlStateMachine : _DefaultStateMachine
{
	private Vector2 _lastDirection = Vector2.Down;
	public Vector2 Direction { get {return  CheckDirection(); } }

	public override void _Ready()
	{
		base._Ready();

		List<PlayerControlState> playerControlStates = GetNode<Node>("States").GetChildren().OfType<PlayerControlState>().ToList();
		
		for (int pcs_idx = 0; pcs_idx < playerControlStates.Count; pcs_idx++)
		{
			playerControlStates[pcs_idx].PCSM = this;
			GD.Print($"State found: {playerControlStates[pcs_idx].Name}");
		}

		ChangeState(playerControlStates[0].Name);
	}

	private Vector2 CheckDirection()
	{
		Vector2 movementVector = (Vector2) GetNode<Node>("States/MovementState").Get("Direction");
		Vector2 attackVector = (Vector2) GetNode<Node>("States/AttackState").Get("Direction");
		
		_lastDirection = attackVector == Vector2.Zero ? _lastDirection: attackVector;
		_lastDirection = movementVector == Vector2.Zero ? _lastDirection: movementVector;
		
		return _lastDirection;
	}
}

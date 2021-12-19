	using Godot;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class PlayerControlStateMachine : _DefaultStateMachine
{
	public Vector2 LastDirection = Vector2.Down;

	public override void _Ready()
	{
		base._Ready();

		List<PlayerControlState> playerControlStates = GetNode<Node>("States").GetChildren().OfType<PlayerControlState>().ToList();
		
		for (int pcs_idx = 0; pcs_idx < playerControlStates.Count; pcs_idx++)
			playerControlStates[pcs_idx].PCSM = this;

		ChangeState(playerControlStates[0].Name);
	}
}

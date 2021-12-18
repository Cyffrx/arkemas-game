using Godot;
using System;
using System.Collections.Generic;

public class IdleState : PlayerControlState
{
	public override void UpdateState(float delta)
	{
		// this can probably be consolidated with some sort of "action group"
		if (Input.IsActionPressed("movement_right")) PCSM.ChangeState("MovementState");
		if (Input.IsActionPressed("movement_left")) PCSM.ChangeState("MovementState");
		if (Input.IsActionPressed("movement_up")) PCSM.ChangeState("MovementState");
		if (Input.IsActionPressed("movement_down")) PCSM.ChangeState("MovementState");
		if (Input.IsActionJustPressed("attack_click")) PCSM.ChangeState("AttackState");
	}
}
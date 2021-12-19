using Godot;
using System;
using System.Collections.Generic;

public class Idling : PlayerControlState
{
	public override void UpdateState(float delta)
	{
		// movement
		if (Input.IsActionPressed("move_up")) PCSM.ChangeState("Moving");
		if (Input.IsActionPressed("move_down")) PCSM.ChangeState("Moving");
		if (Input.IsActionPressed("move_left")) PCSM.ChangeState("Moving");
		if (Input.IsActionPressed("move_right")) PCSM.ChangeState("Moving");

		// actions
		if (Input.IsActionJustPressed("action_attack")) PCSM.ChangeState("Attacking");
		if (Input.IsActionJustPressed("action_dodge")) PCSM.ChangeState("Dodging");
	}
}
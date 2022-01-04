using Godot;
using System;
using System.Collections.Generic;

public class Attacking : ActorState
{
	private bool comboAttack;
	private int attackChain = 0;
	private int attackChainMax = 2;

	public override void UpdateState(float _delta)
	{
		if (Input.IsActionJustPressed("action_attack")) comboAttack = true;

		// only want to continue forwards if the direction is being held
		Vector2 hold_velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_up")) hold_velocity.y = -1;
		if (Input.IsActionPressed("move_down")) hold_velocity.y = 1;
		if (Input.IsActionPressed("move_left")) hold_velocity.x = -1;
		if (Input.IsActionPressed("move_right")) hold_velocity.x = 1;

		ASM.Velocity = hold_velocity.Normalized() != ASM.Velocity ? Vector2.Zero: ASM.Velocity;

		ASM.kb.MoveAndSlide( ASM.WalkSpeed * ASM.Velocity.Normalized());
	}
}

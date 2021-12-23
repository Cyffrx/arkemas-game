using Godot;
using System;
using System.Collections.Generic;

public class Default : ActionState
{
	private Vector2 lastVelocity;
	public override void _Ready()
	{
		base._Ready();
		
		lastVelocity = Vector2.Down;
	}

	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);

		Vector2 velocity = Vector2.Zero;
		
		#region movement
		if (Input.IsActionPressed("move_up")) velocity.y -= 1;
		if (Input.IsActionPressed("move_down")) velocity.y += 1;
		if (Input.IsActionPressed("move_left")) velocity.x -= 1;
		if (Input.IsActionPressed("move_right")) velocity.x += 1;

		ASM.sprite.FlipH = velocity.x < 0;

		switch (velocity.LengthSquared())
		{
			case 0:
			if (ASM.animPlayer.CurrentAnimation != "idle") ASM.animPlayer.Play("idle");
			break;
			default:
			if (ASM.animPlayer.CurrentAnimation != "run") ASM.animPlayer.Play("run");
			ASM.kb.MoveAndSlide(velocity.Normalized() * (float) Owner.GetNode<Node>("Stats").Get("RunSpeed"));
			break;
		}
		#endregion

		#region actions
		if (Input.IsActionJustPressed("action_interact")) {} // ?
		if (Input.IsActionJustPressed("action_attack")) ASM.ChangeState("Attacking");
		if (Input.IsActionJustPressed("action_dodge")) ASM.ChangeState("Dodging", new Dictionary<string, object>() {{"Velocity", velocity}});
		#endregion

		#region casting
		// these state changes should probably carry a reference to what spell they're casting too
		if (Input.IsActionJustPressed("cast_markAndRecall")) ASM.ChangeState("Casting");
		if (Input.IsActionJustPressed("cast_pulse")) ASM.ChangeState("Casting");
		if (Input.IsActionJustPressed("cast_offensive")) ASM.ChangeState("Casting");
		if (Input.IsActionJustPressed("cast_defensive")) ASM.ChangeState("Casting");
		#endregion
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
	}
}

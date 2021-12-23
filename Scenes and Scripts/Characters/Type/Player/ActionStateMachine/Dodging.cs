using Godot;
using System;
using System.Collections.Generic;

public class Dodging : ActionState
{
	private bool attackAfterDodge = false;
	private Vector2 velocity;
	private KinematicBody2D kb;
	[Export] private int StaminaCost = 1;
	Node playerStats;

	public override void _Ready()
	{
		base._Ready();
		kb = (KinematicBody2D) Owner;
		playerStats = Owner.GetNode<Node>("Stats");
	}

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		
		if ( (int) playerStats.Get("Stamina") > 0) 
		{
			playerStats.Set("Stamina", -StaminaCost);

			ASM.animPlayer.Play("dodge");
			velocity = (Vector2) message["Velocity"];
			if (velocity == Vector2.Zero) velocity = kb.GetLocalMousePosition().Normalized();
		} else ASM.ChangeState("Default");
		
		
	}

	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);

		if (!attackAfterDodge) attackAfterDodge = Input.IsActionJustPressed("action_attack");

		// might draw an arrow pointing in the dodge direction
		kb.MoveAndSlide((float) Owner.GetNode<Node>("Stats").Get("DodgeSpeed") * velocity.Normalized());
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
		attackAfterDodge = false;
	}

	public void _on_AnimationPlayer_animation_finished(string animName)
	{
		if (animName == "dodge")
		{
			if (attackAfterDodge) ASM.ChangeState("Attacking");
			else ASM.ChangeState("Default");
		}			
	}
}

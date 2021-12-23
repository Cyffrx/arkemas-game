// dodging has three substates
// there's a backwards backstep where you move in the opposite vector of your mouse
// and there's forwardstepping, where you step towards your mouse
// and there's sidestepping, where you step perpendicularish to your mouse

using Godot;
using System;
using System.Collections.Generic;

public class Dodging : ActionState
{
	private bool attackAfterDodge = false;
	private Vector2 velocity;
	[Export] private int StaminaCost = 1;

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		
		if ( (int) ASM.playerStats.Get("Stamina") > 0) 
		{
			ASM.playerStats.Set("Stamina", (int) ASM.playerStats.Get("Stamina") - (int) ASM.playerStats.Get("DodgeCost"));

			// dodges towards momentum else dodges towards mouse
			velocity = (Vector2) message["Velocity"];
			if (velocity == Vector2.Zero) velocity = ASM.kb.GetLocalMousePosition().Normalized();

			ASM.animPlayer.Play("dodge");
			
			// will need to convert velocity to one of four directions and then play the subsequent dodge animation

		} else ASM.ChangeState("Default");	
	}

	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);

		if (Input.IsActionJustPressed("action_attack")) attackAfterDodge = true;

		// might draw an arrow pointing in the dodge direction
		ASM.kb.MoveAndSlide((float) ASM.playerStats.Get("DodgeSpeed") * velocity.Normalized());
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

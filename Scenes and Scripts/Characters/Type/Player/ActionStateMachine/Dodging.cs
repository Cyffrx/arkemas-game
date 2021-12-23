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
			ASM.playerStats.Set("Stamina", (int)ASM.playerStats.Get("Stamina") - StaminaCost);
			

			ASM.animPlayer.Play("dodge");
			velocity = (Vector2) message["Velocity"];
			if (velocity == Vector2.Zero) velocity = ASM.kb.GetLocalMousePosition().Normalized();
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
			GD.Print("dodge finish");
			if (attackAfterDodge) ASM.ChangeState("Attacking", new Dictionary<string, object>(){{"dodge-strike", true}});
			else ASM.ChangeState("Default");
		}
	}
}

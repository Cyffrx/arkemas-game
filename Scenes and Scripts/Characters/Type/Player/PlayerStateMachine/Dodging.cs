// dodging has three substates
// there's a backwards backstep where you move in the opposite vector of your mouse
// and there's forwardstepping, where you step towards your mouse
// and there's sidestepping, where you step perpendicularish to your mouse

using Godot;
using System;
using System.Collections.Generic;

public class Dodging : PlayerState
{
	[Export] public int StaminaCost = 1;
	private bool _attackAfterDodge;
	
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		if (PSM.Stamina.Value > 0)
		{
			PSM.Stamina.Value -= StaminaCost;

			if (PSM.Velocity == Vector2.Zero) PSM.Velocity = PSM.kb.GetLocalMousePosition().Normalized();
			PSM.animationPlayer.Play("dodge_"+PSM.Direction);
		}
		else PSM.ChangeState("Idling");
	}

	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);
		
		if (Input.IsActionJustPressed("action_attack")) _attackAfterDodge = true;
		PSM.kb.MoveAndSlide((float) PSM.DodgeSpeed * PSM.Velocity.Normalized());
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
		_attackAfterDodge = false;
	}

	public void _on_AnimationPlayer_animation_finished(string animName)
	{
		if (animName.Contains("dodge"))
		{
			if (_attackAfterDodge) PSM.ChangeState("Attacking");
			else PSM.ChangeState("Idling");
		}
	}
}

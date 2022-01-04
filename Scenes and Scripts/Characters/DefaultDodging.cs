using Godot;
using System.Collections.Generic;

public class DefaultDodging : ActorState
{
	[Export] public int StaminaCost = 1;
	public bool _attackAfterDodge;
	
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		if (ASM.Stamina.Value > 0)
		{
			ASM.Stamina.Value -= StaminaCost;

			if (ASM.Velocity == Vector2.Zero) ASM.Velocity = ASM.kb.GetLocalMousePosition().Normalized();
			// play dodge animation
		}
		else ASM.ChangeState("Idling");
	}

	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);

		// might draw an arrow pointing in the dodge direction
		ASM.kb.MoveAndSlide((float) ASM.DodgeSpeed * ASM.Velocity.Normalized());
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
		_attackAfterDodge = false;
	}

	public void _on_AnimationPlayer_animation_finished(string animName)
	{
		if (animName.Contains("dodge") && _attackAfterDodge) ASM.ChangeState("Attacking");
		else ASM.ChangeState("Default");
	}

}
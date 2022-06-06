using Godot;
using System;
using System.Collections.Generic;

public class PlayerAttacking : PlayerState
{
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		if (PSM.CharacterAttributes["Stamina"].Value == PSM.CharacterAttributes["Stamina"].Min)
			PSM.ChangeState("PlayerIdling");
		else
		{
			PSM.CharacterAttributes["Stamina"].Value -= PSM.CharacterAttributes["Attack Stamina Cost"].Value;
			
			PSM.Sprite.FlipH = PSM.Velocity.x < 0;
			PSM.AnimPlayer.Play("attack");
		}
	}

	private void _on_AnimationPlayer_animation_finished(string animName)
	{
		if (animName.Contains("attack"))
			PSM.ChangeState("PlayerIdling");
	}
}

using Godot;
using System;
using System.Collections.Generic;

public class PlayerDodging : PlayerState
{
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		
		if (PSM.CharacterAttributes["Stamina"].Value == PSM.CharacterAttributes["Stamina"].Min)
			PSM.ChangeState("PlayerIdling");
		else
		{
			PSM.CharacterAttributes["Stamina"].Value -= PSM.CharacterAttributes["Dodge Stamina Cost"].Value;

			// if standing stll, dodge away from mouse
			if (PSM.Velocity == Vector2.Zero) PSM.Velocity = PSM.Body.GetLocalMousePosition().Normalized();

			PSM.Sprite.FlipH = PSM.Velocity.x < 0;
			PSM.AnimPlayer.Play("dodge");
		}
	}

	public override void UpdateState( float _delta)
	{
		base.UpdateState(_delta);

		PSM.Body.MoveAndSlide(PSM.Velocity * PSM.CharacterAttributes["Dodge Speed"].Value);
	}

	public void _on_AnimationPlayer_animation_finished(String anim_name)
	{
		if (anim_name.Contains("dodge"))
			PSM.ChangeState("PlayerIdling");
	}
}

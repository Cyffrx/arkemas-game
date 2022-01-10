using Godot;
using System;
using System.Collections.Generic;

public class Staggered : PlayerState
{
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		PSM.animationPlayer.Play("stagger");
	}

	public void _on_AnimationPlayer_animation_finished(string animName)
	{
		if (animName.Contains("stagger")) PSM.ChangeState("Idling");
	}
}
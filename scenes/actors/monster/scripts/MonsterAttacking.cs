using Godot;
using System;
using System.Collections.Generic;

public class MonsterAttacking : MonsterState
{
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		MSM.AnimPlayer.Play("attack");
	}

	private void _on_AnimationPlayer_animation_finished(string anim_name)
	{
		if (anim_name.Contains("attack"))
			MSM.ChangeState("MonsterIdling");
	}
}

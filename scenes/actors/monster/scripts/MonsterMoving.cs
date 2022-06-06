using Godot;
using System;
using System.Collections.Generic;

public class MonsterMoving : MonsterState
{
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		MSM.Sightline.CastTo = MSM.Target.Position;
		MSM.Sightline.Enabled = true;
	}

	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);
		MSM.AnimPlayer.Play("move");

		if (MSM.PlayerDetected)
			MSM.Target.Position = MSM.Goal.Position;
		
		MSM.Sightline.CastTo = -(MSM.Body.Position - MSM.Target.Position);

		if (MSM.PlayerInAttackRange)
			MSM.ChangeState("MonsterAttacking");
		
		if (MSM.Body.Position.DistanceTo(MSM.Target.Position) > 10.0f)
		{
			MSM.Velocity = MSM.Body.Position.DirectionTo(MSM.Target.Position).Normalized();
			MSM.Body.MoveAndSlide(
				MSM.Velocity * MSM.CharacterAttributes["Run Speed"].Value);
			MSM.Sprite.FlipH = MSM.Velocity.x < 0;
		}
		else
			MSM.ChangeState("MonsterIdling");
				
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);

		MSM.Sightline.Enabled = false;
	}
}

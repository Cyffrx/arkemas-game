using Godot;
using System;
using System.Collections.Generic;

public class MonsterMoving : MonsterState
{
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		MSM.Sightline.CastTo = MSM.Target.Position;
	}

	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);

		MSM.AnimPlayer.Play("move");
		
		// if player deteceted, fetch their location
		if (MSM.PlayerDetected)
			MSM.Target.Position = MSM.Goal.Position;
		
		MSM.Sightline.CastTo = MSM.Body.ToLocal(MSM.Target.Position);

		// if chasing and 50.0f units close, attack
		if (MSM.PlayerDetected && MSM.Body.Position.DistanceTo(MSM.Target.Position) <= 50.0f)
			MSM.ChangeState("MonsterAttacking");
		// if over 50.0f units, chase
		else if (MSM.Body.Position.DistanceTo(MSM.Target.Position) > 50.0f)
		{
			MSM.Velocity = MSM.Body.Position.DirectionTo(MSM.Target.Position).Normalized();
			MSM.Body.MoveAndSlide(
				MSM.Velocity * MSM.CharacterAttributes["Run Speed"].Value);
			MSM.Sprite.FlipH = MSM.Velocity.x < 0;
		}
		// if under 50.0f units and not chasing, idle
		else
			MSM.ChangeState("MonsterIdling");
				
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
	}
}

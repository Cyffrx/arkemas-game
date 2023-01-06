using Godot;
using System;
using System.Collections.Generic;

public class MonsterIdling : MonsterState
{
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		MSM.Timers["Boredem"].WaitTime = MSM.RNG.RandfRange(1.0f, 6.0f);
		MSM.Timers["Boredem"].Start();
	}

	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);
		
		MSM.AnimPlayer.Play("idle");
		if (MSM.PlayerDetected ||
			MSM.Body.Position.DistanceTo(MSM.Target.Position) > 50.0f)
			MSM.ChangeState("MonsterMoving");
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);

		MSM.Timers["Boredem"].Stop();
	}

	private void _on_Boredem_timeout()
	{
		if (MSM.CurrentState != this.Name) return;

		// set target to body
		MSM.Target.Position = MSM.Body.Position;

		// 200 is the detectionRadius. this shouldn't be hardcoded, but oh well
		float detectionRadius = 100.0f;
		float rand = MSM.RNG.RandfRange(-1.0f, 1.0f); // get a randomized rotation
		float dist = Math.Abs(rand) * detectionRadius; // total distance to travel
		float radius = dist > 100.0f ? dist : 100.0f; // minimum distance 50.0f

		// pick a random point within bounds
		Vector2 vec = Vector2.Zero;
		vec.x = MSM.RNG.RandfRange(-detectionRadius, detectionRadius);
		vec.y = MSM.RNG.RandfRange(-detectionRadius, detectionRadius);
		MSM.Target.Position = vec;

		MSM.Timers["Boredem"].Start();
	}
}

using System.Collections.Generic;
using Godot;

// https://gamedevelopment.tutsplus.com/series/understanding-steering-behaviors--gamedev-12732	?

public class BunsterChase : BunsterState
{
	public override void UpdateState(float _delta)
	{
		BSM.Velocity = BSM.Target.CastTo.Normalized();
		BSM.kb.MoveAndSlide(BSM.RunSpeed * BSM.Velocity);

		BSM.animationPlayer.Play("run_"+BSM.Direction);
		
		if (BSM.Target.CastTo.Length() < 500) BSM.ChangeState("BunsterAttacking");
		else if (BSM.Target.CastTo.Length() > 1000.0f) BSM.ChangeState("BunsterWander");
	}
}
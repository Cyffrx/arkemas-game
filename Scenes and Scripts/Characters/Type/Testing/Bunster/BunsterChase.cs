using System.Collections.Generic;
using Godot;

// https://gamedevelopment.tutsplus.com/series/understanding-steering-behaviors--gamedev-12732	?

public class BunsterChase : BunsterState
{
	public override void UpdateState(float _delta)
	{
		BSM.kb.MoveAndSlide(BSM.RunSpeed * BSM.Target.CastTo.Normalized());
	}

	public void _on_SearchRadius_area_exited(Area2D area)
	{
		if (area.IsInGroup("player"))
		{
			BSM.ChangeState("BunsterWander");
		}
	}
}
using System.Collections.Generic;
using Godot;

// https://gamedevelopment.tutsplus.com/series/understanding-steering-behaviors--gamedev-12732	?

public class BunsterChase : BunsterState
{
	RayCast2D rayCast2D;
	public override void _Ready()
	{
		rayCast2D = Owner.GetNode<RayCast2D>("RayCast2D");
	}
	public override void UpdateState(float _delta)
	{

		BSM.Velocity = BSM.kb.ToLocal(BSM.Target.GlobalPosition).Normalized();
		rayCast2D.CastTo = BSM.kb.ToLocal(BSM.Target.GlobalPosition);
		
		BSM.kb.MoveAndSlide(BSM.RunSpeed * BSM.Velocity);

		BSM.animationPlayer.Play("run_"+BSM.Direction);
		
		if (rayCast2D.IsColliding())
			if (BSM.kb.ToLocal(BSM.Target.GlobalPosition).Length() <= 250) BSM.ChangeState("BunsterAttacking");
	}
}
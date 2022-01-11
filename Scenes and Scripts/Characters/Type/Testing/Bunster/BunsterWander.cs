using Godot;

public class BunsterWander : BunsterState
{
	public override void _Ready()
	{
		base._Ready();
	}
	public override void UpdateState(float _delta)
	{
		BSM.animationPlayer.Play("idle_"+BSM.Direction);
		
		// check if bunny near point
		//	if they are, generate a new one
		// move to point

		if (BSM.rayCast2D.IsColliding())
			BSM.moveTo = BSM.roamingArea.GeneratePosition2D();

		if (BSM.kb.GlobalPosition.DistanceTo(BSM.moveTo) < 10.0f)
			BSM.moveTo = BSM.roamingArea.GeneratePosition2D();

		BSM.kb.MoveAndSlide(
			BSM.kb.GlobalPosition.DirectionTo(BSM.moveTo).Normalized() * BSM.WalkSpeed
			);
		
		BSM.rayCast2D.CastTo = BSM.kb.ToLocal(BSM.moveTo);
	}

	public void _on_InteractionZone_area_entered(Area2D area)
	{
		if (area.IsInGroup("aecarialSource"))
		{
			BSM.ChangeState("BunsterChase");
			BSM.Target = area;
		}
	}
}

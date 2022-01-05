using Godot;

public class Idling : PlayerState
{
	public override void UpdateState(float _delta)
	{
		
		PSM.Velocity = Vector2.Zero;
		
		#region movement
		if (Input.IsActionPressed("move_up")) PSM.Velocity.y -= 1;
		if (Input.IsActionPressed("move_down")) PSM.Velocity.y += 1;
		if (Input.IsActionPressed("move_left")) PSM.Velocity.x -= 1;
		if (Input.IsActionPressed("move_right")) PSM.Velocity.x += 1;
		
		if (PSM.Velocity == Vector2.Zero) PSM.animationPlayer.Play("idle_"+PSM.Direction);
		else
		{
			PSM.animationPlayer.Play("run_"+PSM.Direction);
			
			PSM.kb.MoveAndSlide(PSM.Velocity.Normalized() * PSM.RunSpeed);
		}
		#endregion

		#region actions
		if (Input.IsActionJustPressed("action_interact")) {} // ?
		if (Input.IsActionJustPressed("action_attack")) PSM.ChangeState("Attacking");
		if (Input.IsActionJustPressed("action_dodge")) PSM.ChangeState("Dodging");
		#endregion

		// #region casting
		// these state changes should probably carry a reference to what spell they're casting too
		// if (Input.IsActionJustPressed("cast_markAndRecall")) PSM.ChangeState("Casting");
		// if (Input.IsActionJustPressed("cast_pulse")) PSM.ChangeState("Casting");
		// if (Input.IsActionJustPressed("cast_offensive")) PSM.ChangeState("Casting");
		// if (Input.IsActionJustPressed("cast_defensive")) PSM.ChangeState("Casting");
		// #endregion
	}
}

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
		
		PSM.kb.MoveAndSlide(PSM.Velocity.Normalized() * PSM.RunSpeed);
		#endregion

		#region animation
		if (PSM.Velocity == Vector2.Zero) PSM.animationPlayer.Play("idle_"+PSM.Direction);
		else PSM.animationPlayer.Play("run_"+PSM.Direction);
		#endregion

		#region actions
		if (Input.IsActionJustPressed("action_attack")) PSM.ChangeState("Attacking");
		if (Input.IsActionJustPressed("action_dodge")) PSM.ChangeState("Dodging");
		PSM.interactionZone.Monitoring = Input.IsActionJustPressed("action_interact");
		#endregion

		#region audio
		if (PSM.Velocity != Vector2.Zero && !PSM.soundPlayer.Playing)
		{
			PSM.soundPlayer.Stream = PSM.footstepSound;
			PSM.soundPlayer.Play();
		}
		#endregion
	}
}

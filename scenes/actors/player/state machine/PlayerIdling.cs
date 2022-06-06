using Godot;

public class PlayerIdling : PlayerState
{
	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);

		PSM.Velocity = Vector2.Zero;
		PSM.Sprite.FlipH = false;
		// play idle animation
		PSM.AnimPlayer.Play("idle");

		// move
		if (Input.IsActionPressed("move_up")
			|| Input.IsActionPressed("move_down")
			|| Input.IsActionPressed("move_left")
			|| Input.IsActionPressed("move_right"))
			PSM.ChangeState("PlayerMoving");

		if (Input.IsActionJustPressed("action_attack"))
			PSM.ChangeState("PlayerAttacking");
		if (Input.IsActionJustPressed("action_dodge"))
			PSM.ChangeState("PlayerDodging");
	}
}

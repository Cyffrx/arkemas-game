using Godot;

public class PlayerMoving : PlayerState
{
	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);

		// face direction of last velocity ; defaults right
		PSM.Sprite.FlipH = PSM.Velocity.x < 0;
		// play idle animation
		PSM.AnimPlayer.Play("move");

		PSM.Velocity = Vector2.Zero;

		// move
		if (Input.IsActionPressed("move_up"))
			PSM.Velocity.y = -1;
		if (Input.IsActionPressed("move_down"))
			PSM.Velocity.y = 1;
		if (Input.IsActionPressed("move_left"))
			PSM.Velocity.x = -1;
		if (Input.IsActionPressed("move_right"))
			PSM.Velocity.x = 1;
		
		PSM.Velocity = PSM.Velocity.Normalized();
		
		PSM.Body.MoveAndSlide( PSM.Velocity * PSM.CharacterAttributes["Run Speed"].Value);

		if (PSM.Velocity == Vector2.Zero)
			PSM.ChangeState("PlayerIdling");

		if (Input.IsActionJustPressed("action_attack"))
			PSM.ChangeState("PlayerAttacking");
		if (Input.IsActionJustPressed("action_dodge"))
			PSM.ChangeState("PlayerDodging");
	}
}

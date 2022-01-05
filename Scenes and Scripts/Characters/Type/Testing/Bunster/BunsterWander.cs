using Godot;

public class BunsterWander : BunsterState
{
	private Vector2 _velocity;
	Timer boredTimer;
	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _Ready()
	{
		base._Ready();

		rng.Randomize();
		boredTimer = GetChild<Timer>(0);
		_velocity = Vector2.Right;
		PickRandomDirection();
	}
	public override void UpdateState(float _delta)
	{
		BSM.kb.MoveAndSlide(BSM.WalkSpeed * _velocity.Normalized());
	}
	
	public void PickRandomDirection() { _velocity = _velocity.Rotated(rng.RandfRange(-2.0f, 2.0f)); }

	public void _on_BoredTimer_timeout()
	{
		PickRandomDirection();
		boredTimer.Start();
	}
}
using Godot;
using System.Linq;
using System.Collections.Generic;

// this might be portable to every actor
public class ActorStateMachine : _DefaultStateMachine
{
	#region stats
	public Stat Health;
	public Stat Aecarium;
	public Stat Stamina;

		#region movement stats
		[Export] public float WalkSpeed;
		[Export] public float RunSpeed;
		[Export] public float Acceleration;
		[Export] public float DodgeSpeed;
		#endregion
	#endregion

	#region events
	[Signal] public delegate void HealthChanged();
	[Signal] public delegate void Died();
	#endregion

	public AnimationPlayer animationPlayer;	// need to make my own animation player to handle directional assignment
	public KinematicBody2D kb;
	public Vector2 Velocity;
	public Sprite sprite; // temporary

	public override void _Ready()
	{
		base._Ready();

		Velocity = Vector2.Zero;
		animationPlayer = Owner.GetNode<AnimationPlayer>("AnimationPlayer");
		kb = (KinematicBody2D) Owner;
		
		sprite = Owner.GetNode<Sprite>("Sprite");

		// List<ActorState> actorStates = this.GetChildren().OfType<ActorState>().ToList();
		// for (int i = 0; i < actorStates.Count; i++) actorStates[i].ASM = this;

		// ChangeState(actorStates[0].Name);
	}

	#region temporary
	public void CheckFlipSprite(Vector2 facing)
	{
		sprite.FlipH = facing.x < 0;
		if (sprite.FlipH) sprite.Position = new Vector2(-56,0);
		else sprite.Position = new Vector2(0,0);
	}
	#endregion
	
	#region health / life state
	public virtual void Hurt(int value) 
	{ 
		EmitSignal(nameof(HealthChanged));
		Health.Value -= value;
		CheckHealthStatus();
	}

	public virtual void Heal(int value)
	{
		EmitSignal(nameof(HealthChanged));
		Health.Value -= value;
		CheckHealthStatus();
	}

	public virtual void CheckHealthStatus()
	{ if (Health.Value <= 0) Die(); }

	public virtual void Die()
	{
		EmitSignal(nameof(Died));
		Owner.QueueFree();
	}

	#endregion
	public class Stat
	{
		public readonly string Name;
		
		// max value
		private int _defaultMax;
		private int _max;
		// value
		private int _defaultValue;
		private int _value;
		// min value
		private int _defaultMin;
		private int _min;

		public int Max {get { return _max; } set {_max = value;}}
		public int Value {get {return _value;} set {_value = _checkLimits(value);}}
		public int Min {get { return _min; } set {_min = value;}}

		private int _checkLimits (int value) { return (value <= _max && value >= _min) ? value : _value; }

		public Stat(string name, int maxValue, int minValue, int value)
		{
			this.Name = name;
			this._defaultMax = maxValue;
			this._defaultMin = minValue;
			this._defaultValue = value;

			Reset();
		}

		public void Reset()
		{
			this.Max = this._defaultMax;
			this.Min = this._defaultMin;
			this.Value = this._defaultValue;
		}
	}

	#region convert velocity to directional integer
	public int Direction
	{
		get
		{
			if (Velocity.x == -1) return 3;
			else if (Velocity.x == 1) return 1;
			else if (Velocity.y == -1) return 2;
			else return 0;
		}
	}
	#endregion
}
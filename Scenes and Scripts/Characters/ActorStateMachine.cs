using Godot;
using System.Linq;
using System.Collections.Generic;

// this might be portable to every actor
public class ActorStateMachine : _DefaultStateMachine
{
	#region stats
	public Attribute Health;
	public Attribute Aecarium;
	public Attribute Stamina;

		#region movement stats
		public float WalkSpeed;
		public float RunSpeed;
		public float Acceleration;
		public float DodgeSpeed;
		#endregion
	#endregion

	#region events
	[Signal] public delegate void Died();
	#endregion

	public AnimationPlayer animationPlayer;	// need to make my own animation player to handle directional assignment
	public AudioStreamPlayer2D soundPlayer;	// need to make my own animation player to handle directional assignment
	public KinematicBody2D kb;
	public Vector2 Velocity;
	public Sprite sprite; // temporary

	public override void _Ready()
	{
		base._Ready();

		Velocity = Vector2.Zero;
		animationPlayer = Owner.GetNode<AnimationPlayer>("AnimationPlayer");
		soundPlayer = Owner.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		kb = (KinematicBody2D) Owner;
		
		sprite = Owner.GetNode<Sprite>("Sprite");

		// List<ActorState> actorStates = this.GetChildren().OfType<ActorState>().ToList();
		// for (int i = 0; i < actorStates.Count; i++) actorStates[i].ASM = this;

		// ChangeState(actorStates[0].Name);
	}

	public override void _Process(float delta)
	{
		if (Health.Value <= 0) Die();
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
		Health.Value -= value;
	}

	public virtual void Heal(int value)
	{
		Health.Value += value;
	}

	public virtual void Die()
	{
		EmitSignal(nameof(Died));
		Owner.QueueFree();
	}
	#endregion

	#region convert velocity to directional integer
	public int Direction
	{
		get
		{
			if (Velocity.x < 0) return 3;
			else if (Velocity.x > 0) return 1;
			else if (Velocity.y < 0) return 2;
			else return 0;
		}
	}
	#endregion
}
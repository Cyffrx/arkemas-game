using Godot;
using System;
using System.Collections.Generic;

public class ActorStats : Node
{
	public Stat Health;
	public Stat Stamina;
	public Stat Aecarium;
	[Export] public float WalkSpeed;
	[Export] public float RunSpeed;
	[Export] public float DodgeSpeed;
	
	[Signal] public delegate void HealthChanged();
	[Signal] public delegate void Died();

	public override void _Ready()
	{
		base._Ready();
		if (
			Health == null
			|| Stamina == null
			|| Aecarium == null
		) throw new Exception("Cannot create actor without stats.");
	}

	public virtual void Damage(int value) 
	{ 
		EmitSignal(nameof(HealthChanged));
		Health.Value -= value;
		CheckHealthStatus();
	}

	public virtual void Heal(int value)
	{
		EmitSignal(nameof(HealthChanged));
		Health.Value += value;
		CheckHealthStatus();
	}

	public virtual void CheckHealthStatus()
	{ if (Health.Value <= 0) Die(); }

	public virtual void Die()
	{
		EmitSignal(nameof(Died));
		Owner.QueueFree();
	}

	public class Stat
	{
		private readonly int DefaultValue;
		private int _value;
		public readonly string Name;
		public int MaxValue;
		public int Value {
			get{ GD.Print($"{this.Name} was queried"); return _value;} 
			set{SetValue(value);}
		}

		public Stat(string name, int maxValue, int startingValue = 0)
		{
			this.Name = name;
			this.MaxValue = maxValue;
			this._value = startingValue; 
			this.DefaultValue = startingValue;
			
		}

		private void SetValue(int value)
		{
			if (_value+value < 0 ) _value = 0;
			else if (_value+value > MaxValue) _value = MaxValue;
			else _value = value;

			GD.Print($"{Name} changed by {value} and is now {Value}" );
		}

		// could use a drain that returns true so long as some stamina is left and removes a value from current stamina
		// could implement a regen to max
	}
}

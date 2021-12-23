using Godot;
using System;
using System.Collections.Generic;

public class ActorStats : Node
{
	#region stats
	[Export] public int Health;
	[Export] public int Stamina;
	[Export] public int Aecarium;

	[Export] public int MaxHealth;
	[Export] public int MaxStamina;
	[Export] public int MaxAecarium;
	#endregion

	[Export] public float WalkSpeed;
	[Export] public float RunSpeed;
	[Export] public float DodgeSpeed;
	
	[Signal] public delegate void HealthChanged();
	[Signal] public delegate void Died();

	public virtual void Damage(int value) 
	{ 
		EmitSignal(nameof(HealthChanged));
		Health -= value;
		CheckHealthStatus();
	}

	public virtual void Heal(int value)
	{
		EmitSignal(nameof(HealthChanged));
		Health += value;
		CheckHealthStatus();
	}

	public virtual void CheckHealthStatus()
	{ if (Health <= 0) Die(); }

	public virtual void Die()
	{
		EmitSignal(nameof(Died));
		Owner.QueueFree();
	}
}

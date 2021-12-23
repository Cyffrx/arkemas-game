using Godot;
using System;

public class PlayerStats : ActorStats
{
	public int MeleeAttackDamage = 5;
	public int MeleeAttackCost = 1;
	public int DodgeCost = 1;
	public float Momentum = 0;

	public override void _Ready()
	{
		Health = 8;
		Stamina = 8;
		Aecarium = 8;

		MaxHealth = 8;
		MaxStamina = 8;
		MaxAecarium = 8;

		MinHealth = 0;
		MinStamina = -2;
		MinAecarium = -MaxAecarium;

		WalkSpeed = 200;
		RunSpeed = 400;
		DodgeSpeed = 600;

		base._Ready();
	}

	public override void Die()
	{
		Heal(50);
	}
	
	public void _on_AecarialDecay_timeout()
	{ 
		Aecarium -= 1; 
		if (Aecarium <= 0) GD.Print($"{Owner.Name} husked!");
	}

	public void _on_HealthRegen_timeout()
	{ if (Health < MaxHealth) Health += 1; }

	public void _on_StaminaRegen_timeout() 
	{ 
		if (Stamina < MaxStamina) 
			if (Stamina < MinStamina) Stamina = MinStamina; 
			else Stamina += 1;
	}

	public void _on_ReportStats_timeout()
	{
		GD.Print($"Aecarium: {Aecarium}");
		GD.Print($"Health: {Health}");
		GD.Print($"Stamina: {Stamina}");
	}
}

using Godot;
using System;

public class PlayerStats : ActorStats
{
	public int MeleeAttackDamage = 5;
	public int MeleeAttackCost = 1;
	public int DodgeCost = 1;
	public float Momentum = 0;

	ProgressBar AecariumBar;
	ProgressBar HealthBar;
	ProgressBar StaminaBar;

	Light2D AecarialLight;


	public override void _Ready()
	{
		Health = 8;
		Stamina = 8;
		Aecarium = 88;

		MaxHealth = 8;
		MaxStamina = 8;
		MaxAecarium = 88;

		MinHealth = 0;
		MinStamina = -2;
		MinAecarium = -MaxAecarium;

		WalkSpeed = 200;
		RunSpeed = 400;
		DodgeSpeed = 600;

		AecariumBar = GetNode<ProgressBar>("/root/MainScene/GUI/HUD/VBoxContainer/AecariumBar");
		HealthBar = GetNode<ProgressBar>("/root/MainScene/GUI/HUD/VBoxContainer/HBoxContainer/HealthBar");
		StaminaBar = GetNode<ProgressBar>("/root/MainScene/GUI/HUD/VBoxContainer/HBoxContainer/StaminaBar");

		AecariumBar.MaxValue = MaxAecarium;
		HealthBar.MaxValue = MaxHealth;
		StaminaBar.MaxValue = MaxStamina;

		AecariumBar.Value = Aecarium;
		HealthBar.Value = Health;
		StaminaBar.Value = Stamina;

		AecarialLight = Owner.GetNode<Light2D>("Light2D");

		base._Ready();
	}

	
	public override void _Process(float delta)
	{
		base._Process(delta);

		AecariumBar.Value = Aecarium;
		HealthBar.Value = Health;
		StaminaBar.Value = Stamina;
	}

	public override void Die()
	{
		Heal(50);
	}
	
	public void _on_AecarialDecay_timeout()
	{ 
		Aecarium -= 1;
		
		if (Aecarium <= 0) GD.Print($"{Owner.Name} husked!"); // maybe the light radius begins growing again but has a tint and highlights aecarial things?
		else AecarialLight.Scale = new Vector2((Aecarium*100/MaxAecarium)/100.0f, (Aecarium*100/MaxAecarium)/100.0f)*2;
	}

	public void _on_HealthRegen_timeout() { if (Health < MaxHealth) Health += 1; }

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

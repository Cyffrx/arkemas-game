using Godot;
using System;

public class PlayerStats : ActorStats
{
	public override void _Ready()
	{
		Health = new ActorStats.Stat("Health", 50);
		Stamina = new ActorStats.Stat("Stamina", 50);
		Aecarium = new ActorStats.Stat("Aecarium", 300);

		WalkSpeed = 200;
		RunSpeed = 400;
		DodgeSpeed = 1000;

		base._Ready();
	}

	public override void Die()
	{
		GD.Print("You died.");
		Heal(50);
	}
}

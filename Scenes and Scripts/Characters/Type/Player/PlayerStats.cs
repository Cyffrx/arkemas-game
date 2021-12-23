using Godot;
using System;

public class PlayerStats : ActorStats
{
	public override void _Ready()
	{
		Health = 8;
		Stamina = 8;
		Aecarium = 8;

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

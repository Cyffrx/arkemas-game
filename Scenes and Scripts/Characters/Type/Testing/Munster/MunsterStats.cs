using Godot;
using System;

public class MunsterStats : ActorStats
{
   public override void _Ready()
	{
		Health = new ActorStats.Stat("Health", 5);
		Stamina = new ActorStats.Stat("Stamina", 5);
		Aecarium = new ActorStats.Stat("Aecarium", 0);

		WalkSpeed = 50;
		RunSpeed = 200;
		DodgeSpeed = 400;

		base._Ready();
	}

	public override void Damage(int damage)
	{
		base.Damage(damage);
		GD.Print("I was damaged. My health is now " + Health);
	}
}

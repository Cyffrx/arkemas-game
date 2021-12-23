using Godot;
using System;

public class MunsterStats : ActorStats
{
   public override void _Ready()
	{
		Health = 1;
		Stamina = 1;
		Aecarium = -1;

		WalkSpeed = 50;
		RunSpeed = 200;
		DodgeSpeed = 400;

		base._Ready();
	}
}

using Godot;
using System;

public class MunsterStats : ActorStats
{
   public override void _Ready()
	{
		Health = new Stat("Health", 2,0,2);
		
		WalkSpeed = 50;
		RunSpeed = 200;
		DodgeSpeed = 400;

		base._Ready();
	}
}

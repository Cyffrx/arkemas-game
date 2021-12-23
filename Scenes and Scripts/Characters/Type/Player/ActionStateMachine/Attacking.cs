using Godot;
using System;
using System.Collections.Generic;

public class Attacking : ActionState
{
	private Area2D damageArea;
	[Export] private int StaminaCost = 2;
	Node playerStats;
	
	public override void _Ready()
	{
		base._Ready();
		damageArea = Owner.GetNode<Area2D>("DamageArea");
		playerStats = Owner.GetNode<Node>("Stats");
	}

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		if ( (int) playerStats.Get("Stamina") > 0) ASM.animPlayer.Play("attack1");
		else ASM.ChangeState("Default");
	}

	public void _on_DamageArea_area_entered(Area2D area)
	{ 
		if (area.IsInGroup("enemies"))
		{
			GD.Print("hit enemy");
			area.Owner.GetNode("Stats").Call("Damage", 5);
		}
	}

	public void _on_AnimationPlayer_animation_finished(string animName)
	{ if (animName == "attack1") ASM.ChangeState("Default"); }
}

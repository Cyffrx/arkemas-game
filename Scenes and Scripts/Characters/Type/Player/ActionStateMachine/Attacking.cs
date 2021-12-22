using Godot;
using System;
using System.Collections.Generic;

public class Attacking : ActionState
{
	private Area2D damageArea;
	[Export] private int StaminaRequirement = 15;
	
	public override void _Ready()
	{
		base._Ready();
		damageArea = Owner.GetNode<Area2D>("DamageArea");
	}

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		if ((int)Owner.GetNode<Node>("Stats").Get("Stamina.Value") < StaminaRequirement) ASM.ChangeState("Default");
		else Owner.GetNode<Node>("Stats").Set("Stamina.Value", -StaminaRequirement);
		
		ASM.animPlayer.Play("attack1");
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

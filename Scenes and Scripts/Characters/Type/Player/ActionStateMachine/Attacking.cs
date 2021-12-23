	using Godot;
using System;
using System.Collections.Generic;

public class Attacking : ActionState
{
	private Area2D damageArea;
	[Export] private int StaminaCost = 2;
	private bool queueAttack;
	private bool dodgeAttack;
	
	public override void _Ready()
	{
		base._Ready();
		damageArea = Owner.GetNode<Area2D>("DamageArea");
	}

	public override void OnStart(Dictionary<string, object> message = null)
	{
		base.OnStart(message);

		int playerStamina = (int) ASM.playerStats.Get("Stamina");

		if ( playerStamina > 0)
		{
			ASM.playerStats.Set("Stamina", playerStamina - StaminaCost);
			if (message == null) ASM.animPlayer.Play("attack");
			else
			{
				if ((bool) message["dodge-strike"]) ASM.animPlayer.Play("dodge-strike");
				else if ((bool) message["attack-combo"]) ASM.animPlayer.Play("attack-combo");
			}

			ASM.sprite.FlipH = ASM.kb.GetLocalMousePosition().x < 0;
		}
		else {ASM.ChangeState("Default"); GD.Print("out of stamina");}
	}

	public override void UpdateState(float _delta)
	{
		if (Input.IsActionJustPressed("action_attack")) queueAttack = true;
	}

	public void _on_DamageArea_area_entered(Area2D area)
	{  if (area.IsInGroup("enemies")) area.Owner.GetNode("Stats").Call("Damage", 5); }

	public void _on_AnimationPlayer_animation_finished(string animName)
	{ 
		if (animName == "attack" || animName == "dodge-strike")
		{
			if (queueAttack) { ASM.ChangeState("Attacking"); queueAttack = false; }
			else ASM.ChangeState("Default");
		}
		// although it's the same thing attack-combo should become attack and attack attack combo
		else if (animName == "attack-combo")
		{
			if (queueAttack) { ASM.ChangeState("Attacking"); queueAttack = false; }
			else ASM.ChangeState("Default");
		}
	}
}

using Godot;
using System;
using System.Collections.Generic;

public class DefaultAttacking : ActorState
{
	private bool _comboAttack;
	[Export] private int StaminaCost;
	[Export] private int AttackDamage;

	private ActorStateMachine.Stat _attackChainState;

	public override void OnStart(Dictionary<string, object> message = null)
	{
		base.OnStart(message);

		if (ASM.Stamina.Value > 0)
		{
			ASM.Stamina.Value -= StaminaCost;
			
			if (ASM.LastState == "Dodging") {} // play dodge attack
			else {_attackChainState.Value++;} // play next attack in chain
		}
		else ASM.ChangeState("Idling");
	}

	public void _on_DamageArea_area_entered(Area2D area)
	{ if (area.IsInGroup("hurtbox")) area.Owner.GetNode("StateMachine").Call("Damage", AttackDamage); }

	public void _on_AnimationPlayer_animation_finished(string animName)
	{
		if (animName.Contains("attack"))
		{
			if (_comboAttack) ASM.ChangeState("Attacking");
			else 
			{
				_attackChainState.Value = _attackChainState.Min;
				ASM.ChangeState("Idling");
			}
		}
	}
}
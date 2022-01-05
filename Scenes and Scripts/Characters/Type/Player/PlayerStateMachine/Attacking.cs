using Godot;
using System;
using System.Collections.Generic;

public class Attacking : PlayerState
{
	private bool _comboAttack;
	[Export] private int StaminaCost;
	[Export] private int AttackDamage;
	private ActorStateMachine.Stat _attackChainState;

	public override void _Ready()
	{
		base._Ready();

		_comboAttack = false;
		StaminaCost = 1;
		AttackDamage = 2;
		_attackChainState = new ActorStateMachine.Stat("AttackChain", 2, 0, 0);
	}

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		//if (PSM.Stamina.Value > 0)
		if (true)
		{
			PSM.Stamina.Value -= StaminaCost;
			
			// this has a custom direction decider since mouse is a thing... later

			PSM.animationPlayer.Play("attack-chain_"+ _attackChainState.Value++ +"_"+PSM.Direction);
		}
		else PSM.ChangeState("Idling");
	}

	public override void UpdateState(float _delta)
	{
		if (Input.IsActionJustPressed("action_attack")) _comboAttack = true;

		// only want to continue forwards if the direction is being held
		Vector2 hold_velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_up")) hold_velocity.y = -1;
		if (Input.IsActionPressed("move_down")) hold_velocity.y = 1;
		if (Input.IsActionPressed("move_left")) hold_velocity.x = -1;
		if (Input.IsActionPressed("move_right")) hold_velocity.x = 1;

		PSM.Velocity = hold_velocity.Normalized() != PSM.Velocity ? Vector2.Zero: PSM.Velocity;

		PSM.kb.MoveAndSlide( PSM.WalkSpeed * PSM.Velocity.Normalized());
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);

		_attackChainState.Value = _attackChainState.Value == _attackChainState.Max ? _attackChainState.Min : _attackChainState.Value;
		_comboAttack = false;
	}

	public void _on_DamageArea_area_entered(Area2D area)
	{ if (area.IsInGroup("hurtbox")) area.Owner.GetNode("StateMachine").Call("Damage", AttackDamage); }


	public void _on_AnimationPlayer_animation_finished(string animName)
	{
		if (animName.Contains("attack"))
		{
			if (_comboAttack) PSM.ChangeState("Attacking");
			else 
			{
				_attackChainState.Value = _attackChainState.Min;
				PSM.ChangeState("Idling");
			}
		}
	}
}

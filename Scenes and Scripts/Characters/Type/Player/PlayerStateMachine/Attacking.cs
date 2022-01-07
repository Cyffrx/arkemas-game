using Godot;
using System;
using System.Collections.Generic;

public class Attacking : PlayerState
{
	private bool _comboAttack = false;
	private bool _hitStun = false;
	[Export] private int StaminaCost = 1;
	[Export] private int AttackDamage = 1;
	private Attribute _attackChainState;

	public override void _Ready()
	{
		base._Ready();

		_attackChainState = new Attribute("AttackChain", 2, 0, 0);
	}

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		if (PSM.Stamina.Value > 0)
		{
			PSM.Stamina.Value -= StaminaCost;
			PSM.StaminaDelayTimer.Start();

			PSM.soundPlayer.Stream = PSM.attackSound;
			PSM.soundPlayer.Play();
			
			// this has a custom direction decider since mouse is a thing... later
			float attackVelocity = PSM.kb.GetLocalMousePosition().Normalized().Angle();
			int attackDirection = 0;

			if (attackVelocity > 0)
			{
				if (attackVelocity < 1.0f) attackDirection = 1;
				else if (attackVelocity < 2.0f) attackDirection = 0;
				else attackDirection = 3;
			}
			else 
			{
				if (attackVelocity > -1.0f) attackDirection = 1;
				else if (attackVelocity > -2.0f) attackDirection = 2;
				else attackDirection = 3;
			}
			
			PSM.animationPlayer.Play("attack-chain_"+ _attackChainState.Value++ +"_"+attackDirection);
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
	{
		if (area.IsInGroup("hurtbox") && Owner != area.Owner && !_hitStun) 
		{
			area.Owner.GetNode("StateMachine").Call("Hurt", AttackDamage);
			_hitStun = true;
		}
	}

	public void _on_AnimationPlayer_animation_finished(string animName)
	{
		if (animName.Contains("attack"))
		{
			_hitStun = false;
			if (_comboAttack) PSM.ChangeState("Attacking");
			else 
			{
				_attackChainState.Value = _attackChainState.Min;
				PSM.ChangeState("Idling");
			}
		}
	}
}
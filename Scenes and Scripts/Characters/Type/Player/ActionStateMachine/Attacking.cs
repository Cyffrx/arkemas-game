using Godot;
using System;
using System.Collections.Generic;

public class Attacking : ActionState
{
	private Area2D _damageArea;
	private Vector2 _velocity = Vector2.Zero;

	private enum AttackDirectionState
	{
		DOWN = 0,
		RIGHT = 1,
		UP = 2,
		LEFT = 3
	}

	private AttackDirectionState _attackDirectionState;
	private bool comboAttack;
	private int attackChain = 0;
	private int attackChainMax = 2;

	public override void _Ready()
	{
		base._Ready();
		_damageArea = Owner.GetNode<Area2D>("DamageArea");
		_damageArea.Monitoring = false;
	}

	public override void OnStart(Dictionary<string, object> message = null)
	{
		base.OnStart(message);

		int playerStamina = (int) ASM.playerStats.Get("Stamina");

		if ( playerStamina > 0)
		{
			#region attack is true
				ASM.playerStats.Set("Stamina", playerStamina - (int) ASM.playerStats.Get("MeleeAttackCost"));

				#region determine attack direction
				// get movement direction
				_velocity = Vector2.Zero;
				if (Input.IsActionPressed("move_up")) _velocity.y -= 1;
				if (Input.IsActionPressed("move_down")) _velocity.y += 1;
				if (Input.IsActionPressed("move_left")) _velocity.x -= 1;
				if (Input.IsActionPressed("move_right")) _velocity.x += 1;
				_velocity = _velocity.Normalized();

				// get mouse direction
				Vector2 mouseDirection = ASM.kb.GetLocalMousePosition().Normalized();

				ASM.CheckFlipSprite(mouseDirection);

				// determine attack direction & set collison box
				_attackDirectionState = AttackDirectionState.RIGHT;
				#endregion

				#region determine attack
				
				// handles which animations play
				// animations named by [name]_[direction]_[order]
				if (ASM.LastState == "Dodging") ASM.animPlayer.Play($"dodge-attack_{(int)_attackDirectionState}_0");
				else ASM.animPlayer.Play($"attack-chain_{(int)_attackDirectionState}_{attackChain}");

				#endregion
			#endregion
		}
		else ASM.ChangeState("Default");
	}

	public override void UpdateState(float _delta)
	{
		if (Input.IsActionJustPressed("action_attack")) comboAttack = true;

		// only want to continue forwards if the direction is being held
		Vector2 hold_velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_up")) hold_velocity.y = -1;
		if (Input.IsActionPressed("move_down")) hold_velocity.y = 1;
		if (Input.IsActionPressed("move_left")) hold_velocity.x = -1;
		if (Input.IsActionPressed("move_right")) hold_velocity.x = 1;

		_velocity = hold_velocity.Normalized() != _velocity ? Vector2.Zero: _velocity;

		ASM.kb.MoveAndSlide( (float) ASM.playerStats.Get("WalkSpeed") * _velocity.Normalized());
	}

	public void _on_DamageArea_area_entered(Area2D area)
	{  if (area.IsInGroup("enemies")) area.Owner.GetNode("Stats").Call("Damage", (int) ASM.playerStats.Get("MeleeAttackDamage")); }

	public void _on_AnimationPlayer_animation_finished(string animName)
	{
		if (animName.Contains("attack"))
		{
			if (comboAttack) 
			{ 
				if (++attackChain == attackChainMax) attackChain = 0;
				ASM.ChangeState("Attacking");
				comboAttack = false;
			}
			else 
			{
				attackChain = 0;
				ASM.ChangeState("Default");
			}
		}
	}
}

// attacks should be changeable / cancelable for about half the startup frames
// attacks might cost 1 stamina but you can attack without it, it just reduces your damage
// attacks might have a sweet spot to combo in that gives better or quicker damage

using Godot;
using System;
using System.Collections.Generic;

public class Attacking : ActionState
{
	private Area2D _damageArea;
	private Vector2 _velocity = Vector2.Zero;

	private enum AttackDirectionState
	{
		FORWARD,
		LEFTSIDE,
		RIGHTSIDE,
		BEHIND
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

				// determine attack direction & set collison box
				_attackDirectionState = AttackDirectionState.FORWARD;
				#endregion

				#region determine attack
				// this animation system could work pretty well for a tighter sword-focused game
				switch (ASM.LastState)
				{
					case "Dodging":
						switch (_attackDirectionState)
						{
							case AttackDirectionState.LEFTSIDE:
							//	play left dodge side strike
							ASM.animPlayer.Play("dodge-attack");
							break;
							case AttackDirectionState.RIGHTSIDE:
							//	play right dodge side strike
							ASM.animPlayer.Play("dodge-attack");
							break;
							case AttackDirectionState.BEHIND:
							// play turnaround slash
							ASM.animPlayer.Play("dodge-attack");
							break;
							default:	// forward
							//	play dodge strike
							ASM.animPlayer.Play("dodge-attack");
							break;
						}
					break;
					case "Attacking":
						switch (_attackDirectionState)
						{
							case AttackDirectionState.LEFTSIDE:
							//	play next combo
							ASM.animPlayer.Play($"attack-chain{attackChain}");
							break;
							case AttackDirectionState.FORWARD:
							//	play next combo
							ASM.animPlayer.Play($"attack-chain{attackChain}");
							break;
							case AttackDirectionState.BEHIND:
							//	play turnaround slash slash
							ASM.animPlayer.Play($"attack-chain{attackChain}");
							break;
							default:	// forward
							//	play next combo
							ASM.animPlayer.Play($"attack-chain{attackChain}");
							break;
						}
					break;
					default: // running / walking / idle attack
						// momentum-based attacks
						if (ASM.playerStats.Get("Momentum") == ASM.playerStats.Get("RunSpeed"))
						{
							switch (_attackDirectionState)
							{
								case AttackDirectionState.LEFTSIDE:
								//	play left side sweep - weak attack while moving
								ASM.animPlayer.Play("dodge-attack");
								break;
								case AttackDirectionState.RIGHTSIDE:
								//	play right side sweep - weak attack while moving
								ASM.animPlayer.Play("dodge-attack");
								break;
								case AttackDirectionState.BEHIND:
								//	play spin slash
								ASM.animPlayer.Play("dodge-attack");
								break;
								default:	// forward
								//	play lunge
								ASM.animPlayer.Play("dodge-attack");
								break;
							}
						}
						else {
							// static attacks
							switch (_attackDirectionState)
							{
								case AttackDirectionState.BEHIND:
								ASM.animPlayer.Play($"attack-chain{attackChain}");
								break;
								default:	// backwards
								// play turnaorund attack
								ASM.animPlayer.Play($"attack-chain{attackChain}");
								break;
							}
						}
					break;
				}
				#endregion
			#endregion
		}
		else {ASM.ChangeState("Default"); GD.Print("out of stamina");}
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

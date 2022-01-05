using System.Collections.Generic;
using Godot;

public class BunsterAttacking : BunsterState
{
	private int AttackDamage = 1;

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		BSM.animationPlayer.Play("attack_" + BSM.Direction);
	}
	public override void UpdateState(float _delta)
	{
		BSM.kb.MoveAndSlide(BSM.RunSpeed * BSM.Velocity);
	}

	public void _on_AnimationPlayer_animation_finished(string animName)
	{
		if (animName.Contains("attack")) BSM.ChangeState("BunsterChase");
	}

	public void _on_DamageArea_area_entered(Area2D area)
	{
		if (area.IsInGroup("hurtbox") && Owner != area.Owner)
		{
			GD.Print("Hurt " + area.Owner.Name);
			area.Owner.GetNode("StateMachine").Call("Hurt", AttackDamage);
		}	
	}
}
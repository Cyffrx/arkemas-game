using Godot;
using System.Linq;
using System.Collections.Generic;

public class PlayerStateMachine : ActorStateMachine
{
	
	public Timer AecariumDecayTimer;
	public Timer HealthRegenTimer;
	public Timer StaminaRegenTimer;
	public Timer StaminaDelayTimer;

    public override void _Ready()
    {
        base._Ready();

        Aecarium = new Attribute("Aecarium", 88, -88, 88);
        Health = new Attribute("Health", 8, 0, 8);
        Stamina = new Attribute("Stamina", 8, 0, 8);

        RunSpeed = 200;
        WalkSpeed = 100;
        DodgeSpeed = 400;

		AecariumDecayTimer = Owner.GetNode<Timer>("Timers/AecariumDecay");
		HealthRegenTimer = Owner.GetNode<Timer>("Timers/HealthRegen");
		StaminaRegenTimer = Owner.GetNode<Timer>("Timers/StaminaRegen");
		StaminaDelayTimer = Owner.GetNode<Timer>("Timers/StaminaDelay");

        List<PlayerState> playerStates = this.GetChildren().OfType<PlayerState>().ToList();
		for (int i = 0; i < playerStates.Count; i++) playerStates[i].PSM = this;

		ChangeState(playerStates[0].Name);
    }

	public void _on_AecariumDecay_timeout()
	{
		Aecarium.Value -= 1;
		AecariumDecayTimer.Start();
	}
	public void _on_StaminaRegen_timeout()
	{
		if (StaminaDelayTimer.IsStopped())
			Stamina.Value += 1;

		StaminaRegenTimer.Start();
	}
	public void _on_HealthRegen_timeout() 
	{
		Heal(1);
		HealthRegenTimer.Start();
	}

	public override void Hurt(int value)
	{
		ChangeState("Staggered");

		base.Hurt(value);
	}
}
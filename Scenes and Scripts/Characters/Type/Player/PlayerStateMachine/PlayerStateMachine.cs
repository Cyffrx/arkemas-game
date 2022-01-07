using Godot;
using System.Linq;
using System.Collections.Generic;

public class PlayerStateMachine : ActorStateMachine
{
	
	Timer aecariumDecayTimer;
	Timer healthRegenTimer;
	Timer staminaRegenTimer;
	public Timer StaminaDelayTimer;

	private Light2D aecarialLight;

    public override void _Ready()
    {
        base._Ready();

        Aecarium = new Attribute("Aecarium", 300, 0, 300);
        Health = new Attribute("Health", 8, 0, 8);
        Stamina = new Attribute("Stamina", 8, 0, 8);

        RunSpeed = 100;
        WalkSpeed = 25;
        DodgeSpeed = 150;

		aecariumDecayTimer = Owner.GetNode<Timer>("Timers/AecariumDecay");
		healthRegenTimer = Owner.GetNode<Timer>("Timers/HealthRegen");
		staminaRegenTimer = Owner.GetNode<Timer>("Timers/StaminaRegen");
		StaminaDelayTimer = Owner.GetNode<Timer>("Timers/StaminaDelay");
		aecarialLight = Owner.GetNode<Light2D>("Light2D");

        List<PlayerState> playerStates = this.GetChildren().OfType<PlayerState>().ToList();
		for (int i = 0; i < playerStates.Count; i++) playerStates[i].PSM = this;

		ChangeState(playerStates[0].Name);
    }

	public override void _Process(float delta)
	{
		base._Process(delta);

		float calculatedEnergy = Mathf.Log( 2.25f * Aecarium.Percentage + 1f);
		aecarialLight.Energy = calculatedEnergy > .75f ? .75f : calculatedEnergy;
	}

	public void _on_AecariumDecay_timeout()
	{
		Aecarium.Value -= 1;
		aecariumDecayTimer.Start();
	}
	public void _on_StaminaRegen_timeout()
	{
		if (StaminaDelayTimer.IsStopped())
			Stamina.Value += 1;

		staminaRegenTimer.Start();
	}
	public void _on_HealthRegen_timeout() 
	{
		Heal(1);
		healthRegenTimer.Start();
	}

	public override void Hurt(int value)
	{
		ChangeState("Staggered");

		base.Hurt(value);
	}

	public override void Die()
	{
		ChangeState("Dead");
	}
}
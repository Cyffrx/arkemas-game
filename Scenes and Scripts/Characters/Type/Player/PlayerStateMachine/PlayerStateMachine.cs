using Godot;
using System.Linq;
using System.Collections.Generic;

public class PlayerStateMachine : ActorStateMachine
{
    public override void _Ready()
    {
        base._Ready();

        Aecarium = new Attribute("Aecarium", 88, -88, 88);
        Health = new Attribute("Health", 8, 0, 8);
        Stamina = new Attribute("Stamina", 8, -4, 8);

        RunSpeed = 200;
        WalkSpeed = 100;
        DodgeSpeed = 400;

        List<PlayerState> playerStates = this.GetChildren().OfType<PlayerState>().ToList();
		for (int i = 0; i < playerStates.Count; i++) playerStates[i].PSM = this;

		ChangeState(playerStates[0].Name);
    }

	public void _on_AecariumDecay_timeout()
	{
		Aecarium.Value -= 1;

	}
	public void _on_StaminaRegen_timeout()
	{
		Stamina.Value += 1;
	}
	public void _on_HealthRegen_timeout() 
	{
		Heal(1);
	}
}
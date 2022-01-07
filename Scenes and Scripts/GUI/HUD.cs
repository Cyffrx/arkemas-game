using Godot;
using System;
using System.Collections.Generic;

public class HUD : _GUIState
{
	PlayerStateMachine PSM;

	ProgressBar AecariumBar;
	ProgressBar HealthBar;
	ProgressBar StaminaBar;
	
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		Owner.GetNode<Control>("HUD").Visible = true;
		GetTree().Paused = false;

		PSM = Owner.GetNode<PlayerStateMachine>("../Player/StateMachine");

		AecariumBar = Owner.GetNode<ProgressBar>("HUD/VBoxContainer/AecariumBar");
		HealthBar = Owner.GetNode<ProgressBar>("HUD/VBoxContainer/HBoxContainer/HealthBar");
		StaminaBar = Owner.GetNode<ProgressBar>("HUD/VBoxContainer/HBoxContainer/StaminaBar");

		AecariumBar.MaxValue = PSM.Aecarium.Max;
		AecariumBar.MinValue = PSM.Aecarium.Min;

		HealthBar.MaxValue = PSM.Health.Max;
		HealthBar.MinValue = PSM.Health.Min;
		
		StaminaBar.MaxValue = PSM.Stamina.Max;
		StaminaBar.MinValue = PSM.Stamina.Min;
	}

	public override void _Process(float delta)
	{
		base._Process(delta);

		if (Input.IsActionJustPressed("pause")) GSM.ChangeState("PauseMenu");

		AecariumBar.Value = PSM.Aecarium.Value;
		StaminaBar.Value = PSM.Stamina.Value;
		HealthBar.Value = PSM.Health.Value;

	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
		Owner.GetNode<Control>("HUD").Visible = false;
	}
}

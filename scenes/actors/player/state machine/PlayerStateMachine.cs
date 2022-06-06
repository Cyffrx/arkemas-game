using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class PlayerStateMachine : ActorStateMachine
{
	public override void _Ready()
	{
		base._Ready();

		CharacterAttributes["Health"] =  new CharacterAttribute("Health", 100.0f, 0.0f, 100.0f);
		CharacterAttributes["Stamina"] = new CharacterAttribute("Stamina", 100.0f, 0.0f, 100.0f);
		CharacterAttributes["Run Speed"] = new CharacterAttribute("Run Speed", 150.0f, 85.0f, 100.0f);

		CharacterAttributes.Add("Dodge Speed", new CharacterAttribute("Dodge Speed", 550.0f, 125.0f, 125.0f));
		CharacterAttributes.Add("Dodge Stamina Cost", new CharacterAttribute("Dodge Stamina Cost", 25.0f, 5.0f, 15.0f));
		CharacterAttributes.Add("Attack Stamina Cost", new CharacterAttribute("Attack Stamina Cost", 50.0f, 5.0f, 15.0f)); // temporary until weapons are implemented

		List<PlayerState> playerStates = this.GetChildren().OfType<PlayerState>().ToList();
		
		for (int i = 0;  i < playerStates.Count; i++)
			playerStates[i].PSM = this;
		
		ChangeState(playerStates[0].Name);
	}

	// anything that _always_ happens for ths character each frames
	public override void _Process(float delta)
	{
		base._Process(delta);
	}
}

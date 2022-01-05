using Godot;
using System.Linq;
using System.Collections.Generic;

public class PlayerStateMachine : ActorStateMachine
{
    public override void _Ready()
    {
        base._Ready();

        Aecarium = new ActorStateMachine.Stat("Aecarium", 88, -88, 88);
        Health = new ActorStateMachine.Stat("Health", 8, 0, 8);
        Stamina = new ActorStateMachine.Stat("Stamina", 8, -4, 8);

        RunSpeed = 400;
        WalkSpeed = 200;
        DodgeSpeed = 600;

        List<PlayerState> playerStates = this.GetChildren().OfType<PlayerState>().ToList();
		for (int i = 0; i < playerStates.Count; i++) playerStates[i].PSM = this;

		ChangeState(playerStates[0].Name);
    }
}
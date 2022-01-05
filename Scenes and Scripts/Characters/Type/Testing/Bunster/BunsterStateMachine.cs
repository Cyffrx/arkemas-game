using Godot;
using System.Linq;
using System.Collections.Generic;

public class BunsterStateMachine : ActorStateMachine
{
    public override void _Ready()
    {
        base._Ready();

        Health = new ActorStateMachine.Stat("Health", 10, 0, 8);
        Stamina = new ActorStateMachine.Stat("Stamina", 10, -4, 8);

        RunSpeed = 200;
        WalkSpeed = 100;
        
        List<BunsterState> bunsterStates = this.GetChildren().OfType<BunsterState>().ToList();
		for (int i = 0; i < bunsterStates.Count; i++) bunsterStates[i].BSM = this;

		ChangeState(bunsterStates[0].Name);
    }
}

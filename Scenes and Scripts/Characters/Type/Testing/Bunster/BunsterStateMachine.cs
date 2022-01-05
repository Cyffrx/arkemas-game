using Godot;
using System.Linq;
using System.Collections.Generic;

public class BunsterStateMachine : ActorStateMachine
{
	public RayCast2D Target;

	public override void _Ready()
	{
		base._Ready();

		Health = new Attribute("Health", 10, 0, 8);
		Stamina = new Attribute("Stamina", 10, 0, 8);
		Target = Owner.GetNode<RayCast2D>("Target");

		RunSpeed = 100;
		WalkSpeed = 50;
		
		List<BunsterState> bunsterStates = this.GetChildren().OfType<BunsterState>().ToList();
		for (int i = 0; i < bunsterStates.Count; i++) bunsterStates[i].BSM = this;

		ChangeState(bunsterStates[0].Name);
	}

	public override void _Process(float delta)
	{
		base._Process(delta);

		Target.CastTo = kb.ToLocal(GetNode<KinematicBody2D>("../../../../Player").Position);
		if (Target.CastTo.Length() < 1000.0f) ChangeState("BunsterChase");
		else ChangeState("BunsterWander");

		// handle radar?
	}
}
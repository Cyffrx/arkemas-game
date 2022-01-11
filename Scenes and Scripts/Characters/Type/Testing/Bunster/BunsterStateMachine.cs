using Godot;
using System.Linq;
using System.Collections.Generic;

public class BunsterStateMachine : ActorStateMachine
{
	public Area2D Target;
	public AudioStream lungeSound;

	public override void _Ready()
	{
		base._Ready();

		Health = new Attribute("Health", 3, 0, 3);
		Stamina = new Attribute("Stamina", 10, 0, 8);
		// Target = Owner.GetNode<RayCast2D>("Target");

		RunSpeed = 100;
		WalkSpeed = 50;

		lungeSound = ResourceLoader.Load("res://Sounds/bunsterScream/bunsterScream.wav") as AudioStream;

		List<BunsterState> bunsterStates = this.GetChildren().OfType<BunsterState>().ToList();
		for (int i = 0; i < bunsterStates.Count; i++) bunsterStates[i].BSM = this;

		ChangeState(bunsterStates[0].Name);
	}

	public void _on_InteractionZone_area_entered(Area2D area)
	{
		if (area.IsInGroup("aecarialSource"))
		{
			ChangeState("BunsterChase");
			Target = area;
		}
	}

	public void _on_InteractionZone_area_exited(Area2D area)
	{
		if (area == Target) ChangeState("BunsterWander");
	}
}

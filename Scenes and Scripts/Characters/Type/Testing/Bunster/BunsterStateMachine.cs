using Godot;
using System.Linq;
using System.Collections.Generic;

public class BunsterStateMachine : ActorStateMachine
{
	public RoamingArea roamingArea;
	public Area2D Target;
	public AudioStream lungeSound;
	public RayCast2D rayCast2D;
	public Vector2 moveTo;

	public override void _Ready()
	{
		base._Ready();

		Health = new Attribute("Health", 3, 0, 3);
		Stamina = new Attribute("Stamina", 10, 0, 8);
		// Target = Owner.GetNode<RayCast2D>("Target");

		RunSpeed = 100;
		WalkSpeed = 50;
		
		rayCast2D = Owner.GetNode<RayCast2D>("RayCast2D");
		roamingArea = Owner.GetParent() as RoamingArea;
		// moveTo = Owner.GetNode<Position2D>("Position2D");

		lungeSound = ResourceLoader.Load("res://Sounds/bunsterScream/bunsterScream.wav") as AudioStream;

		List<BunsterState> bunsterStates = this.GetChildren().OfType<BunsterState>().ToList();
		for (int i = 0; i < bunsterStates.Count; i++) bunsterStates[i].BSM = this;

		ChangeState(bunsterStates[0].Name);
	}
}

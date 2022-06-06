using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class MonsterStateMachine : ActorStateMachine
{

	public Area2D DetectionRadius;
	public Area2D AttackRadius;
	public Position2D Target;
	public RayCast2D Sightline;
	public Dictionary<string, Timer> Timers;

	public RandomNumberGenerator RNG;
	public bool PlayerDetected;
	public bool PlayerInAttackRange;

	public Node2D Goal;

	public override void _Ready()
	{
		base._Ready();

		CharacterAttributes["Health"] =  new CharacterAttribute("Health", 200.0f, 0.0f, 200.0f);
		CharacterAttributes["Stamina"] = new CharacterAttribute("Stamina", 100.0f, 0.0f, 100.0f);
		CharacterAttributes["Run Speed"] = new CharacterAttribute("Run Speed", 100.0f, 75.0f, 85.0f);

		DetectionRadius = GetParent().GetNode<Area2D>("Pathfinding/DetectionRadius");
		DetectionRadius = GetParent().GetNode<Area2D>("Pathfinding/AttackRadius");
		Target = GetParent().GetNode<Position2D>("Pathfinding/Target");
		Sightline = GetParent().GetNode<RayCast2D>("Pathfinding/Sightline");

		RNG = new RandomNumberGenerator();
		
		Timers = new Dictionary<string, Timer>();
		foreach (Timer t in GetParent().GetNode("Timers").GetChildren())
			Timers.Add(t.Name, t);


		// TIFO that this state setting below needs to happen last, especially if we're building dynamic subnode trees
		List<MonsterState> monsterStates = this.GetChildren().OfType<MonsterState>().ToList();
		
		for (int i = 0;  i < monsterStates.Count; i++)
			monsterStates[i].MSM = this;
		
		ChangeState(monsterStates[0].Name);

		
	}

	public override void Die()
	{
		base.Die();
		this.QueueFree();
	}

	private void _on_DetectionRadius_body_entered(object body)
	{
		Node2D node = body as Node2D;

		if (node.IsInGroup("player"))
		{
			Goal = node;
			PlayerDetected = true;
			ChangeState("MonsterMoving");
		}
	}

	private void _on_DetectionRadius_body_exited(object body)
	{
		Node2D node = body as Node2D;

		if (node.IsInGroup("player"))
		{
			PlayerDetected = false;
			ChangeState("MonsterAttacking");
		}
	}

	private void _on_AttackRadius_body_entered(object body)
	{
		Node2D node = body as Node2D;

		if (node.IsInGroup("player"))
		{
			PlayerInAttackRange = true;
			ChangeState("MonsterAttacking");
		}
	}

	private void _on_AttackRadius_body_exited(object body)
	{
		Node2D node = body as Node2D;

		if (node.IsInGroup("player"))
		{
			PlayerInAttackRange = false;
			ChangeState("MonsterIdling");
		}
	}
}

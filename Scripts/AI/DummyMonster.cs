using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class DummyMonster : KinematicBody2D
{
	private float MoveSpeed = 50;
	private float DetectionRadius = 200;
	Vector2 PlayerPosition;
	Vector2 PlayerLastPosition;
	Vector2 Velocity;

	List<RayCast2D> Radar;
	RayCast2D DirectionToPlayer;
	Random rand = new Random((new System.DateTime().Millisecond));	// isn't being random

	public enum State
	{
		SEARCH,
		CHASE
	}

	public State _state;

	private int counter = 0;
	
	public override void _Ready()
	{
		base._Ready();

		Radar = GetNode<Node2D>("Radar").GetChildren().OfType<RayCast2D>().ToList();
		DirectionToPlayer = GetNode<RayCast2D>("DirectionToPlayer");
		PlayerLastPosition = Position;
		_state = State.SEARCH;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess (float delta)
	{
		PlayerPosition = GetNode<KinematicBody2D>("/root/TestLevel/Player").Position;
		DirectionToPlayer.CastTo = ToLocal(PlayerPosition);

		if (DirectionToPlayer.IsColliding())
		{
			Node2D node2d = (Node2D) DirectionToPlayer.GetCollider();
			if (node2d.IsInGroup("player"))
			{
				PlayerLastPosition = node2d.Position;
				_state = State.CHASE;
			}
			else if ((PlayerLastPosition - Position).Length() < 10) _state = State.SEARCH;
		}

		switch (_state)
		{
			case State.CHASE:
				Velocity = (PlayerLastPosition - this.GlobalPosition).Normalized();

				for (int i = 0; i < Radar.Count; i++)
				{
					Radar[i].Rotate(.25f);
					// if (Radar[i].IsColliding())
					// 	velocity -= Radar[i].CastTo;
				}

				MoveAndSlide(MoveSpeed * Velocity);

				break;
			default:
				if (counter++ > 60)
				{
					Velocity = Vector2.Up.Rotated((float) rand.NextDouble());
					counter = 0;
				}
				MoveAndSlide(MoveSpeed * Velocity);
			break;
		}
	}
}

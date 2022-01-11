using Godot;
using System;

public class RoamingArea : Area2D
{
	[Export] private float _radius;
	private CircleShape2D _area;
	private RandomNumberGenerator rng = new RandomNumberGenerator();
	private Position2D _position2d;

	public override void _Ready()
	{
		base._Ready();
		_area = (CircleShape2D) GetNode<CollisionShape2D>("CollisionShape2D").Shape;
		_position2d = GetNode<Position2D>("Position2D");
		rng.Randomize();
		_area.Radius = _radius;
	}

    public Vector2 GeneratePosition2D()
	{
		return ToGlobal(RotateRandom(Vector2.Right * _area.Radius * rng.RandfRange(-1.0f, 1.0f) ));
	}

	private Vector2 RotateRandom(Vector2 vec)
	{
		return vec.Rotated(rng.RandfRange(-2.0f, 2.0f));
	}
}

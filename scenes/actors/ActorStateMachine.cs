using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class ActorStateMachine : StateMachine
{
	public Dictionary<string, CharacterAttribute> CharacterAttributes;

	public Vector2 Velocity;
	public KinematicBody2D Body;
	public Sprite Sprite;
	public AnimationPlayer AnimPlayer;


	public override void _Ready()
	{
		base._Ready();

		CharacterAttributes = new Dictionary<string, CharacterAttribute>();

		CharacterAttributes.Add("Health", new CharacterAttribute("Health", 100.0f, 0.0f, 100.0f));
		CharacterAttributes.Add("Stamina", new CharacterAttribute("Stamina", 100.0f, 0.0f, 100.0f));
		CharacterAttributes.Add("Run Speed", new CharacterAttribute("Run Speed", 120.0f, 80.0f, 100.0f));

		Velocity = Vector2.Right;
		Body = GetParent() as KinematicBody2D;
		Sprite = GetParent().GetNode("Sprite") as Sprite;
		AnimPlayer = GetParent().GetNode("AnimationPlayer") as AnimationPlayer;
	}

	public virtual void Hurt(float value)
	{
		GD.Print(this.Name + " was hurt for " + value + " damage.");

		CharacterAttributes["Health"].Value -=  value;
		if (CharacterAttributes["Helath"].Value == CharacterAttributes["Health"].Min)
			Die();
	}

	public virtual void Die()
	{
		GD.Print(this.Name + " died. ");
	}
}

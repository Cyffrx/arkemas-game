using Godot;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ActionStateMachine : _DefaultStateMachine
{
	public AnimationPlayer animPlayer;
    public Node playerStats;
    public KinematicBody2D kb;
    public Sprite sprite;
	
    public override void _Ready()
    {
        base._Ready();
        
		animPlayer = Owner.GetNode<AnimationPlayer>("AnimationPlayer");
        playerStats =  Owner.GetNode<Node>("Stats");
        sprite = Owner.GetNode<Sprite>("Sprite");
        kb = (KinematicBody2D) Owner;

        List<ActionState> actionStates = this.GetChildren().OfType<ActionState>().ToList();

        for (int i = 0; i < actionStates.Count; i++) actionStates[i].ASM = this;

        ChangeState(actionStates[0].Name);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if ( (int) playerStats.Get("Aecarium") == 0) {playerStats.Call("Die"); }
    }

	public void CheckFlipSprite(Vector2 facing)
	{
		sprite.FlipH = facing.x < 0;
		if (sprite.FlipH)
        {
            sprite.Position = new Vector2(-56,0);
            Owner.GetNode<CollisionShape2D>("DamageArea/CollisionShape2D").Position = new Vector2(-64,-2);
        }
		else
        {
            sprite.Position = new Vector2(0,0);
            Owner.GetNode<CollisionShape2D>("DamageArea/CollisionShape2D").Position = new Vector2(58,-2);
        }
	}

}

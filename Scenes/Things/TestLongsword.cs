using Godot;
using System;

public class TestLongsword : Area2D
{
    public void _on_TestLongsword_body_entered(PhysicsBody2D body)
    {
        if (body.IsInGroup("player"))
        {
            PlayerInformation.Items.Add(Name);
            QueueFree();
        }
    }
}
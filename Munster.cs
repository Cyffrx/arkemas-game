using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class Munster : KinematicBody2D
{
    [Export] public float WanderSpeed;
    [Export] public float PursuitSpeed;
    public Vector2 Velocity;
}

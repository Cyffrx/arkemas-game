using Godot;
using System;

public class TheWarren : Node2D
{
    public override void _Ready()
    {
        CanvasModulate canvas = GetChild<CanvasModulate>(0);
        canvas.Visible = true;
    }
}

using Godot;
using System;

public class Adventurer : KinematicBody2D
{

    DialogueBox dialogueBox;

    public override void _Ready()
    {
        dialogueBox = GetNode("DialougeBox") as DialogueBox;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public void _on_InteractionZone_input_event(Viewport view, InputEvent inputEvent, int shape_idx)
    {   
        GD.Print("input detected!");
        if (inputEvent.IsActionPressed("action_interact")
            && !dialogueBox.Active)
            dialogueBox.StartDialogue(@"C:\Users\souls\Documents\GitHub\arkemas-game\Scenes and Scripts\Testing\Data\dialogue\testing\old test.json");
    }
}

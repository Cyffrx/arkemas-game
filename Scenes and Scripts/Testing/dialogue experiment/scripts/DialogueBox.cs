using Godot;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;


// the dialoguebox should display the npc's text above them
// the player's responses will be displayed below the NPC
public class DialogueBox : Control
{
    private class DialogueEntry
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Next { get; set; }
        public List<DialogueEntry> Responses {get; set;}
        public Dictionary<string, string> PlayerKnowledgeStateChanges {get; set;}
    }

    public string DialogueFilePath = @"C:\Users\souls\Documents\GitHub\arkemas-game\Scenes and Scripts\Testing\Data\dialogue\testing\test.json";
    Dictionary<string, DialogueEntry> DialogueTree;
    
    // reveals NPC name when mouse hovers over them
    Label NamePlate;
    
    // contains the active dialogue entry
    DialogueEntry ActiveText;
    Label DialoguePlate;
    
    // revealed when a dialogue entry contains a list of responses
    VBoxContainer ResponseContainer;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        //debug 
        //string path = @"C:\Users\souls\Documents\GitHub\arkemas-game\Scenes and Scripts\Testing\Data\dialogue\testing\test.json";
        
        NamePlate = GetNode<Label>("VBox/NPCInformation/Nameplate");
        DialoguePlate = GetNode<Label>("VBox/NPCInformation/Dialogue");
        ResponseContainer = GetNode<VBoxContainer>("VBox/ResponseContainer");
        
        // if (File.Exists(path)) // want to check for this at some point
        using (StreamReader reader = new StreamReader(DialogueFilePath))
        {
            string json = reader.ReadToEnd();
            DialogueTree = JsonConvert.DeserializeObject<Dictionary<string, DialogueEntry>>(json);
        }

        NamePlate.Modulate = new Color (0,0,0,0);

        SetDisplayedDialogue();
    }

    // public because externale vents might need to call this?
    public void SetDisplayedDialogue(string key = "00000000")
    {
        // refuse dialogue refresh if npc is prompting player input
        if (key == "mc") return;

        foreach( Node n in ResponseContainer.GetChildren())
            n.QueueFree();

        // end obviously ends the conversation
        if (key == "end")
        {
            _on_Exit_pressed();
            return;
        }

        ActiveText = DialogueTree[key];
        
        NamePlate.Text = ActiveText.Name;
        DialoguePlate.Text = ActiveText.Text;

        for (int i = 0; i < ActiveText.Responses.Count; i++)
        {
            Label label = new Label();

            label.Text = ActiveText.Responses[i].Text;
            label.MouseFilter = MouseFilterEnum.Stop;

            label.Connect("mouse_entered", this, "_response_option_entered", new Godot.Collections.Array(new object[] {label}));
            label.Connect("mouse_exited", this, "_response_option_exited", new Godot.Collections.Array(new object[] {label}));
            label.Connect("gui_input", this, "_response_option_activated", new Godot.Collections.Array(new object[] {label}));

            label.MouseFilter = MouseFilterEnum.Stop;

            ResponseContainer.AddChild(label);
        }
    }

    public void _response_option_entered(Label label)
    {
        label.Modulate = new Color(1, 0, 0);
    }
    public void _response_option_exited(Label label)
    {
        label.Modulate = new Color(1, 1, 1);
    }
    public void _response_option_activated(InputEvent inputEvent, Label label)
    {
        // if not right click / interact
        if (!inputEvent.IsActionPressed("action_interact") ) return;
        
        Godot.Collections.Array responseLabels = ResponseContainer.GetChildren();

        // i'd prefer to clean this up somehow
        for (int i = 0; i < responseLabels.Count; i++)
        {
            Label checkLabel = responseLabels[i] as Label;

            if (checkLabel == label)
            {
                GD.Print($"Dialogue option {i+1} of {ActiveText.Responses.Count} pressed [{label.Text}]\nProceeding with key [{ActiveText.Responses[i].Next}]");
                SetDisplayedDialogue(ActiveText.Responses[i].Next);
                return;
            }
        }
    }

    public void _on_Next_pressed()
    {
        SetDisplayedDialogue(ActiveText.Next);
    }

    public void _on_Exit_pressed()
    {
        this.Modulate = new Color (0, 0, 0, 0);
    }

    public void _on_MarginContainer_mouse_entered()
    {
        NamePlate.Modulate = new Color (1,1,1,1);
    }

    public void _on_MarginContainer_mouse_exited()
    {
        NamePlate.Modulate = new Color (0,0,0,0);
    }
}

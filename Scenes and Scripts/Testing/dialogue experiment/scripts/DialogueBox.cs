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
    }

    Dictionary<string, DialogueEntry> dialogs;
    private Label Nameplate;
    private List<Label> DisplayedText;
    private VBoxContainer DisplayedTextContainer;

    private DialogueEntry ActiveText;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        this.MouseFilter = MouseFilterEnum.Ignore;

        //debug 
        string path = @"C:\Users\souls\Documents\GitHub\arkemas-game\Scenes and Scripts\Testing\Data\dialogue\testing\test.json";
        
        Nameplate = GetNode<Label>("Panel/MarginContainer/VBoxContainer/SpeakerName");
        DisplayedTextContainer = GetNode<VBoxContainer>("Panel/MarginContainer/VBoxContainer/HBoxContainer/TextContainer");
        
        DisplayedText = new List<Label>();
        
        // if (File.Exists(path)) // want to check for this at some point
        using (StreamReader reader = new StreamReader(path))
        {
            string json = reader.ReadToEnd();
            dialogs = JsonConvert.DeserializeObject<Dictionary<string, DialogueEntry>>(json);
        }
        
        SetDisplayedDialogue();
    }

    // 00000000 should be the standard greeting dialogue
    void SetDisplayedDialogue(string key = "00000000")
    {
        GD.Print("Displaying dialogue key " + key);

        Nameplate.Text = "";
        DisplayedText.Clear();

        // end obviously ends the conversation
        if (key == "end")
        {
            _on_Exit_pressed();
            return;
        }

        // mc means that the prior dialogue prompts a player response
        if (key == "mc")
        {
            
            Nameplate.Text = ActiveText.Responses[0].Name;
            // these dialogues also needs events added to them
            for (int i = 0; i < ActiveText.Responses.Count; i++)
            {
                Label label = new Label
                {
                    Text = ActiveText.Responses[i].Text,
                    SizeFlagsHorizontal = (int) SizeFlags.ShrinkCenter,   
                };

                label.Connect("mouse_entered", this, "_response_option_entered", new Godot.Collections.Array(new object[] {label}));
                label.Connect("mouse_exited", this, "_response_option_exited", new Godot.Collections.Array(new object[] {label}));
                label.Connect("gui_input", this, "_response_option_clicked", new Godot.Collections.Array(new object[] {label}));
                
                label.MouseFilter = MouseFilterEnum.Stop;

                DisplayedText.Add(label);
            }
        }
        // if it's not an mc or end, then display dialogue as normal
        else
        {
           ActiveText = dialogs[key];
        
            Nameplate.Text = ActiveText.Name;
            
            Label label = new Label
            {
                    Text = ActiveText.Text,
                    SizeFlagsHorizontal = (int) SizeFlags.ShrinkCenter,
            };

            DisplayedText.Add(label);
        }

        foreach( Node n in DisplayedTextContainer.GetChildren())
            n.QueueFree();

        for (int i = 0; i < DisplayedText.Count; i++)
        {
            DisplayedTextContainer.AddChild(DisplayedText[i]);
        }
    }

    public void _response_option_entered(Label label)
    {
        GD.Print("entered dialogue option");
        label.Modulate = new Color(1, 0, 0);
    }
    public void _response_option_exited(Label label)
    {
        GD.Print("exited dialogue option");
        label.Modulate = new Color(1, 1, 1);
    }
    public void _response_option_clicked(InputEvent inputEvent, Label label)
    {

        if (!inputEvent.IsActionPressed("action_interact") ) return;
        GD.Print("clicked response option");
        SetDisplayedDialogue(ActiveText.Responses[DisplayedText.IndexOf(label)].Next);        
    }

    public void _on_Next_pressed()
    {
        GD.Print("Fetch next dialogue.");
        SetDisplayedDialogue(ActiveText.Next);
    }

    public void _on_Exit_pressed()
    {
        GD.Print("Closing Dialogue.");
        QueueFree();
    }
}

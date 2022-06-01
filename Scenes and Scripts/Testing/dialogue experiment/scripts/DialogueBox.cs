using Godot;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;


// the dialoguebox should display the npc's text above them
// the player's responses will be displayed below the NPC
public class DialogueBox : Control
{
    public class DialogueEntry
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Next { get; set; }
        public List<DialogueEntry> Responses {get; set;}
        public Dictionary<string, string> PlayerKnowledgeStateChanges {get; set;}
    }

    public class DialogueContainer
    {
        // load the conversation contained within the provided filepath
        public void LoadNewConversation(string filepath)
        {
            this.DialogueFilePath = filepath;

            // if file does not exist, return
            if (!System.IO.File.Exists(filepath)) return;
            
            // attempt to read new file
            using (StreamReader reader = new StreamReader(DialogueFilePath))
            {
                string json = reader.ReadToEnd();
                Dialogue = JsonConvert.DeserializeObject<Dictionary<string, DialogueEntry>>(json);
            }
        }

        // goes to the next sequential dialouge
        public DialogueEntry GetDialogue(string key = "00000000")
        {
            return Dialogue[key];
        }

        // location of dialogue file
        private string DialogueFilePath = @"C:\Users\souls\Documents\GitHub\arkemas-game\Scenes and Scripts\Testing\Data\dialogue\testing\test.json";

        // entries read from dialogue file
        private Dictionary<string, DialogueEntry> Dialogue;
    }
    
    #region vars
    DialogueContainer dialogueContainer;
    DialogueEntry de;
    
    // shows NPC name when mouse hovers over them
    Label NamePlate;

    // displays the current dialogue text
    Label DialoguePlate;
    
    // displays player responses, if any
    VBoxContainer ResponseDisplay;

    public bool Active;
    #endregion
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        NamePlate = GetNode<Label>("VBox/NPCInformation/Nameplate");
        NamePlate.Hide();

        DialoguePlate = GetNode<Label>("VBox/NPCInformation/Dialogue");
        ResponseDisplay = GetNode<VBoxContainer>("VBox/ResponseContainer");

        dialogueContainer = new DialogueContainer();
        Active = false;

        this.Hide();
    }

    public void StartDialogue(string filename)
    {
        dialogueContainer.LoadNewConversation(filename);

        Active = true;        
        NextDialogue();

        this.Show();
    }

    public void NextDialogue(string key = "00000000")
    {
        if (key == "end")
        {
            EndDialogue();
            return;
        }

        foreach (Node n in ResponseDisplay.GetChildren())
            n.QueueFree();

        if (key == null) key = de.Next;
        de = dialogueContainer.GetDialogue(key);

        this.NamePlate.Text = de.Name;
        this.DialoguePlate.Text = de.Text;

        // in the event there's no dialogues, add a placeholder that'll continue
        // using the next key of the current de
        if (de.Responses.Count == 0)
        {
            DialogueEntry e = new DialogueEntry
            {
                Next = de.Next,
                Text = "..."
            };

            AddResponse(e);
        }

        for (int i = 0; i < de.Responses.Count; i++)
            AddResponse(de.Responses[i]);

        GD.Print("dialogue set!");
    }

    public void AddResponse(DialogueEntry e)
    {
        Label label = new Label();

        label.Text = e.Text;
        label.MouseFilter = MouseFilterEnum.Stop;

        label.Connect("mouse_entered", this, "_response_option_entered", new Godot.Collections.Array(new object[] {label}));
        label.Connect("mouse_exited", this, "_response_option_exited", new Godot.Collections.Array(new object[] {label}));
        label.Connect("gui_input", this, "_response_option_activated", new Godot.Collections.Array(new object[] {label}));

        label.MouseFilter = MouseFilterEnum.Stop;

        ResponseDisplay.AddChild(label);
    }

    public void EndDialogue()
    {
        Active = false;
        this.Hide();
    }

    
    /*
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
    */

    #region events

        #region dialogue events
        
        // highlight / unhighlight hovered text
        public void _response_option_entered(Label label) { label.Modulate = new Color(1, 0, 0); }
        public void _response_option_exited(Label label) { label.Modulate = new Color(1, 1, 1); }

        // triggers on player selecting a dialogue option
        public void _response_option_activated(InputEvent inputEvent, Label label)
        {
            // if not right click / interact
            if (!inputEvent.IsActionPressed("action_interact") ) return;
            
            // player selected response
            Godot.Collections.Array children = ResponseDisplay.GetChildren();
            
            for (int i = 0; i < children.Count; i++)
            {
                Label lab = children[i] as Label;
                if (label == lab)
                {
                    // ah so because no file-set response it breaks
                    if (de.Responses.Count > 0)
                    {
                        DialogueEntry d = de.Responses[i];
                        NextDialogue(de.Responses[i].Next); // if actual respons
                    }
                    else
                    {
                        NextDialogue(de.Next);
                    }                    

                    break;
                }
            }
        }
        #endregion

        #region hover identify
    public void _on_MarginContainer_mouse_entered()
    {
        //NamePlate.Show();
    }

    public void _on_MarginContainer_mouse_exited()
    {
        //NamePlate.Hide();
    }
    #endregion

    #endregion
}
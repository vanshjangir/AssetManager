using Godot;
using System;

public partial class HomePanel : Panel
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_itch_button_pressed()
	{
		GD.Print("ButtonPressed");
		//var yamateSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		//yamateSound.Stream = GD.Load<AudioStream>("res://addons/AssetManager/yamate.wav");
		//yamateSound.Play();
	}
	
	
}







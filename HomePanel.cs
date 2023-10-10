#if TOOLS
using Godot;
using System;
using System.Collections.Generic;
[Tool]
public partial class HomePanel : Panel
{
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Button itch = GetNode<Button>("PanelContainer/VBoxContainer/HBoxContainer/itch_button");
		var itchCallable = new Callable(this, nameof(onItchButtonPressed));
		itch.Connect("pressed", itchCallable);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void onItchButtonPressed()
	{
//		var yamateSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
//		yamateSound.Stream = GD.Load<AudioStream>("res://addons/AssetManager/yamate.wav");
//		yamateSound.Play((float)0.03);
		GD.Print("button pressed!");
		Assets asset = new Assets();
		List<Dictionary<string, string>> assetList = asset.Load();
		
		for(int i = 0; i < assetList.Count; i++){
			GD.Print(assetList[i]);
		}
	}
}
#endif

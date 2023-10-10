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
		var yamateSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		yamateSound.Stream = GD.Load<AudioStream>("res://addons/AssetManager/button.wav");
		yamateSound.Play((float)0.03);
		Assets assetsScraper = new Assets();
		List<Dictionary<string, string>> assetList = assetsScraper.Load();
		foreach (var assetData in assetList)
	{
		string imageUrl = assetData["image"];
		string imageText = assetData["text"];
		string imageLink = assetData["link"];
		GD.Print($"Image URL: {imageUrl}");
		GD.Print($"Asset Text: {imageText}");
		GD.Print($"Asset Link: {imageLink}");
		GD.Print();
	}

	}
}
#endif

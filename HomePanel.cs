#if TOOLS
using Godot;
using System;
using System.Net;
using System.Collections.Generic;

[Tool]
public partial class HomePanel : Panel
{
	
	public override void _Ready()
	{
		Button itch = GetNode<Button>("PanelContainer/VBoxContainer/HBoxContainer/itch_button");
		var itchCallable = new Callable(this, nameof(onItchButtonPressed));
		itch.Connect("pressed", itchCallable);
	}
	
	public void imageLoad(string imageUrl, string localPath){
		using (WebClient webClient = new WebClient())
		{
			try
			{
				webClient.DownloadFile(imageUrl, localPath);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error downloading image: {ex.Message}");
			}
		}
	}
	
	private void onItchButtonPressed()
	{
		var yamateSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		yamateSound.Stream = GD.Load<AudioStream>("res://addons/AssetManager/button.wav");
		yamateSound.Play((float)0.03);
		
		Assets assetsScraper = new Assets();
		List<Dictionary<string, string>> assetList = assetsScraper.Load();
		int count = 1;
		foreach (var assetData in assetList)
		{
			string localPath = $@"E:\vansh\GodotTest\addons\AssetManager\tmp\image{count}.png";
			string imageUrl = assetData["image"];
			string imageText = assetData["text"];
			string imageLink = assetData["link"];
			GD.Print($"Image URL: {imageUrl}");
			GD.Print($"Asset Text: {imageText}");
			GD.Print($"Asset Link: {imageLink}");
			GD.Print();
			imageLoad(imageUrl, localPath);
			count++;
		}
	}
}
#endif

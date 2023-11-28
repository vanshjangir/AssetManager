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
	
	public Image imageLoad(string imageUrl, string localPath){
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
		
		Image image = new Image();
		try
		{
			image.Load(localPath);
		}
		catch (Exception ex)
		{
			GD.Print($"Error loading image texture: {ex.Message}");
		}
		return image;
	}
	
	public void createTexture(Image assetImage, int count)
	{
		ImageTexture texture = ImageTexture.CreateFromImage(assetImage);
		TextureRect textureRect = GetNode<TextureRect>($"PanelContainer2/VBoxContainer/Row{count/3}/textureRect{count%3}");
		if(textureRect == null){
			GD.Print("textureRect is null");
		}else{
			textureRect.Texture = texture;
			
		}
		
	}
	
	private void onItchButtonPressed()
	{
		var yamateSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		yamateSound.Stream = GD.Load<AudioStream>("res://addons/AssetManager/button.wav");
		yamateSound.Play((float)0.03);
		
		Assets assetsScraper = new Assets();
		List<Dictionary<string, string>> assetList = assetsScraper.Load();
		int count = 0;
		foreach (var assetData in assetList)
		{
			string localPath = $@"D:\Godot\Godot-python\addons\AssetManager\tmp\image{count}.png";
			string imageUrl = assetData["image"];
			string imageText = assetData["text"];
			string imageLink = assetData["link"];
			GD.Print($"Image URL: {imageUrl}");
			GD.Print($"Asset Text: {imageText}");
			GD.Print($"Asset Link: {imageLink}");
			GD.Print();
			Image assetImage = imageLoad(imageUrl, localPath);
			createTexture(assetImage, count);
			count++;
		}
	}
}
#endif

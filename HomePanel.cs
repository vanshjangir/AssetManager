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
	
	
	
	private void onItchButtonPressed()
	{
		var yamateSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		yamateSound.Stream = GD.Load<AudioStream>("res://addons/AssetManager/button.wav");
		yamateSound.Play((float)0.03);
		
		Assets assetsScraper = new Assets();
		List<Dictionary<string, string>> assetList = assetsScraper.Load();
		int count = 1;
		
		VBoxContainer container = GetNode<VBoxContainer>("$PanelContainer2/ScrollContainer/VBoxContainer");
		
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
			Image image = imageLoad(imageUrl, localPath);
			count++;
			
			// Create a new TextureButton instance
			TextureButton textureButton = container.GetNode<TextureButton>("$PanelContainer2/ScrollContainer/VBoxContainer/TextureButton");

			// Set the texture to the downloaded image
//			ImageTexture texture = new ImageTexture();
			var texture = ImageTexture.CreateFromImage(image);
			textureButton.TextureNormal = texture;

			// Set other properties of the TextureButton if needed
//			textureButton.RectMinSize = new Vector2(100, 100); // Adjust as needed

			// Connect the button press signal to a method if needed
			textureButton.Connect("pressed", new Callable(this, "_on_texture_button_pressed"));
//			textureButton.HintTooltip = imageText;
			
			// Add the TextureButton to the container
//			container.AddChild(textureButton);
		}
	}
	
	private void _on_texture_button_pressed()
	{
		GD.Print("lmao pressed a buttton !!");
	}
}
#endif




#if TOOLS
using Godot;
using System;
using System.Net;
using System.Collections.Generic;
using System.IO;

[Tool]
public partial class HomePanel : Panel
{
	Assets assetsScraper = new Assets();
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
	
	public void createTexture(Image assetImage, String imageText, String assetLink, int count)
	{
		ImageTexture texture = ImageTexture.CreateFromImage(assetImage);
		TextureRect textureRect = GetNode<TextureRect>($"PanelContainer2/ScrollContainer/HFlow/Element{count}/textureRect");
		Button button = GetNode<Button>($"PanelContainer2/ScrollContainer/HFlow/Element{count}/Button");
		
		if(textureRect == null){
			GD.Print("textureRect is null");
		}else{
			textureRect.Texture = texture;
			button.Text = imageText;
			button.Pressed += () => onDownloadButtonPressed(assetLink);
		}
		
	}
	
	private void onItchButtonPressed()
	{
		var yamateSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		yamateSound.Stream = GD.Load<AudioStream>("res://addons/AssetManager/button.wav");
		yamateSound.Play((float)0.03);
		
		List<Dictionary<string, string>> assetList = assetsScraper.Load();
		int count = 0;
		foreach (var assetData in assetList)
		{
			string localPath  = Path.Combine(Directory.GetCurrentDirectory(),"addons\\AssetManager\\tmp\\image{count}.png");
			
			//string localPath = $@"C:\Users\devan\Desktop\DesktopFiles\godot\addons\AssetManager\tmp\image{count}.png";
			
			string imageUrl = assetData["image"];
			string imageText = assetData["text"];
			string assetLink = assetData["link"];
			GD.Print($"Image URL: {imageUrl}");
			GD.Print($"Asset Text: {imageText}");
			GD.Print($"Asset Link: {assetLink}");
			//GD.Print($"JSON FILE:{dirPath}");
			GD.Print();
			Image assetImage = imageLoad(imageUrl, localPath);
			createTexture(assetImage, imageText, assetLink, count);
			count++;
		}
	}
	
	private void onDownloadButtonPressed(String assetLink){
		GD.Print("download button pressed");
		assetsScraper.download(assetLink);
		GD.Print("function ended!");
	}
}
#endif

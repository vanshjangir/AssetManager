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
	OptionButton dropDown;
	LineEdit searchBar;
	String url;
	String[] tags = {"Select tag","3d","2d","audio","backgrounds","cute","fighting","fantasy","retro","sprites","tileset",};
	
	public override void _Ready()
	{
		url = "https://itch.io/game-assets/free";
		
		Button itch = GetNode<Button>("PanelContainer/VBoxContainer/HBoxContainer/Node2D/itch_button");
		itch.Pressed += () => onItchPressed();
		
		
		searchBar = GetNode<LineEdit>("PanelContainer/VBoxContainer/Node2D2/LineEdit");
//		searchBar.Clear();
		String query = searchBar.Text;
		searchBar.TextSubmitted += (query) =>  onTextSubmitted(query);
		
		dropDown = GetNode<OptionButton>("PanelContainer/VBoxContainer/Node2D/OptionButton");
		var id = dropDown.GetSelectedId();
		dropDown.ItemSelected += (id) => onItemSelected(id);
		addDropItems();
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
	
	private void LoadItch(String given_url)
	{
		var yamateSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		yamateSound.Stream = GD.Load<AudioStream>("res://addons/AssetManager/button.wav");
		yamateSound.Play((float)0.03);
		
		List<Dictionary<string, string>> assetList = assetsScraper.Load(given_url);
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
		GD.Print("Asset Downloaded!");
	}
	private void addDropItems(){
		dropDown.Clear();
		foreach(var item in tags)
		{
			dropDown.AddItem(item);
		}
	}
	
	private void onItchPressed(){
		searchBar.Clear();
		dropDown.Select(0);
		LoadItch("https://itch.io/game-assets/free");
	}
	
	private void onItemSelected(long id){
		GD.Print("selected: ",tags[id]);
		searchBar.Clear();
		url = "https://itch.io/game-assets/free";
		if(id!=0){
			String tag="/tag-" + tags[id];
			url = url + tag;
		}
		LoadItch(url);
	}
	
	private void onTextSubmitted(String query){
		dropDown.Select(0);
		GD.Print("Searching tags: ",query);
		url = "https://itch.io/game-assets/free";
		String tag="/tag-" + query;
		url = url + tag;
		LoadItch(url);
	}
}
#endif

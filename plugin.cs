#if TOOLS
using Godot;
using System;

[Tool]
public partial class plugin : EditorPlugin
{
	Button button;
	Container container;
	Control AssetWindow;
	
	TextureRect assetPreview;
	public void ahhh()
	{
		assetPreview = new TextureRect();
		assetPreview.Size = new Vector2(200,200);
		assetPreview.Visible = false;
		//rest for python integration
	}
	
	private void onButtonPressed(){
		
		if (AssetWindow != null){
			AssetWindow.Visible = ! AssetWindow.Visible;
			return;
		}
		
		var editorRoot = GetTree().Root;

		AssetWindow = new Panel();
		AssetWindow.SetSize(new Vector2(1000,800));
		AssetWindow.SetPosition(new Vector2(400,150));
		editorRoot.AddChild(AssetWindow);
		string imagePath = "res://.godot/imported/icon.svg-218a8f2b3041327d8a5756f3a245f83b.ctex";
		//assetWindow.AddChild(assetPreview);
		DisplayassetPreview(imagePath);
			
	}
	private void DisplayassetPreview(string path){
		Texture2D texture  = (Texture2D)GD.Load(path);
		assetPreview.Texture = texture;
		assetPreview.Visible = true;
	}
	
	 
	public override void _EnterTree()
	{
		button = new Button();
		container = new Container();
		var editorRoot = GetTree().Root;
		var onButtonPressedCallable = new Callable(this, nameof(onButtonPressed));
		var ahhhCallable = new Callable(this, nameof(ahhh));
		
		button.Text = "OniChan_Yamete_kudasai";
		button.Connect("pressed", onButtonPressedCallable);
		button.Connect("pressed", ahhhCallable);
		container.SetPosition(new Vector2(1080,10));
		container.AddChild(button);
		editorRoot.AddChild(container);
	}

	public override void _ExitTree()
	{
		if(AssetWindow != null){
			AssetWindow.QueueFree();
		}
		if(button != null){
			button.QueueFree();
		}
		if(container != null){
			container.QueueFree();
		}
	
	}
}
#endif

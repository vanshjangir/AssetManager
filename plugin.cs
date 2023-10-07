#if TOOLS
using Godot;
using System;

[Tool]
public partial class plugin : EditorPlugin
{
	Button button;
	Container container;
	PackedScene pkdScene = ResourceLoader.Load<PackedScene>("res://addons/AssetManager/main.tscn");
	
	Panel homePanelIns;

	
	public void ahhh()
	{
		//for python integration
	}
	
	private void onButtonPressed(){
		
		if (homePanelIns != null){
			homePanelIns.Visible = ! homePanelIns.Visible;
			return;	
		}

		var editorRoot = GetTree().Root;
		homePanelIns = (Panel)pkdScene.Instantiate();
		editorRoot.AddChild(homePanelIns);
		
//		AssetWindow = new Panel();
//		Label heading = new Label();
//		heading.Text = "Asset Manager";
//		AssetWindow.AddChild(heading);
//		AssetWindow.SetSize(new Vector2(1000,800));
//		AssetWindow.SetPosition(new Vector2(400,150));
//		editorRoot.AddChild(AssetWindow);
		
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
		if(button != null){
			button.QueueFree();
		}
		if(container != null){
			container.QueueFree();
		}
	
	}
}
#endif

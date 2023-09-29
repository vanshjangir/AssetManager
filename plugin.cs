#if TOOLS
using Godot;
using System;

[Tool]
public partial class plugin : EditorPlugin
{
	Button button;
	Container container;
	Control AssetWindow;
	
	public void ahhh()
	{
		//for python integration
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

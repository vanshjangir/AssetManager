#if TOOLS
using Godot;
using System;

[Tool]
public partial class plugin : EditorPlugin
{
	public void onButtonPressed(){
		var box = new BoxContainer();
		box.SetSize(new Vector2(1000,800));

		var editorRoot = GetTree().Root;
		editorRoot.AddChild(box);
	}
	
	
	public override void _EnterTree()
	{
		var button = new Button();
		var container = new Container();
		var editorRoot = GetTree().Root;
		var onButtonPressedCallable = new Callable(this, nameof(onButtonPressed));
		
		button.Text = "OniChan_Yamete_kudasai";
		button.Connect("pressed", onButtonPressedCallable);
		container.SetPosition(new Vector2(1080,10));
		container.AddChild(button);
		editorRoot.AddChild(container);
	}

	public override void _ExitTree()
	{
		
	}
}
#endif

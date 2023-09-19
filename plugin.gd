@tool
extends EditorPlugin

func DownloadAsset(url):
	# download assets
	var command = "python"
	var arguments = ["loadasset/fetch.py"]
	var result = OS.execute(command,arguments);
	
func loadAsset(scroll):
	# loadassets in the popup window
	pass

func AssetWindow():
	var popup = Popup.new()
	popup.set_size(Vector2(1000,1000))
	var editor_root = get_tree().get_root()
	editor_root.add_child(popup)

	popup.popup_centered()

func _enter_tree():
	var button = Button.new()
	button.set_name("mybutton")
	var container = BoxContainer.new()
	container.set_name("mycontainer")
	var editor_root = get_tree().get_root()

	button.text = "AssManager"
	button.connect("pressed", AssetWindow)
	container.set_position(Vector2(1070,10))
	container.add_child(button)
	editor_root.add_child(container)

func _exit_tree():
	var editor_root = get_tree().get_root()
	var container = editor_root.get_node("mycontainer") 
	var button = editor_root.get_node("mybutton") 
	if container != null:
		container.queue_free()
	if button != null:
		button.queue_free()

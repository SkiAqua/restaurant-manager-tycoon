extends Node

const DEBUG_RESOLUTION = Vector2(640, 1080)
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	DisplayServer.window_set_size(DEBUG_RESOLUTION)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

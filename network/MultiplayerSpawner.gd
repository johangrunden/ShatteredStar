extends Node2D

@export var player_scene: PackedScene

func _ready():
	if multiplayer.is_server():
		multiplayer.peer_connected.connect(_on_peer_connected)
		spawn_player(multiplayer.get_unique_id())

func _on_peer_connected(id):
	spawn_player(id)

func spawn_player(id):
	var player = player_scene.instantiate()
	add_child(player)
	player.set_multiplayer_authority(id)
	player.global_position = Vector2(100 + id * 50, 100)

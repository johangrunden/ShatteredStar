extends Node

var is_host := false
var player_scene = preload("res://characters/player/Player.tscn")

func _ready():
	print("NetworkManager ready")
	multiplayer.peer_connected.connect(_on_peer_connected)

func host_game(port: int = 8910):
	var peer = ENetMultiplayerPeer.new()
	peer.create_server(port)
	multiplayer.multiplayer_peer = peer
	is_host = true
	_spawn_player(multiplayer.get_unique_id(), "TANK")

func join_game(ip: String, port: int = 8910):
	var peer = ENetMultiplayerPeer.new()
	peer.create_client(ip, port)
	multiplayer.multiplayer_peer = peer
	is_host = false
	_spawn_player(multiplayer.get_unique_id(), "DPS")

func _on_peer_connected(id):
	if is_host:
		_spawn_player(id, "HEALER")

func _spawn_player(id: int, role: String):
	var player = player_scene.instantiate()
	player.name = str(id)
	player.role = role
	add_child(player)
	player.set_multiplayer_authority(id)

extends Node

var is_host := false

func _ready():
    print("NetworkManager ready")

func host_game(port: int = 8910):
    var peer = ENetMultiplayerPeer.new()
    peer.create_server(port)
    multiplayer.multiplayer_peer = peer
    is_host = true
    print("Hosting on port %s" % port)

func join_game(ip: String, port: int = 8910):
    var peer = ENetMultiplayerPeer.new()
    peer.create_client(ip, port)
    multiplayer.multiplayer_peer = peer
    is_host = false
    print("Joining %s:%s" % [ip, port])

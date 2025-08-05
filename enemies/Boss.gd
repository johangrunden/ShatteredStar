extends CharacterBody2D

@export var speed := 100.0
@onready var player = null

func _ready():
    await get_tree().create_timer(1.0).timeout
    player = get_tree().get_root().get_node("Main").get_node(str(multiplayer.get_unique_id()))

func _physics_process(_delta):
    if not player:
        return
    var dir = (player.global_position - global_position).normalized()
    velocity = dir * speed
    move_and_slide()

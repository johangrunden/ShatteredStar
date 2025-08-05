extends CharacterBody2D

@export var speed := 200.0
@export var role := "DPS"

@onready var sprite = $Sprite2D

func _ready():
    match role:
        "TANK":
            sprite.modulate = Color.RED
        "HEALER":
            sprite.modulate = Color.GREEN
        "DPS":
            sprite.modulate = Color.BLUE

func _physics_process(_delta):
    if not is_multiplayer_authority():
        return

    var input_vector = Vector2(
        Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left"),
        Input.get_action_strength("ui_down") - Input.get_action_strength("ui_up")
    ).normalized()

    velocity = input_vector * speed
    move_and_slide()

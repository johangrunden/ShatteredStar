extends CharacterBody2D

var target := null
@export var speed := 100.0

func _physics_process(delta):
	if target and is_instance_valid(target):
		var dir = (target.global_position - global_position).normalized()
		velocity = dir * speed
		move_and_slide()

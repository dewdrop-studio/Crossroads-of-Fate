using CrossroadsofFate.globals;
using Godot;
using System;
using System.Linq;

public partial class Player : Area2D
{
	PlayerMovementState movementState;


	[Export]
	public float MovementSpeed = 20.0f;
	public Direction direction;
	public Vector2 ScreenSize;



	public override void _Ready()
	{

		movementState = PlayerMovementState.STOPPED;
		direction = Direction.DOWN;
		ScreenSize = GetViewport().GetVisibleRect().Size;

		AnimatedSprite2D sprite = GetNode<AnimatedSprite2D>("Sprite");
		sprite.Scale = Scale;

		CollisionShape2D collider = GetNode<CollisionShape2D>("Collider");
		collider.Scale = Scale;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{


		Vector2 velocity = Vector2.Zero;
		velocity.X = Godot.Input.GetAxis("move_left", "move_right");
		velocity.Y = Godot.Input.GetAxis("move_up", "move_down");

		AnimatedSprite2D animatedSprite = GetNode<AnimatedSprite2D>("Sprite");


		if (velocity.Length() > 0)
		{
			movementState = PlayerMovementState.WALKING;

			if (velocity.Y != 0)
			{
				direction = (velocity.Y > 0) ? Direction.DOWN : Direction.UP;
			}
			if (velocity.X != 0)
			{
				direction = (velocity.X > 0) ? Direction.RIGHT : Direction.LEFT;
			}


		}
		else
		{
			movementState = PlayerMovementState.STOPPED;
		}





		switch (movementState)
		{
			case PlayerMovementState.STOPPED:
				{

					switch (direction)
					{
						case Direction.UP:
							animatedSprite.Animation = "stand_up";
							break;
						case Direction.DOWN:
							animatedSprite.Animation = "stand_down";
							break;
						case Direction.LEFT:
							animatedSprite.Animation = "stand_left";
							break;
						case Direction.RIGHT:
							animatedSprite.Animation = "stand_right";
							break;

						default:
							break;
					}

					break;
				}

			case PlayerMovementState.WALKING:
				{
					switch (direction){
					case Direction.UP:
						animatedSprite.Animation = "walk_up";
						break;
					case Direction.DOWN:
						animatedSprite.Animation = "walk_down";
						break;
					case Direction.LEFT:
						animatedSprite.Animation = "walk_left";
						break;
					case Direction.RIGHT:
						animatedSprite.Animation = "walk_right";
						break;

					default:
					break;
				}


					break;
				}

			case PlayerMovementState.BINDED:
				{
					
					break;
				}

			default:
				break;
		}

		
		Position += velocity.Normalized() * MovementSpeed * ((float)delta);
		
		animatedSprite.Play();

	}

}

using CrossroadsofFate.globals;
using Godot;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.XPath;

public partial class PlayerOverworld : Area2D
{
	PlayerMovementState movementState;
	
	[Signal]
	public delegate void PositionChangedEventHandler(Vector2 position);


	[Export]
	public int MaxHealth = 100;
	public int health = 50;
	
	[Signal]
	public delegate void HealthChangedEventHandler(int health);



	[Export]
	public int level = 1;
	public int exp = 0;
	[Signal]
	public delegate void ExpChangedEventHandler(int level, int exp);


	[Export]
	public float MovementSpeed = 20.0f;
	public Direction direction;
	public Vector2 ScreenSize;

	

	private Camera2D camera;
	private CollisionShape2D collider;
	private AnimatedSprite2D sprite;



	public override void _Ready()
	{

		movementState = PlayerMovementState.STOPPED;
		direction = Direction.DOWN;
		ScreenSize = GetViewport().GetVisibleRect().Size;

		sprite = GetNode<AnimatedSprite2D>("Sprite");
		sprite.Scale = Scale;

		collider = GetNode<CollisionShape2D>("Collider");
		collider.Scale = Scale;

		camera = GetNode<Camera2D>("Camera2D");
		camera.Position = Position;

		health = MaxHealth;


		EmitSignal(SignalName.HealthChanged, health);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{


		Vector2 velocity = Vector2.Zero;
		velocity.X = Godot.Input.GetAxis("move_left", "move_right");
		velocity.Y = Godot.Input.GetAxis("move_up", "move_down");



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
							sprite.Animation = "stand_up";
							break;
						case Direction.DOWN:
							sprite.Animation = "stand_down";
							break;
						case Direction.LEFT:
							sprite.Animation = "stand_left";
							break;
						case Direction.RIGHT:
							sprite.Animation = "stand_right";
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
						sprite.Animation = "walk_up";
						break;
					case Direction.DOWN:
						sprite.Animation = "walk_down";
						break;
					case Direction.LEFT:
						sprite.Animation = "walk_left";
						break;
					case Direction.RIGHT:
						sprite.Animation = "walk_right";
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
		
		sprite.Play();

		if ( Input.IsActionJustPressed("CreateDamage") ){
			CreateDamage(10);
			GiveExp(100);
			
		}else if ( Input.IsActionJustPressed("HealthReset") ){
			health = MaxHealth;
			EmitSignal(SignalName.HealthChanged, health);
		}

	}

	public void CreateDamage(int value){
		health -= value;

		Godot.GD.Print("Health: " + health);

		if (health <= 0){
			health = 0;	// Game over
		}
		EmitSignal(SignalName.HealthChanged, health);
	}

	public void GiveExp(int value){
		exp += value;
		var required = Functions.CalculateRequiredExp(level);

		while (exp >= required){
			LevelUp();
			required = Functions.CalculateRequiredExp(level);
			Godot.GD.Print("Current EXP: " + exp + " / " + required);
		}
		EmitSignal(SignalName.ExpChanged, level, exp);
	}

	private void LevelUp(){
		level++;
		Godot.GD.Print("Level Up: " + level);
		EmitSignal(SignalName.ExpChanged, level, exp);
	}
}

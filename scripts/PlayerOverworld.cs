using CrossroadsofFate;
using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class PlayerOverworld : CharacterBody2D
{
	State.PlayerMovementState movementState;

	[Signal]
	public delegate void PositionChangedEventHandler(Vector2 position);


	[Export]
	public int MaxHealth = 100;
	[Export]
	public int health = 100;

	[Signal]
	public delegate void HealthChangedEventHandler(int health);



	public int level = 1;
	public int exp = 0;
	[Signal]
	public delegate void ExpChangedEventHandler(int level, int exp);


	[Export]
	public float MovementSpeed = 20.0f;
	[Export]
	public State.Direction direction;
	public Vector2 ScreenSize;



	private Camera2D camera;
	private CollisionShape2D collider;
	private AnimatedSprite2D sprite;



	private IList<Items.InventoryItem> inventory;

	public override void _Ready()
	{

		movementState = State.PlayerMovementState.STOPPED;
		direction = State.Direction.DOWN;
		ScreenSize = GetViewport().GetVisibleRect().Size;

		sprite = GetNode<AnimatedSprite2D>("Sprite");
		sprite.Scale = Scale;

		collider = GetNode<CollisionShape2D>("Collider");
		collider.Scale = Scale;

		camera = GetNode<Camera2D>("/root/World/Camera");
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

		velocity = velocity.Normalized();

		if (velocity.Length() != 0)
		{
			var collisionInfo = MoveAndCollide(velocity * MovementSpeed * ((float)delta));
			if (collisionInfo != null)
			{
				velocity = Vector2.Zero;
			}
			else
			{
			}
			EmitSignal(SignalName.PositionChanged, Position);

		}



		if (velocity.Length() > 0)
		{
			movementState = State.PlayerMovementState.WALKING;

			if (velocity.Y != 0)
			{
				direction = (velocity.Y > 0) ? State.Direction.DOWN : State.Direction.UP;
			}
			if (velocity.X != 0)
			{
				direction = (velocity.X > 0) ? State.Direction.RIGHT : State.Direction.LEFT;
			}


		}
		else
		{
			movementState = State.PlayerMovementState.STOPPED;
		}





		switch (movementState)
		{
			case State.PlayerMovementState.STOPPED:
				{

					switch (direction)
					{
						case State.Direction.UP:
							sprite.Animation = "stand_up";
							break;
						case State.Direction.DOWN:
							sprite.Animation = "stand_down";
							break;
						case State.Direction.LEFT:
							sprite.Animation = "stand_left";
							break;
						case State.Direction.RIGHT:
							sprite.Animation = "stand_right";
							break;

						default:
							break;
					}

					break;
				}

			case State.PlayerMovementState.WALKING:
				{
					switch (direction)
					{
						case State.Direction.UP:
							sprite.Animation = "walk_up";
							break;
						case State.Direction.DOWN:
							sprite.Animation = "walk_down";
							break;
						case State.Direction.LEFT:
							sprite.Animation = "walk_left";
							break;
						case State.Direction.RIGHT:
							sprite.Animation = "walk_right";
							break;

						default:
							break;
					}


					break;
				}

			case State.PlayerMovementState.BINDED:
				{

					break;
				}

			default:
				break;
		}


		sprite.Play();

		if (Input.IsActionJustPressed("CreateDamage"))
		{
			CreateDamage(10);
			GiveExp(100);

		}
		else if (Input.IsActionJustPressed("HealthReset"))
		{
			health = MaxHealth;
			EmitSignal(SignalName.HealthChanged, health);
		}

	}

	public void CreateDamage(int value)
	{
		health -= value;

		Godot.GD.Print("Health: " + health);

		if (health <= 0)
		{
			health = 0; // Game over
		}
		EmitSignal(SignalName.HealthChanged, health);
	}

	public void GiveExp(int value)
	{
		if (level >= Leveling.MAX_LEVEL)
		{
			return;
		}

		exp += value;
		var required = Leveling.CalculateRequiredExp(level);

		while (exp >= required)
		{
			LevelUp();
			required = Leveling.CalculateRequiredExp(level);
			Godot.GD.Print("Current EXP: " + exp + " / " + required);
		}
		EmitSignal(SignalName.ExpChanged, level, exp);
	}

	private void LevelUp()
	{
		level++;
		Godot.GD.Print("Level Up: " + level);
		EmitSignal(SignalName.ExpChanged, level, exp);
	}

	public void AddItem(int id, int quantity)
	{
		Items.InventoryItem item = null;

		try
		{
			item = Items.GetItem(id);
		}
		catch (Exception e)
		{
			Godot.GD.PrintErr("Error: " + e.Message);
			return;
		}


		if (item.stackable)
		{
			foreach (Items.InventoryItem i in inventory)
			{
				if (i.id == id)
				{
					i.quantity += quantity;
					return;
				}
			}
		}

		item.quantity = quantity;
		inventory.Add(item);
	}
}

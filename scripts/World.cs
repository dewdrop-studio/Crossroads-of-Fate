using Godot;
using System;

public partial class World : Node2D
{

	private Camera2D camera;
	private Control UIcontainer;

	private float time = 0.0f;


	public override void _Ready()
	{
		camera = GetNode<Camera2D>("Camera");
		UIcontainer = GetNode<Control>("Camera/WorldUi");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


}

using Godot;
using System;

public partial class HPBar : TextureProgressBar
{
	
	private PlayerOverworld player;


	private TextureProgressBar healthBar;

	public override void _Ready()
	{
		player = GetNode<PlayerOverworld>("/root/World/Player");
		
		
		Godot.GD.Print("Player Health: " + player.health);
		
		MaxValue = player.MaxHealth;
		Value = player.health;



		Godot.GD.Print("Binding Signal to Player Health Change\n");
		player.HealthChanged += OnHealthChange;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void OnHealthChange(int health){

		MaxValue = player.MaxHealth;
		Value = player.health;

		Godot.GD.Print("Updating Health\n" + "Current Health: " + player.health + "\nMax Health: " + player.MaxHealth);
	}
	
}

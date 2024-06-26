using Godot;
using System;

public partial class EXPBar : TextureProgressBar
{
	
	private PlayerOverworld player;

	public override void _Ready()
	{
		player = GetNode<PlayerOverworld>("/root/World/Player");
			
		
		Godot.GD.Print("Player E: " + player.health);
		
		MaxValue = player.MaxHealth;
		Value = player.health;



		Godot.GD.Print("Binding Signal to Player Health Change\n");
		player.ExpChanged += OnEXPChange;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		

	}

	public void OnEXPChange(int level, int exp){
		
		MaxValue = CrossroadsofFate.globals.Functions.CalculateRequiredExp(level);
		Value = exp;
	}
	
}

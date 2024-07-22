using CrossroadsofFate;
using Godot;
using System;

public partial class ExpBar : TextureProgressBar
{

	public override void _Ready()
	{
		PlayerOverworld player = GetNode<PlayerOverworld>("/root/World/Player");
			
		
		Godot.GD.Print("Player E: " + player.health);
		
		MinValue = Leveling.CalculateRequiredExp(player.level - 1);
		MaxValue = Leveling.CalculateRequiredExp(player.level);
		Value = player.exp;

		Godot.GD.Print("Binding Signal to Player Health Change\n");
		player.ExpChanged += OnEXPChange;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		

	}

	public void OnEXPChange(int level, int exp){
		MinValue = Leveling.CalculateRequiredExp(level - 1);
		MaxValue = Leveling.CalculateRequiredExp(level);
		Value = exp;
	}
	
}

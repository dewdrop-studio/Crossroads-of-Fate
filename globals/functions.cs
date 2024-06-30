using Godot;
using System;
using System.Diagnostics.Tracing;

namespace CrossroadsofFate.globals
{
	public class Functions
	{
		public static int CalculateRequiredExp(int level)
		{
			int power = 4;
			int multiplier = 20;

			int exp = (int)Math.Floor(Math.Pow(level, power)) + (level * multiplier);
			return exp;
		}
	}
	
}
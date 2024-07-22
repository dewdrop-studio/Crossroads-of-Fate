using Godot;
using System;
using System.Diagnostics.Tracing;

namespace CrossroadsofFate
{
	public class Leveling
	{
		public const int MAX_LEVEL = 100;

		public static int CalculateRequiredExp(int level)
		{
			if (level <= 0)
			{
				return 0;
			}

			int power = 4;
			int multiplier = 20;

			int exp = (int)Math.Floor(Math.Pow(level, power)) + (level * multiplier);
			return exp;

		}


		const int MAX_BATTLES = 5;
		const int MIN_BATTLES = 2;
		public static int CalculateGainedExp(int level)
		{

			int required = CalculateRequiredExp(level);
			int random = new Random().Next(MIN_BATTLES, MAX_BATTLES);


			int gained = (int)Math.Floor(required * (random / 100.0));
			return gained;

		}
	}

	public class Vector2Extensions
	{
		public static Vector2 Clamp(Vector2 vec, float min, float max)
		{
			vec.X = Mathf.Clamp(vec.X, min, max);
			vec.Y = Mathf.Clamp(vec.Y, min, max);
			return vec;
		}

	}

}
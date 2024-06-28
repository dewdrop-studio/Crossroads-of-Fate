using Godot;
using System;
using System.Diagnostics.Tracing;

namespace CrossroadsofFate.globals
{
	public class Function{

		public int CalculateRequiredExp(int level){
			int power = 2;
			int multiplier = 20;
			if(level == 1){
				return 0;
			}
			int exp = (int)Math.Pow(level,power) + (level*multiplier);
			return exp;
		}
	}
}
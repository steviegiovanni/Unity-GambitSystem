using System;

namespace GameSystems.LevelSystem{
	/// <summary>
	/// RPG exp gain event arguments.
	/// </summary>
	public class RPGExpGainEventArgs : EventArgs {
		/// <summary>
		/// Gets the exp gained.
		/// </summary>
		/// <value>The exp gained.</value>
		public int ExpGained{ get; private set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="RPGExpGainEventArgs"/> class.
		/// </summary>
		/// <param name="expGained">Exp gained.</param>
		public RPGExpGainEventArgs (int expGained){
			ExpGained = expGained;
		}
	}
}

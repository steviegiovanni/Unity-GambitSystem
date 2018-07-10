using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// RPG entity level.
/// </summary>
public abstract class RPGEntityLevel : MonoBehaviour {
	/// <summary>
	/// The level.
	/// </summary>
	[SerializeField]
	private int _level = 1;

	/// <summary>
	/// The level minimum.
	/// </summary>
	[SerializeField]
	private int _levelMin = 1;

	/// <summary>
	/// The level max.
	/// </summary>
	[SerializeField]
	private int _levelMax = 100;

	/// <summary>
	/// The exp current.
	/// </summary>
	private int _expCurrent = 0;

	/// <summary>
	/// The exp required.
	/// </summary>
	private int _expRequired = 0;

	/// <summary>
	/// Occurs when on entity exp gain.
	/// </summary>
	public event EventHandler<RPGExpGainEventArgs> OnEntityExpGain;

	/// <summary>
	/// Occurs when on entity level change.
	/// </summary>
	public event EventHandler<RPGLevelChangeEventArgs> OnEntityLevelChange;

	/// <summary>
	/// Occurs when on entity level up.
	/// </summary>
	public event EventHandler<RPGLevelChangeEventArgs> OnEntityLevelUp;

	/// <summary>
	/// Occurs when on entity level down.
	/// </summary>
	public event EventHandler<RPGLevelChangeEventArgs> OnEntityLevelDown;

	/// <summary>
	/// Gets or sets the level.
	/// </summary>
	/// <value>The level.</value>
	public int Level{
		get { return _level;}
		set{ _level = value;}
	}

	/// <summary>
	/// Gets or sets the level minimum.
	/// </summary>
	/// <value>The level minimum.</value>
	public int LevelMin{
		get { return _levelMin;}
		set{ _levelMin = value;}
	}

	/// <summary>
	/// Gets or sets the level max.
	/// </summary>
	/// <value>The level max.</value>
	public int LevelMax{
		get { return _levelMax;}
		set{ _levelMax = value;}
	}

	/// <summary>
	/// Gets or sets the exp current.
	/// </summary>
	/// <value>The exp current.</value>
	public int ExpCurrent{
		get { return _expCurrent;}
		set{ _expCurrent = value;}
	}

	/// <summary>
	/// Gets or sets the exp required.
	/// </summary>
	/// <value>The exp required.</value>
	public int ExpRequired{
		get{ return _expRequired;}
		set{ _expRequired = value;}
	}

	/// <summary>
	/// Gets the exp required for level.
	/// </summary>
	/// <returns>The exp required for level.</returns>
	/// <param name="level">Level.</param>
	public abstract int GetExpRequiredForLevel (int level);

	/// <summary>
	/// Awake this instance.
	/// </summary>
	private void Awake(){
		ExpRequired = GetExpRequiredForLevel (Level);
	}

	/// <summary>
	/// Modifies the exp.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void ModifyExp(int amount){
		ExpCurrent += amount;

		if (OnEntityExpGain != null) {
			OnEntityExpGain (this, new RPGExpGainEventArgs (amount));
		}

		CheckCurrentExp ();
	}

	/// <summary>
	/// Sets the current exp.
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetCurrentExp(int value){
		int expGained = value - ExpCurrent;

		ExpCurrent = value;

		if (OnEntityExpGain != null) {
			OnEntityExpGain (this, new RPGExpGainEventArgs (expGained));
		}

		CheckCurrentExp ();
	}

	/// <summary>
	/// Checks the current exp.
	/// </summary>
	public void CheckCurrentExp(){
		int oldLevel = Level;

		InternalCheckCurrentExp ();

		if (oldLevel != Level && OnEntityLevelChange != null)
			OnEntityLevelChange (this, new RPGLevelChangeEventArgs (_level, oldLevel));
	}

	/// <summary>
	/// Internals the check current exp.
	/// </summary>
	private void InternalCheckCurrentExp(){
		while (true) {
			if (ExpCurrent > ExpRequired) {
				ExpCurrent -= ExpRequired;
				InternalIncreaseCurrentLevel ();
			} else if (ExpCurrent < 0) {
				ExpCurrent += GetExpRequiredForLevel (Level - 1);
				InternalDecreaseCurrentLevel ();
			} else {
				break;
			}
		}	
	}

	/// <summary>
	/// Increases the current level.
	/// </summary>
	public void IncreaseCurrentLevel(){
		int oldLevel = Level;
		InternalIncreaseCurrentLevel ();
		if ((oldLevel != Level) && (OnEntityLevelChange != null)) {
			OnEntityLevelChange (this, new RPGLevelChangeEventArgs (Level, oldLevel));
		}
	}

	/// <summary>
	/// Internals the increase current level.
	/// </summary>
	private void InternalIncreaseCurrentLevel(){
		int oldLevel = Level++;

		if (Level > LevelMax) {
			Level = LevelMax;
			ExpCurrent = GetExpRequiredForLevel (Level);
		}

		ExpRequired = GetExpRequiredForLevel (Level);
		if ((oldLevel != Level) &&(OnEntityLevelUp != null)) {
			OnEntityLevelUp (this, new RPGLevelChangeEventArgs (Level, oldLevel));
		}
	}

	/// <summary>
	/// Decreases the current level.
	/// </summary>
	public void DecreaseCurrentLevel(){
		int oldLevel = Level;
		InternalDecreaseCurrentLevel ();
		InternalIncreaseCurrentLevel ();
		if ((oldLevel != Level) && (OnEntityLevelChange != null)) {
			OnEntityLevelChange (this, new RPGLevelChangeEventArgs (Level, oldLevel));
		}
	}

	/// <summary>
	/// Internals the decrease current level.
	/// </summary>
	private void InternalDecreaseCurrentLevel(){
		int oldLevel = Level--;
		if (Level < LevelMin) {
			Level = LevelMin;
			ExpCurrent = 0;
		}


		ExpRequired = GetExpRequiredForLevel (Level);
		if ((oldLevel != Level) &&(OnEntityLevelDown != null)) {
			OnEntityLevelDown (this, new RPGLevelChangeEventArgs (Level, oldLevel));
		}
	}

	/// <summary>
	/// Sets the level.
	/// </summary>
	/// <param name="targetLevel">Target level.</param>
	public void SetLevel(int targetLevel){
		SetLevel (targetLevel, true);
	}

	/// <summary>
	/// Sets the level.
	/// </summary>
	/// <param name="targetLevel">Target level.</param>
	/// <param name="clearExp">If set to <c>true</c> clear exp.</param>
	public void SetLevel(int targetLevel, bool clearExp){
		int oldLevel = Level;
		Level = targetLevel;
		ExpRequired = GetExpRequiredForLevel (Level);

		if (clearExp) {
			SetCurrentExp (0);
		} else {
			InternalCheckCurrentExp ();
		}

		InternalIncreaseCurrentLevel ();
		if ((oldLevel != Level) && (OnEntityLevelChange != null)) {
			OnEntityLevelChange (this, new RPGLevelChangeEventArgs (Level, oldLevel));
		}
	}
}

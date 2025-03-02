﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxThreeOfAKind : ScoreCardBox
{

	/// <summary>
	/// When this box is created it initializes variables
	/// </summary>
	void Start()
	{
		Initialize();
	}

	/// <summary>
	/// This box can be filled in by the user
	/// </summary>
	/// <returns>This box can be filled in by the user</returns>
	protected override bool ShouldBoxBeFilledIn()
	{
		return true;
	}

	/// <summary>
	/// This uses the yahtzee scoring class to tell the points in this box's category
	/// </summary>
	/// <returns>Points in the three of a kind category based on the dice</returns>
	public override int GetPoints()
	{
		return YahtzeeScoring.ThreeOfAKind(diceNumbers[0], diceNumbers[1], diceNumbers[2], diceNumbers[3], diceNumbers[4]);
	}

	/// <summary>
	/// This updates the information in the box and checks to see if the mouse was clicked or if the enter key was pressed and reacts appropriately
	/// </summary>
	void Update()
	{
		UpdateInformation();
		CheckForEnterKey();
		CheckForMouseClick();
	}
}

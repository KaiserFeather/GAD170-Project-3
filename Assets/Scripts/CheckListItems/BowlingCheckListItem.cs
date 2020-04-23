using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingCheckListItem : CheckListItem
{
    public int Score { get; set; }
    public int MaxScore { get; set; }
    public override bool IsComplete { get { return Score == MaxScore; } }
    //these state what will be present on the scoreboard
    public override float GetProgress()
    {
        return (float)Score / (float)MaxScore; //this information is retrieved from the BowlingLandBehaviour script
    }

    public override string GetStatusReadout()
    {
        return Score.ToString() + " / " + MaxScore.ToString(); //this prints the current score
    }

    public override string GetTaskReadout()
    {
        return "Total bowling tally";
    }

    //this will change the colour of the score on the scoreboard depending on how
    //many pins are knocked over
    //if the player knocks them all over, a strikethrough is included on the score
    public void OnBowlingScored()
    {
        {
            var ourData = new GameEvents.CheckListItemChangedData();
            ourData.item = this;
            ourData.previousItemProgress = GetProgress();

            GameEvents.InvokeCheckListItemChanged(ourData);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingCheckListItem : CheckListItem
{
    public int Score { get; set; }
    public int MaxScore { get; set; }
    public override bool IsComplete { get { return Score == MaxScore; } }

    public override float GetProgress()
    {
        return (float)Score / (float)MaxScore;
    }

    public override string GetStatusReadout()
    {
        return Score.ToString() + " / " + MaxScore.ToString();
    }

    public override string GetTaskReadout()
    {
        return "Total bowling tally";
    }

    public void OnBowlingScored()
    {
        //if (numberOfHoopsScored < numberOfRequiredHoops)
        {
            var ourData = new GameEvents.CheckListItemChangedData();
            ourData.item = this;
            ourData.previousItemProgress = GetProgress();

            //numberOfHoopsScored++;

            GameEvents.InvokeCheckListItemChanged(ourData);
        }
    }
}

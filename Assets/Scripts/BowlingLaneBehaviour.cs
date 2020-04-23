using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple bowling lane logic, is triggered externally by buttons that are routed
/// to the InitialiseRound, TalleyScore and ResetRack.
/// 
/// Future work;
///   Use the timer in update to limit how long a player has to bowl,
///   Detect that the player/ball is 'bowled' from behind the line
/// </summary>
public class BowlingLaneBehaviour : MonoBehaviour
{
    public GameObject pinPrefab;
    public GameObject bowlingBall;
    public Transform[] pinSpawnLocations;
    public Transform defaultBallLocation;
    int score;
    //Creating a list of all the pins so they can be individually checked for different functions
    public List<GameObject> pins = new List<GameObject>();
    BowlingCheckListItem bowlingCheckListItem;

    void Start()
    {
        //this is used for putting results on the scoreboard
        bowlingCheckListItem = FindObjectOfType<BowlingCheckListItem>();
        bowlingCheckListItem.MaxScore = pinSpawnLocations.Length; //the maximum score is the number of pin spawn locations
    }

    [ContextMenu("InitialiseRound")]
    public void InitialiseRound()
    {   
        foreach (Transform pinLoc in pinSpawnLocations) //for each of the pins, it finds a suitable location to spawn
        {
            GameObject newPin = Instantiate(pinPrefab, pinLoc.position, pinLoc.rotation); //spawns the pin in a location
            pins.Add(newPin);
        }
        
    }

    //this puts the bowling ball back into its spawn location if it rolls off the end of the land
    public void BallReachedEnd()
    {
        bowlingBall.transform.position = defaultBallLocation.transform.position; //sets the position it should be in
        bowlingBall.transform.rotation = defaultBallLocation.transform.rotation; //sets the roatation it should have
        bowlingBall.GetComponent<Rigidbody>().velocity = Vector3.zero; //stops movement
        bowlingBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; //stops movement
    }
    [ContextMenu("TalleyScore")]
    //this will figure out which pins are knocked over and use that to determine the score
    public void TalleyScore()
    {
        int score = 0;
        for (int i = 0; i < pins.Count; i++)
        {
            float angle = Vector3.Dot(Vector3.up, pins[i].transform.up);
            if (angle <= 0.9f)//if a pin falls past this angle, it counts as one point
            {
                score++;
            }
        }
        print(score);
        bowlingCheckListItem.Score = score; //used for adding score to scoreboard
        bowlingCheckListItem.OnBowlingScored();
    }

    [ContextMenu("ResetRack")]
    public void ResetRack()
    {
        for (int i = 0; i < pins.Count; i++)
        {
            //this sets all of the pins back where they should be with the correct position and rotation
            pins[i].transform.position = pinSpawnLocations[i].transform.position;
            pins[i].transform.rotation = pinSpawnLocations[i].transform.rotation;
            bowlingBall.transform.position = defaultBallLocation.transform.position;
            bowlingBall.transform.rotation = defaultBallLocation.transform.rotation;
            //this makes the pin stop moving so it doesn't fall again
            bowlingBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bowlingBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            bowlingCheckListItem.Score = 0;
            bowlingCheckListItem.OnBowlingScored();

        }
    }

    protected void Update()
    {
        
    }
}

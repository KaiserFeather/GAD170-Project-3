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
    //TODO; we need a way of tracking the pins that are used for scoring and so we can clean them up
    public List<GameObject> pins = new List<GameObject>();
    BowlingCheckListItem bowlingCheckListItem;

    void Start()
    {
        bowlingCheckListItem = FindObjectOfType<BowlingCheckListItem>();
        bowlingCheckListItem.MaxScore = pinSpawnLocations.Length;
    }

    [ContextMenu("InitialiseRound")]
    public void InitialiseRound()
    {
        //TODO; need to move or init or create pins for a round of bowling, most likely to include some of the following;
        
        foreach (Transform pinLoc in pinSpawnLocations) //for each of the pins, it finds a suitable location to spawn
        {
            GameObject newPin = Instantiate(pinPrefab, pinLoc.position, pinLoc.rotation); //spawns the pin in a location
            pins.Add(newPin);
        }
        
    }

    public void BallReachedEnd()
    {
        //TODO; this needs to return the ball to the ball feed so the player could bowl again or at least clean ups
        bowlingBall.transform.position = defaultBallLocation.transform.position;
        bowlingBall.transform.rotation = defaultBallLocation.transform.rotation;
        bowlingBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bowlingBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    [ContextMenu("TalleyScore")]
    public void TalleyScore()
    {
        //TODO; determine score and get that information out to a checklist item, either via event or directly
        int score = 0;
        for (int i = 0; i < pins.Count; i++)
        {
            float angle = Vector3.Dot(Vector3.up, pins[i].transform.up);
            if (angle <= 0.9f)
            {
                score++;
            }
        }
        print(score);
        bowlingCheckListItem.Score = score;
        bowlingCheckListItem.OnBowlingScored();
    }

    [ContextMenu("ResetRack")]
    public void ResetRack()
    {
        //TODO; clean up all objects created by the bowling lane, preparing for a new round of bowling to occur
        for (int i = 0; i < pins.Count; i++)
        {
            pins[i].transform.position = pinSpawnLocations[i].transform.position;
            pins[i].transform.rotation = pinSpawnLocations[i].transform.rotation;
            bowlingBall.transform.position = defaultBallLocation.transform.position;
            bowlingBall.transform.rotation = defaultBallLocation.transform.rotation;
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

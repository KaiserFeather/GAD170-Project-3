using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item that when used changes to the next song, when out of songs turns off, when used while off, plays first song.
/// 
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BoomBoxItem : InteractiveItem
{
    //Retrieves the music from the provided list
    protected AudioSource audioSource;
    [SerializeField] AudioClip[] radiomusic;
    //this prevents the first song being skipped
    int song = -1;

    bool isUsed = false;
    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>(); //Sets the list to use for the songs
        audioSource.clip = radiomusic[0];
    }
    
    public void PlayClip()
    {
        audioSource.clip = radiomusic[song];
        audioSource.Play();
    }

    void Update()
    {
        //This allows the player to skip songs
        if (!audioSource.isPlaying && isUsed)
        {
            song++;

            if (song > radiomusic.Length - 1)
                song = 0;


            PlayClip();
        }
    }

    public override void OnUse()
    {
        base.OnUse();
        isUsed = true;

        if (song < radiomusic.Length - 1)
        {
            //This auto plays the next song
            song++;
            PlayClip();
        }
        else
        {
            //This stops the music when it reaches the end of the playlist
            audioSource.Stop();
            song = 0;
            isUsed = false;
        }
    }
}

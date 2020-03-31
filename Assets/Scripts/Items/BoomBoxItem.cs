using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item that when used changes to the next song, when out of songs turns off, when used while off, plays first song.
/// 
/// TODO; It should auto play, randomise order potentially and go to next track when used.
///     In other words, act kind of like the radio in a GTA style game.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BoomBoxItem : InteractiveItem
{
    //TODO: you will need more data than this, like clips to play and a way to know which clip is playing
    protected AudioSource audioSource;
    [SerializeField] AudioClip[] radiomusic;
    int song = -1;

    bool isUsed = false;
    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = radiomusic[0];
    }

    public void PlayClip()
    {
        audioSource.clip = radiomusic[song];
        audioSource.Play();
    }

    void Update()
    {
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
            song++;
            PlayClip();
        }
        else
        {
            audioSource.Stop();
            song = 0;
            isUsed = false;
        }
    }
}

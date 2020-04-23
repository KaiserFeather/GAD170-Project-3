using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item that when used while held acts as a physics based projectile instantiator
/// </summary>
public class NerfGunItem : InteractiveItem
{
    public GameObject nerfDartPrefab;
    public Transform nerfDartSpawnLocation;
    public float fireRate = 1; //this sets how often the gun can fire
    public float launchForce = 10; //this sets how strong the force of the shot is
    protected float fireRateCounter;

    AudioSource audioSource;
    float timer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); //this sets the sound of the gunshot
    }
    protected void Update()
    {
        timer += Time.deltaTime; //used to stop the player from firing too often
    }

    public override void OnUse()
    {
        base.OnUse();
        FireNow();
    }

    public void FireNow()
    {
        if (timer >= fireRate) //ensures that the player can fire
        {
            audioSource.Play(); //plays the gunshot
            GameObject dart = Instantiate(nerfDartPrefab, nerfDartSpawnLocation.position, Quaternion.identity); //sets how the shot will spawn
            Rigidbody rb = dart.GetComponent<Rigidbody>(); //spawns the bullet
            rb.AddForce(transform.forward * launchForce); //fires the bullet
            timer = 0; //sets the timer so that the player has to wait before firing again
        }
    }
}

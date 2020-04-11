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
    public float fireRate = 1;
    public float launchForce = 10;
    protected float fireRateCounter;

    AudioSource audioSource;
    float timer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    protected void Update()
    {
        timer += Time.deltaTime;
    }

    public override void OnUse()
    {
        base.OnUse();
        FireNow();
    }

    public void FireNow()
    {
        if (timer >= fireRate)
        {
            audioSource.Play();
            GameObject dart = Instantiate(nerfDartPrefab, nerfDartSpawnLocation.position, Quaternion.identity);
            Rigidbody rb = dart.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * launchForce);
            timer = 0;
        }
    }
}

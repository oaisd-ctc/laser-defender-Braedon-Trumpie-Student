using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    [Header("AI")]
    public bool useAI;
    [SerializeField] float BotFireRateVariance;

    bool isFiring;
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    
    public void SetIsFiring(bool b)
    {
        isFiring = b;
    }

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(
                projectilePrefab, 
                transform.position, 
                Quaternion.identity);
            if(instance.GetComponent<DamageDealer>() != null)
                instance.GetComponent<DamageDealer>().damage = GetComponent<DamageDealer>().projectileDamage;
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = useAI ?
                    -transform.up * projectileSpeed : 
                    transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(!useAI ?
                1f/firingRate : 1f/Mathf.Clamp(
                    Random.Range(
                        firingRate - BotFireRateVariance,
                        firingRate + BotFireRateVariance),
                    0, float.MaxValue));
        }
    }
}

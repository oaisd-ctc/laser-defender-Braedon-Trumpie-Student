using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{   [SerializeField] int maxHealth = 50;
    int health = 50;
    [SerializeField] ParticleSystem DeathEffect;
    [SerializeField] int shipDestroyPoints = 100;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;

    void Awake() 
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        PlayerInput playerInput = other.GetComponent<PlayerInput>();
        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            StartCoroutine(HitEffect());
            ShakeCamera();
            if(playerInput == null)
            {
                damageDealer.Hit();
            }
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetHealth()
    {
        return health;
    }
    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            audioPlayer.PlayDamageClip();
            PlayDeathEffect();
            if(GetComponent<Shooter>() != null && GetComponent<Shooter>().useAI)
            {
                scoreKeeper.AddScore(shipDestroyPoints);
                Debug.Log(scoreKeeper.GetScore());
            }
            Destroy(gameObject);
        }
    }
    IEnumerator HitEffect()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    void PlayDeathEffect()
    {
        ParticleSystem instance = Instantiate(DeathEffect,
            transform.position, 
            Quaternion.identity);
        Destroy(instance.gameObject,
            instance.main.duration + instance.main.startLifetime.constantMax);
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}

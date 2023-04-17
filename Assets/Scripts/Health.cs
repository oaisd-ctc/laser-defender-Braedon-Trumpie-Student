using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem DeathEffect;

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        PlayerInput playerInput = other.GetComponent<PlayerInput>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            StartCoroutine(HitEffect());
            if(playerInput == null)
            {
                damageDealer.Hit();
            }
        }
    }
    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject + " has " + health + " health.");
        if(health <= 0)
        {
            PlayDeathEffect();
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
}

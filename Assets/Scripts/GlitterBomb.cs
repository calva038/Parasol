using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitterBomb : MonoBehaviour
{
    public float radius = 2f;
    public float force = 100f;
    public float uplift = 1f;
    public GameObject explosionEffect;
    bool hasExploded = false;
    
    void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Vector2 explosionPosition = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, radius, Physics2D.DefaultRaycastLayers, -Mathf.Infinity, Mathf.Infinity);
        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                AddExplosionForce(rb, force, transform.position, radius, uplift);
            }
            if (nearbyObject.name.Contains("Enemy"))
            {
                Health enemyHealth = nearbyObject.GetComponent<Health>();
                enemyHealth.ReceiveDamage(5);
            }
        }
        Destroy(gameObject);
    }

    void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * explosionForce * wearoff;
        body.AddForce(baseForce);

        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }
}

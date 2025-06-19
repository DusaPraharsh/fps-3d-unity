using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    private float health = 50f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

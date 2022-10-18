using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : EnemyShooterBase
{
    [SerializeField] private int damage = 5;
    [SerializeField] private GameObject hitVFX = null;

    protected override void Start()
    {
        speedMultiplier = 2f;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("BOOOM!");
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
            vfx.layer = gameObject.layer;
            vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
            Destroy(gameObject);
        }
    }
}
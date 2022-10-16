using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected Transform firingOrigin = null;
    [SerializeField] protected GameObject bulletPrefab = null;
    [SerializeField] protected float bulletForce = 20f;
    [SerializeField] protected float fireInterval = 2f;
    [SerializeField] protected float playerProximityLimit = 10f;
    [SerializeField] protected float speedMultiplier = 1f;

    protected Rigidbody2D rb;
    protected PlayerMovement player;

    protected bool seePlayer = false;
    protected float fireTimer;
    protected bool shooting = false;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        fireTimer = fireInterval;
    }

    protected virtual void Update()
    {
        if (player == null)
        {
            return;
        }

        CheckPlayerProximity();
        FacePlayerIfSeen();
        FollowPLayerIfSeen();
        FirePlayerIfSeen();
    }

    private void CheckPlayerProximity()
    {
        if (player.gameObject.layer == gameObject.layer)
        {
            // same layer, then check distance
            if (Vector2.Distance(player.gameObject.transform.position, transform.position) <
                playerProximityLimit)
            {
                // player in range
                seePlayer = true;
                shooting = true;
                return;
            }
        }
        seePlayer = false;
        shooting = false;
    }

    private void FacePlayerIfSeen()
    {
        if (seePlayer)
        {
            Debug.Log("See!");
            Vector2 playerPos = player.gameObject.transform.position;
            Vector2 lookDirection = playerPos - rb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }

    protected virtual void FollowPLayerIfSeen() 
    {
        if (seePlayer) {
            var player = FindObjectOfType<PlayerMovement>();
            GetComponentInChildren<EnemyMovement>().MoveTo(player.transform.position, speedMultiplier);
        }
    }

    protected virtual void FirePlayerIfSeen()
    {
        
    }

}

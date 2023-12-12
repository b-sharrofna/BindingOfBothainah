using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class FamiliarScript : MonoBehaviour
{
    private float lastFire;
    private GameObject player;
    public Animator animator;
    Vector2 movement;
    public FamiliarData familiar;
    private float lastOffsetX;
    private float lastOffestY;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootver = Input.GetAxis("ShootVertical");
        if ((shootHor != 0 || shootver != 0) && Time.time > lastFire + familiar.fireDelay)
        {
            Shoot(shootHor, shootver);
            lastFire = Time.time;
        }

        
    }
    void FixedUpdate()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            float offestX = (movement.x < 0) ? Mathf.Floor(movement.x) : Mathf.Ceil(movement.x);
            float offestY = (movement.y < 0) ? Mathf.Floor(movement.y) : Mathf.Ceil(movement.y);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, familiar.speed * Time.fixedDeltaTime);
            lastOffsetX = offestX;
            lastOffestY = offestY;
        }
        else
        {
            if (!(transform.position.x < lastOffsetX + 0.5f) || !(transform.position.y < lastOffestY + 0.5f))
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x - lastOffsetX, player.transform.position.y - lastOffestY), familiar.speed * Time.fixedDeltaTime);
            }
        }
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(familiar.bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        float posX = (x < 0) ? Mathf.Floor(x) * familiar.speed : Mathf.Ceil(x) * familiar.speed;
        float posY = (y < 0) ? Mathf.Floor(y) * familiar.speed : Mathf.Ceil(y) * familiar.speed;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(posX, posY);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    public TMP_Text collectedText;
    public static int collectedAmount = 0;
    public GameObject bulletPrefab;
    public float bulletSpeed = 7.5f;
    private float lastFire;
    public float fireDely;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        fireDely = GameController.FireRate;
        speed = GameController.MoveSpeed;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootver = Input.GetAxis("ShootVertical");
        if ((shootHor != 0 || shootver != 0) && Time.time > lastFire + fireDely)
        {
            Shoot(shootHor, shootver);
            lastFire = Time.time;
        }

        //rb.velocity = new Vector3(hor * speed, ver * speed, 0);

        collectedText.text = "Items collected: " + collectedAmount; 


    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed, 0);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Die,
    Attack
};
public enum EnemyType
{
    Melee,
    Ranged
};
public class EnemyScript : MonoBehaviour
{
    GameObject player;
    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType; 
    public float range;
    public float speed;
    public float bulletSpeed; 
    private bool chooseDir = false;
    private bool dead = false;
    private Vector3 randomeDir;
    public float attackingRnage;
    private bool coolDownAttack = false;
    public bool notInRoom = false; 
    public float coolDown;
    public Animator animator;


    public GameObject bulletPreFab; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case (EnemyState.Idle):
                //Idle();
            break;
            case (EnemyState.Wander):
                Wander();
            break;
            case (EnemyState.Follow):
                Follow();
            break;
            case (EnemyState.Die):
            break;

            case (EnemyState.Attack):
                Attack();
            break;
        }
        if (!notInRoom)
        {

            if (isPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;

            }
            else if (isPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Wander;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackingRnage)
            {
                currState = EnemyState.Attack;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
    }
    private bool isPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
    private IEnumerator chooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomeDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomeDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false; 
    }
    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(chooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if (isPlayerInRange(range))
        {
            currState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType)
            {
                case (EnemyType.Melee):
                    
                    GameController.DamagePlayer(1);
                    StartCoroutine(CoolDown());
                break;

                case(EnemyType.Ranged):
                    animator.SetBool("IsAttacking", true);
                    StartCoroutine(AttackFalse());
                    GameObject bullet = Instantiate(bulletPreFab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletScript>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletScript>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                break;
            }
        }
        
    }
    private IEnumerator CoolDown()
    {
        
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
        animator.SetBool("IsAttacking", false);
    }

    private IEnumerator AttackFalse()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsAttacking", false);
    }
    public void Death()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);
       
    }
}

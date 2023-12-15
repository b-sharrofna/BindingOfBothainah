using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TMP_Text healthText;

    public static GameController instance;

    private static float health = 6;
    private static int maxHealth = 6;
    private static float moveSpeed = 5.0f;
    private static float fireRate = 0.25f;
    private static float bulletSize = 0.5f;
    private static int numOfBullets = 0; 
    private bool bootCollected = false;
    private bool potionCollected = false;
    private bool screwCollected = false;
    private bool eyeCollected = false;

    public List<string> collectedNames = new List<string>(); 


    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }
    public static int Bullets { get => numOfBullets; set => numOfBullets = value; }

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health; 
    }

    public static void DamagePlayer(int damge)
    {
        health -= damge;
        if(health <= 0)
        {
            KillPlayer(); 
        }
    }

    public static void HealPlayer(float healNum)
    {
        health = Mathf.Min(maxHealth, health + healNum);
    }
    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed; 
    }
    public static void FireRateChange(float rate)
    {
        fireRate -= rate;
    }
    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }

    public static void BulletNumChange(int num)
    {
        numOfBullets += num;
    }
    public void UpdateCollectedItems(CollectorScript item)
    {
        collectedNames.Add(item.item.name);

        foreach(string i in collectedNames)
        {
            switch (i)
            {
                case "Boot":
                    bootCollected = true;
                    break;
                case "Screw":
                    screwCollected = true;
                    break;
                case "Potion":
                    potionCollected = true;
                    break;
                case "Eye":
                    eyeCollected = true;
                    break; 

            }
        }
        if(bootCollected && screwCollected)
        {
            FireRateChange(0.25f);
        }
    }
    public static void KillPlayer()
    {
        SceneManager.LoadScene(6);
    }


}

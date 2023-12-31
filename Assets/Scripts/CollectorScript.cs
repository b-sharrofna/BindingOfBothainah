using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite itemSprite; 
}
public class CollectorScript : MonoBehaviour
{
    public Item item;
    public float healthChange;
    public float moveSpeedChange;
    public float attackSpeedChange;
    public float bulletSizeChange;
    public int bulletNum; 


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerScript.collectedAmount++;
            GameController.HealPlayer(healthChange);
            GameController.MoveSpeedChange(moveSpeedChange);
            GameController.FireRateChange(attackSpeedChange);
            GameController.BulletSizeChange(bulletSizeChange);
            GameController.BulletNumChange(bulletNum);
            GameController.instance.UpdateCollectedItems(this);

            Destroy(gameObject);
        }
    }
}

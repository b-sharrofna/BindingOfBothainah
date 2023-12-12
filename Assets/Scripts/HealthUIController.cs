using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public GameObject HeartContainer;
    private float fillValue; 

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)GameController.Health;
        fillValue = fillValue / GameController.MaxHealth;
        HeartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}

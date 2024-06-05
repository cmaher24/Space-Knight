using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public Transform HealthManger;
    public Transform DHolder;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {
            Vector3 newPosition = new Vector3(1, DHolder.position.y, DHolder.position.z);
            DHolder.position = newPosition;
        }
      
        if (HealthManger.position.x == -1)
        {
            Vector3 newPosition = new Vector3(0, HealthManger.position.y, HealthManger.position.z);
            HealthManger.position = newPosition;
            TakeDamage(2);
        }


    }

    public void TakeDamage(float damage) 
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }
    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }
}

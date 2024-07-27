using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class health : MonoBehaviour
{
    public int thisHP;
    
    public UnityEvent onDeath;
    public UnityEvent onDamage;
    
    public void removeHP(int damage)
    {
        onDamage.Invoke();
        thisHP -= damage;
        if (thisHP <= 0)
        {
            die();
        }
    }
    
    public void die()
    {
        onDeath.Invoke();
    }
    
    public void addHP(int healAmount)
    {
        thisHP += healAmount;
    }
}

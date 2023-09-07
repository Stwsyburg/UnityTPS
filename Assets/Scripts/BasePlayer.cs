using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public static int HP=100;
    public bool isAlive()
    {
        return HP > 0;
    }

    public void GetDamage(int dmg)
    {
        HP -= dmg;
        if(!isAlive())
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}

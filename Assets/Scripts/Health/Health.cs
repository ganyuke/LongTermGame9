using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health = 50;
    
    // Only take damage from these types. 
    // Does not prevent `onDamage` from being invoked.
    public DamageType vulnerableDamageTypes;
    
    // Does not invoke after `onDeath`
    public UnityEvent<int, DamageType> onDamage;
    
    public UnityEvent onDeath;

    public void TakeDamage(int damage, DamageType damageType)
    {
        if (vulnerableDamageTypes.HasFlag(damageType))
        {
            health -= damage;   
        }
        
        if (health <= 0)
        {
            onDeath.Invoke();
            return;
        }
        
        onDamage.Invoke(damage, damageType);
    }
    
}
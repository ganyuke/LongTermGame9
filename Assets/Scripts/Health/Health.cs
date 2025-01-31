using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private int value;

    public int Value => value;

    // Only take damage from these types. 
    // Does not prevent `onDamage` from being invoked, only sets damage to zero.
    [SerializeField]
    private DamageType vulnerableDamageTypes;
    
    // Does not invoke after `onDeath`
    public UnityEvent<int, DamageType> onDamage;
    
    public UnityEvent onDeath;
    
    public void ApplyDamage(int damage, DamageType damageType)
    {
        if (!vulnerableDamageTypes.HasFlag(damageType))
        {
            damage = 0;
        }
        
        value -= damage;
        
        if (value <= 0)
        {
            onDeath.Invoke();
            return;
        }
        
        onDamage.Invoke(damage, damageType);
    }
    
}
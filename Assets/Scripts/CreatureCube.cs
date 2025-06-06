using UnityEngine;

public class CreatureCube : Creature
{

    // Basic move front back and spin
    protected override void Move()
    {
        return;
    }

    // Attack in a very basic manner?
    protected override void Attack()
    {
        return;
    }
    
    // Just spawns
    protected override void EnterAnimation()
    {
        Debug.Log("Entering animation cube");
        gameObject.SetActive(true);
    }
}

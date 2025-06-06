using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

//ABSTRACTION//
public abstract class Creature : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //EnterAnimation();
    }

    // Update is called once per frame 
    //INHERITANCE// all creatures can use the attack and move method when the player presses j or space bar
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    // Move in a specific pattern to showcase the movement of the creature
    protected abstract void Move();

    // Show an attack animation
    protected abstract void Attack();

    // Trigger the animation when entering the scene
    protected abstract void EnterAnimation();
}

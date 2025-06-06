using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System.Collections;

//ABSTRACTION//
public abstract class Creature : MonoBehaviour
{
    private bool isMoving = false, isAttacking = false;
    private float waitTime = 1.1f;
    // Update is called once per frame 
    //INHERITANCE// all creatures can use the attack and move method when the player presses j or space bar
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MovingCoroutine());
            Move();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            Attack();
        }
    }

    IEnumerator MovingCoroutine()
    {
        Move();
        yield return new WaitForSeconds(waitTime);
        isMoving = false;
    }

    // Move in a specific pattern to showcase the movement of the creature
    public abstract void Move();

    // Show an attack animation
    public abstract void Attack();

    // Trigger the animation when entering the scene
    public abstract void EnterAnimation();
}

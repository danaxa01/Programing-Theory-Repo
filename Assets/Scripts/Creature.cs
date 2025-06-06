using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System.Collections;

//ABSTRACTION//
public abstract class Creature : MonoBehaviour
{
    public bool isActing = false;
    public float waitTime { get; protected set; } = 2f;

    protected Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame 
    //INHERITANCE// all creatures can use the attack and move method when the player presses j or space bar
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isActing)
        {
            isActing = true;
            StartCoroutine(MovingCoroutine());
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isActing)
        {
            isActing = true;
            StartCoroutine(AttackingCoroutine());
        }
    }

    IEnumerator MovingCoroutine()
    {
        Move();
        yield return new WaitForSeconds(waitTime);
        isActing = false;
    }

    IEnumerator AttackingCoroutine()
    {
        Attack();
        yield return new WaitForSeconds(waitTime);
        isActing = false;
    }

    // Move in a specific pattern to showcase the movement of the creature
    //POLYMORPHISM//
    public abstract void Move();

    // Show an attack animation
    //POLYMORPHISM//
    public abstract void Attack();

    // Trigger the animation when entering the scene
    //POLYMORPHISM//
    public abstract void EnterAnimation();

    //INHERITANCE//
    protected bool IsGrounded()
    {
        // Simple grounded check
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}

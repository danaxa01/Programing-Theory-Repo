using UnityEngine;
using System.Collections;
public class CreatureCube : Creature
{

    private float moveDistance = 2f, basicMoveSpeed = 2f;
    private float jumpForce = 3f, attackJumpForce = 3f;
    private float delayBetweenMoves = 0.3f, pauseAfterDash = 0.2f;

    private Rigidbody rb;

    private void Start()
    {
         rb = GetComponent<Rigidbody>();
    }

    // Basic move front back and spin
    public override void Move()
    {
        StartCoroutine(MovementSequence());
    }

    // Attack in a very basic manner?
    public override void Attack()
    {
        StartCoroutine(AttackSequence());
    }

    // Just spawns
    public override void EnterAnimation()
    {
        return;
    }

    
    private IEnumerator MovementSequence()
    {
        // Move Forward
        yield return MoveInDirection(Vector3.forward, basicMoveSpeed);

        yield return new WaitForSeconds(delayBetweenMoves);

        // Move Backward
        yield return MoveInDirection(Vector3.back, basicMoveSpeed);

        yield return new WaitForSeconds(delayBetweenMoves);

        // Jump
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private IEnumerator AttackSequence()
    {
        // Small jump
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * attackJumpForce, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(0.3f); // Let the jump play out slightly

        // Dash forward
        yield return MoveInDirection(Vector3.forward, basicMoveSpeed * 3);

        // Pause briefly
        yield return new WaitForSeconds(pauseAfterDash);

        // Dash backward
        yield return MoveInDirection(Vector3.back, basicMoveSpeed * 3);
    }
    
    private IEnumerator MoveInDirection(Vector3 direction, float moveSpeed)
    {
        float moved = 0f;

        while (moved < moveDistance)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.Translate(direction * step);
            moved += step;
            yield return null;
        }
    }

    private bool IsGrounded()
    {
        // Simple grounded check
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}

using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class CreatureSphere : Creature
{

    private Vector3 attackTargetOffset = new Vector3(0, 0, 5f);
    private float jumpForce = 3f;
    private float dashSpeed = 15f;
    private float pauseTime = 0.01f;
    private float rollDistance = 3f;
    private float rollSpeed = 20f;

    private Vector3 originalPosition = new Vector3(0, 0.5f, 0);

    //Zoom through the scene in fast speed
    public override void Move()
    {
        StartCoroutine(RollForwardAndBack());
    }

    // Attack the target from different directions
    public override void Attack()
    {
        StartCoroutine(ThreeDirectionalAttack());
    }

    // Roll in a circle and return to the main position.
    public override void EnterAnimation()
    {
        return;
    }

    private IEnumerator RollForwardAndBack()
    {
        Vector3 forwardTarget = originalPosition + transform.forward * rollDistance;

        // Roll forward
        yield return MoveToPosition(forwardTarget);

        yield return new WaitForSeconds(0.2f);

        // Roll back to original
        yield return MoveToPosition(originalPosition);
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, target, rollSpeed * Time.deltaTime);
            rb.MovePosition(newPos);

            yield return null;
        }
    }

    private IEnumerator ThreeDirectionalAttack()
    {
        Vector3[] startOffsets = {
            new Vector3(-3f, 0, -3f),
            new Vector3(3f, 0, -2f),
            new Vector3(0, 0, -4f)
        };

        Vector3 targetPoint = originalPosition + attackTargetOffset;

        for (int i = 0; i < 3; i++)
        {
            // Move to start position i
            transform.position = originalPosition + startOffsets[i];
            rb.linearVelocity = Vector3.zero;

            // Jump
            if (IsGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(0.2f);

            // Dash toward the target point
            yield return DashToPoint(targetPoint);

            yield return new WaitForSeconds(pauseTime);
        }

        // Return to original position
        transform.position = originalPosition;
    }

    private IEnumerator DashToPoint(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        float traveled = 0f;

        Vector3 direction = (target - transform.position).normalized;

        while (traveled < distance)
        {
            float step = dashSpeed * Time.deltaTime;
            transform.Translate(direction * step, Space.World);
            traveled += step;
            yield return null;
        }
    }

}

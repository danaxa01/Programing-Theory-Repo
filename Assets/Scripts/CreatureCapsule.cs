using UnityEngine;
using System.Collections;

public class CreatureCapsule : Creature
{

    // Basically Fly?
    public override void Move()
    {
        StartCoroutine(MoveAnimation());
    }

    // Move up and Dive in to attack the target
    public override void Attack()
    {
        StartCoroutine(AttackAnimation());
    }

    // Fly in the scene
    public override void EnterAnimation()
    {
        StartCoroutine(SpawnAnimation());
    }

    public IEnumerator MoveAnimation()
    {
        Vector3 originalPos = transform.position;

        // Simulate 3 flaps (jump up 3x)
        for (int i = 0; i < 3; i++)
        {
            transform.position += Vector3.up * 1.5f;
            yield return new WaitForSeconds(0.15f);
        }

        // Then spiral down to originalPos
        yield return SpiralToPosition(originalPos, 5f, 3f);  // radius, speed
    }
    private IEnumerator SpiralToPosition(Vector3 target, float radius, float speed)
    {
        float angle = 0f;
        Vector3 center = target;
        float height = transform.position.y;

        while (radius > 0.1f)
        {
            angle += speed * Time.deltaTime;
            radius -= Time.deltaTime;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            height -= Time.deltaTime * 1.5f; // spiral descent

            transform.position = new Vector3(center.x + x, height, center.z + z);
            yield return null;
        }

        transform.position = target;
    }

    public IEnumerator AttackAnimation()
    {
        Vector3 originalPos = transform.position;
        Vector3 forwardTarget = originalPos + transform.forward * 5f;

        // 2 flaps upward
        for (int i = 0; i < 2; i++)
        {
            transform.position += Vector3.up * 1.5f;
            yield return new WaitForSeconds(0.2f);
        }

        // Dash forward
        yield return DashToPoint(forwardTarget, 12f);

        // Small pause
        yield return new WaitForSeconds(0.1f);

        // Dash back
        yield return DashToPoint(originalPos, 12f);

        // Fall to original Y
        float fallSpeed = 6f;
        while (transform.position.y > originalPos.y)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }

    private IEnumerator DashToPoint(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator SpawnAnimation()
    {
        Vector3 targetPos = new Vector3(0f, 0.5f, 0f);
        Quaternion targetRot = Quaternion.Euler(0, 90, -90);

        // Start from high position & rotated (like diving in)
        transform.position = new Vector3(0, 10f, -5f);
        transform.rotation = Quaternion.Euler(90, 0, 0);  // Dive angle

        float duration = 1f;
        float timer = 0f;

        while (timer < duration)
        {
            float t = timer / duration;

            transform.position = Vector3.Lerp(transform.position, targetPos, t);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, t);

            timer += Time.deltaTime;
            yield return null;
        }

        // Final snap
        transform.position = targetPos;
        transform.rotation = targetRot;
    }
}

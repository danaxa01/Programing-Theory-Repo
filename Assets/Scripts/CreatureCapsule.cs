using UnityEngine;

public class CreatureCapsule : Creature
{

    // Basically Fly?
    public override void Move()
    {
        return;
    }

    // Move up and Dive in to attack the target
    public override void Attack()
    {
        return;
    }

    // Fly in the scene
    public override void EnterAnimation()
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
        }

        // Final snap
        transform.position = targetPos;
        transform.rotation = targetRot;
    }
}

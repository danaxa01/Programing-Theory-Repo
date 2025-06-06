using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public abstract class Creature : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected abstract void Move();

    protected abstract void Attack();
}

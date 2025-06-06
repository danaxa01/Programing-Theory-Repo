using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cubeC, sphereC, capsuleC;
    private GameObject currentCreature;
    private bool hasAppeared = false;

    // Update is called once per frame
    void Update()
    {
        AppearObject();
    }

    void AppearObject()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DisappearPrevious();
            currentCreature = cubeC;
            currentCreature.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            DisappearPrevious();
            currentCreature = sphereC;
            currentCreature.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            DisappearPrevious();
            currentCreature = capsuleC;
            currentCreature.gameObject.SetActive(true);
        }
        if (currentCreature != null && !hasAppeared)
        {
            hasAppeared = true;
            currentCreature.GetComponent<Creature>().isActing = true;
            currentCreature.GetComponent<Creature>().EnterAnimation();
            currentCreature.GetComponent<Creature>().isActing = false;
        }
    }

    void DisappearPrevious()
    {
        if (currentCreature != null && hasAppeared)
        {
            currentCreature.gameObject.SetActive(false);
            hasAppeared = false;
        }
    }
}

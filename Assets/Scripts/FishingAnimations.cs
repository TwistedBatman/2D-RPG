using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishingAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;
    float timeToWait;
    bool reeling = false;
    bool caught = false;

    private void Update()
    {
        if (!reeling)
        {
            reeling = true;
            timeToWait = Random.Range(8f, 16f);
            StartCoroutine(WaitFor(timeToWait));

        }
    }

    IEnumerator WaitFor(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        if (caught)
        {
            animator.SetTrigger("Caught");
            animator.SetTrigger("Casting");
            caught = false;
            reeling = false;
            animator.SetBool("Reeling", false);
        }
        else if (reeling)
        {
            animator.SetBool("Reeling", true);
            StartCoroutine(WaitFor(Random.Range(4f, 8f)));
            caught = true;
        }
    }
}

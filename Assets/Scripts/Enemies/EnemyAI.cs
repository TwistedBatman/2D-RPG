using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public float runSpeed;
    public float walkSpeed;
    public Animator animator;
    public bool dead = false;
    public float maxDistance = 2f;
    [SerializeField] GameObject alertMark;

    private bool foundTarget = false;
    //private bool obstacle = false;
    private Vector3 startingPosition;
    private Vector3 newPosition;
    private float startingWalkSpeed;
    public float startingRunSpeed;
    bool findFirstTime = true;
    bool containsParameter;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        newPosition = GetRoamingPosition();
        //animator.Play("Run");
        startingWalkSpeed = walkSpeed;
        startingRunSpeed = runSpeed;
        containsParameter = AnimatorContainsParameter();
        //target = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void FixedUpdate()
    {
        SpriteDirection(newPosition);
        if (!foundTarget) // If the target is not in range roam around the starting position
        {
            if (transform.position != newPosition) // Wait until the enemy reaches the new position
            {
                transform.position = Vector2.MoveTowards(transform.position, newPosition, walkSpeed * Time.deltaTime);
            }
            else // Then get a new random position
            {
                newPosition = GetRoamingPosition();
                walkSpeed = startingWalkSpeed; // Change the walking speed back to original once the enemy reaches the starting position
                findFirstTime = true;
                if (containsParameter)
                    animator.SetBool("Detected", false);
            }
        }
        else // If the target is in range move towards them
        {
            if (findFirstTime)
            {
                alertMark.SetActive(true);
                StartCoroutine(waitForAlert(0.6f));
                findFirstTime = false;
            }
            if (gameObject.tag == "Ranger")
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -runSpeed * 1.07f * Time.deltaTime);
            else if (gameObject.tag == "Melee" || gameObject.tag == "MeleeLeft")
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, runSpeed * Time.deltaTime);
            SpriteDirection(target.transform.position);
/*            if (transform.position.x > target.transform.position.x)
                GetComponentInChildren<SpriteRenderer>().flipX = true;
            else
                GetComponentInChildren<SpriteRenderer>().flipX = false;*/
            walkSpeed = runSpeed;
        }
    }

    public bool AnimatorContainsParameter()
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name == "Detected")
                return true;
        }
        return false;
    }


    // Add the starting position with a random so that the target roams around its starting position
    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDirection() * Random.Range(1f, maxDistance);
    }

    // Generate a random normalized direction
    public static Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void SpriteDirection(Vector3 position)
    {
        if (tag == "MeleeLeft")
        {
            if (transform.position.x > position.x)
                GetComponentInChildren<SpriteRenderer>().flipX = false;
            else
                GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            if (transform.position.x > position.x)
                GetComponentInChildren<SpriteRenderer>().flipX = true;
            else
                GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }

    // On range collision with the player make the foundTarget true so that the enemy can start chasing him
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foundTarget = true;
            if (containsParameter)
                animator.SetBool("Detected", true);
        }
    }

    // When the player leaves the enemy's range wait a few seconds then  make the foundTarget false to stop chasing and return to starting position
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.activeInHierarchy)
            if (collision.gameObject.tag == "Player" && !dead)
            {
                //runSpeed = startingRunSpeed;
                //animator.SetFloat("Walking", 1f);
                StartCoroutine(waitForTarget(4f));
            }
    }
    IEnumerator waitForTarget(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        foundTarget = false;
    }

    IEnumerator waitForAlert(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        alertMark.SetActive(false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // If the enemy hits an obstacle get a new random roaming position
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            newPosition = GetRoamingPosition();
            /* if (foundTarget)
             {
                 Debug.Log(target.transform.position.y);
                 obstacle = true;
                 target.transform.position.y.Equals(target.transform.position.y - 2);
                 Debug.Log(target.transform.position.y);
                 transform.position = Vector2.MoveTowards(transform.position, target.transform.position, runSpeed * Time.deltaTime);
             }*/
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && (gameObject.tag == "Melee" || gameObject.tag == "MeleeLeft"))
        {
            runSpeed = runSpeed / 4;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Melee" && !dead)
        {
            runSpeed = startingRunSpeed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
   // public GameObject target;
   // public float runSpeed;
    public float walkSpeed;
    public Animator animator;
   // [SerializeField] GameObject alertMark;

    //private bool foundTarget = false;
    //private bool obstacle = false;
    private Vector3 startingPosition;
    private Vector3 newPosition;
    private float startingWalkSpeed;
    //private float startingRunSpeed;
    //bool findFirstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        newPosition = GetRoamingPosition();
        //animator.Play("Run");
        //startingWalkSpeed = walkSpeed;
        //startingRunSpeed = runSpeed;
       // target = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void FixedUpdate()
    {
        if (transform.position != newPosition) // Wait until the enemy reaches the new position
        {
            if (transform.position.x < newPosition.x)
                GetComponent<SpriteRenderer>().flipX = true;
            else
                GetComponent<SpriteRenderer>().flipX = false;

            transform.position = Vector2.MoveTowards(transform.position, newPosition, walkSpeed * Time.deltaTime);
            //StartCoroutine(waitFor(Random.Range(1f, 5f)));
            
        }
        else // Then get a new random position
        {
            newPosition = GetRoamingPosition();
            //walkSpeed = startingWalkSpeed; // Change the walking speed back to original once the enemy reaches the starting position
            // findFirstTime = true;
        }
    }

    // Add the starting position with a random so that the target roams around its starting position
    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDirection() * Random.Range(4f, 4f);
    }

    // Generate a random normalized direction
    public static Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    // On range collision with the player make the foundTarget true so that the enemy can start chasing him
    /*    public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
                foundTarget = true;
        }*/

    // When the player leaves the enemy's range wait a few seconds then  make the foundTarget false to stop chasing and return to starting position
    /*    public void OnTriggerExit2D(Collider2D collision)
        {
            if (gameObject.activeInHierarchy)
                if (collision.gameObject.tag == "Player")
                {
                    runSpeed = startingRunSpeed;
                    animator.SetFloat("Walking", 1f);
                    StartCoroutine(waitForTarget(2f));
                }
        }*/
    IEnumerator waitFor(float timeToWait)
    {

        yield return new WaitForSeconds(timeToWait);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, walkSpeed * Time.deltaTime);
    }

    /*    IEnumerator waitForAlert(float timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            alertMark.SetActive(false);
        }*/

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
}

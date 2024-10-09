using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    public float startingMoveSpeed = 5f;
    public float moveSpeed;

    public Rigidbody2D rb;
    public Animator animator;

    private MapManager mapManager;
    //public Camera cam;

    //Interactable focus;
    // maybe public static
    float angle;
    Vector2 movement;
   // Vector2 mousePos;
    //Vector2 lookDir;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = startingMoveSpeed;
        mapManager = FindObjectOfType<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Where the mouse is on screen

        if (movement.x < 0) 
        {
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>()) 
            {
                sprite.flipX = true;
            }
        }
        else if (movement.x > 0)
        {
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.flipX = false;
            }
        }
        // Set the values to the animator parameters
        animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // Finds the angle at which the mouse points
/*        lookDir = mousePos - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;*/

        movement.Normalize(); // Normalize the vector so when 2 input keys are pressed at the same time the speed doesn't double
        float adjustedSpeed = mapManager.GetTileSpeed(transform.position) * moveSpeed;
        rb.MovePosition(rb.position + movement * adjustedSpeed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision) // If what you collide with is an interactable object
    {
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable != null)
        {
            WithinRange(interactable); 
        }
    }

    void WithinRange(Interactable range)
    {
        //focus = newFocus;
        range.InRange(transform); 
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }
}

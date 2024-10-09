using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float damageToGive = 2f;
    public LayerMask enemyLayers;
    public Animator animator;
    public PlayerMovement playerMovement;
   
    //[SerializeField] List<GameObject> weapons;
    AudioManager audioManager;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform arrowPos;

    float angle;
    [SerializeField] Camera mainCamera = default;
    Quaternion bulletRotation;

    float rotation;
    public Weapons weapon = Weapons.Sword;
    public string tool;


    //public EnemyHealthManager enemyHM;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // When pressing left click
        if (Input.GetMouseButtonDown(0))
        {
            switch (weapon)
            {
                case Weapons.Sword:
                    //Debug.Log("sword");
                    animator.SetTrigger("Attack"); // Play the attack animation
                    break;
                case Weapons.Axe:
                    animator.SetTrigger("Axe");
                    break;              
                case Weapons.Pickaxe:
                    animator.SetTrigger("Pickaxe");
                    break;
            }
            //Attack();
        }
    }

    public void SetWeapons(Enum toEquip)
    {
        //weapon = Weapons;
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Position of the mouse
        //Vector3 direction = Direction(transform.position, mousePosition);
        //angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; // Calculate the angle
       // Debug.DrawRay(transform.position, direction * 5f, Color.red); // Create a ray that points to where the mouse is
        bulletRotation = Quaternion.Euler(0, 0, angle); // Convert the angle (degrees) to a new Quaternion

    }

    Vector3 Direction(Vector3 location, Vector3 target)
    {
        // Calculate the direction for the projectile by subtracting the target location (mouse) with the current location
        return (target - location).normalized;
    }

    public enum Weapons
    {
        Sword,
        Axe,
        Pickaxe,
        Bow,
        Wand
    }

    void Attack()
    {
        switch (weapon)
        {
            case Weapons.Sword:
                //audioManager.PlaySound("SwordSwing", 0f);
                //animator.SetTrigger("Attack"); // Play the attack animation
                //animator.SetBool("Attack 0", true);
                // Create a circle with the given range from a specified point and detect all other objects on a given layer
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies) // Damage every enemy that came onto contact with the circle
                {
                    enemy.gameObject.GetComponentInParent<EnemyHealthManager>().HurtEnemy(damageToGive);
                    //enemyHM.HurtEnemy(2f);
                }
                //animator.SetBool("Attack 0", false);
                break;
            case Weapons.Bow:
                break;
            case Weapons.Wand:
                break;
            default:
                break;
        }
        //Instantiate(arrow,transform.position, bulletRotation);
    }

    public void HalveSpeed()
    {
        playerMovement.moveSpeed = playerMovement.moveSpeed / 2f;
        audioManager.PlaySound("WeaponSwing", 0f);
    }

    public void ResetSpeed()
    {
        playerMovement.moveSpeed = playerMovement.startingMoveSpeed;
    }

    void OnDrawGizmosSelected() // Draw on the editor the attack range
    {
/*        if (attackPoint == null)
            return;*/
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

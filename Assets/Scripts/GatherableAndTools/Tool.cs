using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    //public Animator animator;
    [field: SerializeField] public int MinHarvest { get; private set; } = 1;
    [field: SerializeField] public int MaxHarvest { get; private set; } = 3;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask harvestableLayers;
    //public List<Sound> audioSources;


    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        Harvestable harvestable = collision.GetComponent<Harvestable>();

        if(harvestable != null)
        {
            int amountToHarvest = Random.Range(MinHarvest, MaxHarvest);
            //audioManager.PlaySound("HittingTree", 0f);
            harvestable.Harvest(amountToHarvest);
        }
    }*/

    public void AxeAttack()
    {
        Collider2D harvestableHit = Physics2D.OverlapCircle(attackPoint.position, attackRange, harvestableLayers);
        //Debug.Log(harvestable);
        if (harvestableHit != null && harvestableHit.name == "Tree")
        {
            audioManager.PlaySound("HittingTree", 0f);
            Harvestable harvestable = harvestableHit.GetComponent<Harvestable>();
            int amountToHarvest = Random.Range(MinHarvest, MaxHarvest);
            harvestable.Harvest(amountToHarvest);
        }
    }

    public void PickaxeAttack()
    {
        Collider2D harvestableHit = Physics2D.OverlapCircle(attackPoint.position, attackRange, harvestableLayers);
        //Debug.Log(harvestable);
        if (harvestableHit != null && harvestableHit.name == "Iron")
        {
            audioManager.PlaySound("HittingRock", 0f);
            Harvestable harvestable = harvestableHit.GetComponent<Harvestable>();
            int amountToHarvest = Random.Range(MinHarvest, MaxHarvest);
            harvestable.Harvest(amountToHarvest);
        }
    }

    void OnDrawGizmosSelected() // Draw on the editor the attack range
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

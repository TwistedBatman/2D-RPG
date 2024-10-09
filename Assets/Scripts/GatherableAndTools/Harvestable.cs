using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    [field: SerializeField] public int ResourceCount { get; private set; }

    [field: SerializeField] public ParticleSystem ResourceEmitPS { get; private set; }

    private int amountHarvested = 0;

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

    public void Harvest(int amount)
    {
        // Can't harvest more resources than are left in the node
        int amountToSpawn = Mathf.Min(amount, ResourceCount - amountHarvested);

        if(amountToSpawn > 0)
        {
            ResourceEmitPS.Emit(amount);
            amountHarvested += amountToSpawn;
        }

        // When the node is depleted destroy tree
        if(amountHarvested >= ResourceCount)
        {
            if (name == "Tree")
                audioManager.PlaySound("TreeCutDown", 0f);
            else if (name == "Iron")
                audioManager.PlaySound("RockBreak", 0f);
            Destroy(transform.parent.gameObject);
        }
    }
}

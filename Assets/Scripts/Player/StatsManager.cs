using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public static StatsManager statsManager;

    public TextMeshProUGUI melee;
    public TextMeshProUGUI range;
    public TextMeshProUGUI magic;
    public TextMeshProUGUI critical;
    public TextMeshProUGUI health;
    public TextMeshProUGUI mana;
    public TextMeshProUGUI defense;
    public TextMeshProUGUI exp;

    public List<TextMeshProUGUI> stats;

    
    private void Awake()
    {
        if (statsManager != null)
        {
            Debug.LogError("Found more than one Stats Manager in the scene.");
        }
        statsManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStats()
    {
        melee.text = "1%";
        range.text = "0.5%";
        magic.text = "0.5%";
        critical.text = "1%";
        health.text = "100";
        mana.text = "100";
        defense.text = "1";
        //exp.text = "1/100";
    }
}

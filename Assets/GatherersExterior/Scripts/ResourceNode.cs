using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatherersExterior
{
public class ResourceNode : MonoBehaviour
{
    private ParticleSystem _ps;
    private Harvestable _harvestable;

    private void Start()
    {
        _harvestable = GetComponentInChildren<Harvestable>();
        _ps = GetComponentInChildren<ParticleSystem>();

        if (_harvestable == null)
        {
            throw new MissingComponentException($"{name} does not have a harvestable component in itself or children. Invalid resource node");
        }
        else
        {
            _harvestable.OnHarvestableDestroyed.AddListener(ResourceNode_OnHarvestableDestroyed);
        }

        if (_ps == null)
        {
            throw new MissingComponentException($"{name} has no particle system");
        }
    }

    // Queue cleaning of the entire node when the particle system is done rendering particles
    private void ResourceNode_OnHarvestableDestroyed()
    {
        StartCoroutine(FinishNode());
    }

    /// <summary>
    /// Checks for the particle system being done rendering and then completely removes the node from the game scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator FinishNode()
    {
        yield return new WaitUntil(() => _ps.particleCount == 0);

        Destroy(gameObject);
    }
}
}
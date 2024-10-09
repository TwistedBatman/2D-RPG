using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEmitter : MonoBehaviour
{
    [field: SerializeField] public GameObject ObjectPrefab { get; private set; }

    private ParticleSystem ps;
    private List<ParticleSystem.Particle> exitParticles = new();

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleTrigger()
    {
        ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exitParticles);

        foreach(ParticleSystem.Particle p in exitParticles)
        {
            GameObject spawnObject = Instantiate(ObjectPrefab);
            spawnObject.transform.position = p.position;
        }
    }
}

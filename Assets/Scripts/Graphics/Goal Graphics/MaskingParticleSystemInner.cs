using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskingParticleSystemInner : MonoBehaviour
{
    public int count;   // number of particles
    public GameObject maskingParticle;

    private List<GameObject> maskingParticles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(maskingParticles.Count < count && maskingParticle != null)
        // Spawn more particles
        {
            maskingParticles.Add(Instantiate(maskingParticle));
        }

        while(maskingParticles.Count > count)
        // Detroy Particles
        {
            Destroy(maskingParticles[0]);
            maskingParticles.RemoveAt(0);
        }
    }
}

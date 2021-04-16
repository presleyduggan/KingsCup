using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{

    public ParticleSystem parts;
    public int emitVol;
    public bool flow;

    // Start is called before the first frame update
    void Start()
    {
        DisableParticles();
        flow = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(flow != false)
        {
            Pour();
        }
        else
        {
            parts.Stop();
        }
    }

    public void Pour()
    {
            //parts.startColor = new Color(1, 0.92f, .016f, 1);

            parts.Emit(emitVol);
        
    }

    public void DisableParticles()
    {
        parts.Stop();
    }

}

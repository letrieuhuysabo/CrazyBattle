using System.Collections;
using UnityEngine;

public static class PlayAndStopParticle
{
    public static void PlayParticle(Transform trans)
    {
        ParticleSystem ptc = trans.GetComponent<ParticleSystem>();
        if (ptc != null)
        {
            ptc.Play(true);
        }
        if (ptc.GetComponent<Collider2D>() != null)
        {
            ptc.GetComponent<Collider2D>().enabled = true;
        }
    }
    public static void StopParticle(Transform trans)
    {
        ParticleSystem ptc = trans.GetComponent<ParticleSystem>();
        ptc.Stop(true);
        if (ptc.GetComponent<Collider2D>() != null)
        {
            ptc.GetComponent<Collider2D>().enabled = false;
        }
    }
    public async static void StopParticle(Transform trans, float delay)
    {
        await WaitTask.WaitForSeconds(delay);
        StopParticle(trans);
    }
    public static void ResetParticle(Transform trans)
    {
        ParticleSystem ptc = trans.GetComponent<ParticleSystem>();
        ptc.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ptc.Play(true);
        if (ptc.GetComponent<Collider2D>() != null)
        {
            ptc.GetComponent<Collider2D>().enabled = true;
        }
    }
}

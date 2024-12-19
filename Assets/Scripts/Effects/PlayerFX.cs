using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : EntityFX
{
    [Header("Screen shake FX")]
    [SerializeField] private float shakeMultiplier;
    public Vector3 shakeHighDamage;

    private CinemachineImpulseSource screenShake;    

    [Space]
    [SerializeField] private ParticleSystem dustFx;

    protected override void Start()
    {
        base.Start();
        
        screenShake = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        
    }

    public void ScreenShake(Vector3 _shakePower)
    {
        screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * player.facingDir, _shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse();
    }    

    public void PlayDustFX()
    {
        if (dustFx != null)
        {
            dustFx.Play();
        }
    }
}

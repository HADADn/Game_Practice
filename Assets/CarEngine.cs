using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public string blendParameter = "Blend";
    public Animator animator;
    public AudioSource Engine;

    public float minPitch = 0f;
    public float maxPitch = 1.2f;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (Engine == null)
            Engine = GetComponent<AudioSource>();

        if (Engine != null && !Engine.isPlaying)
        {
            Engine.loop = true;
            Engine.Play();
        }
    }

    void Update()
    {
        if (animator != null && Engine != null)
        {
            float blendValue = animator.GetFloat(blendParameter);
            blendValue = Mathf.Clamp01(blendValue);

            float pitch = Mathf.Lerp(minPitch, maxPitch, blendValue);
            Engine.pitch = pitch;
        }
    }
}

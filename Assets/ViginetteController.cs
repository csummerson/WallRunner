using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ViginetteController : MonoBehaviour
{
    public Volume volume;
    
    public Slider health;

    VolumeProfile profile;

    // Start is called before the first frame update
    void Start()
    {
        profile = volume.profile;
    }

    // Update is called once per frame
    void Update()
{
        if (profile.TryGet(out Vignette vignette))
        {
            float healthPercentage = health.value / health.maxValue;

            if (healthPercentage < 0.66f)
            {
                float maxIntensity = 0.6f;
                float intensity = Mathf.Lerp(0, maxIntensity, (0.5f - healthPercentage) / 0.5f);
                
                vignette.intensity.value = intensity;
            }
            else
            {
                vignette.intensity.value = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMusicEffectController : MonoBehaviour
{
    private AudioSpectrum Audio;

    private float LowestBass;
    private float SecondLowestBass;
    private float AverageBass;

    public Light[] BassLights;
    public float BassIntensityMultiplier;
    public float BassLightStandingIntensity;

    private float highesttreble;
    private float Secondhighesttreble;
    private float Averagetreble;

    public Light[] TrebleLights;
    public float trebleIntensityMultiplier;
    public float trebleLightStandingIntensity;
    // Start is called before the first frame update
    void Start()
    {
        Audio = this.GetComponent<AudioSpectrum>();
    }

    // Update is called once per frame
    void Update()
    {
        BeatAnalysis();
        LightManipulation();
    }

    void LightManipulation()
    {
        foreach (Light i in BassLights)
        {
            i.intensity = BassLightStandingIntensity + AverageBass * BassIntensityMultiplier;
        }

        foreach (Light p in TrebleLights)
        {
            p.intensity = trebleLightStandingIntensity + Averagetreble * trebleIntensityMultiplier;
        }
    }

    void BeatAnalysis()
    {
        LowestBass = Audio.Levels[2];
        SecondLowestBass = Audio.Levels[1];
        float thirdlowestbass = Audio.Levels[0];
        AverageBass = ((LowestBass + SecondLowestBass + thirdlowestbass) / 3);

        highesttreble = Audio.Levels[6];
        Secondhighesttreble = Audio.Levels[7];
        float thirdhighesttreble = Audio.Levels[8];
        Averagetreble = ((highesttreble + Secondhighesttreble + thirdhighesttreble) / 3);
    }
}

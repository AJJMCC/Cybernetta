using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMaticBeats : MonoBehaviour
{
    private AudioSpectrum Audio;
    private float LowestBass;
    private float SecondLowestBass;
    private float AverageBass;
    public GameObject AutoCube;


    // Start is called before the first frame update
    void Start()
    {
        Audio = this.GetComponent<AudioSpectrum>();
    }

    // Update is called once per frame
    void Update()
    {
        BeatAnalysis();
        CubeManupulation();
    }

    void BeatAnalysis()
    {
        LowestBass = Audio.Levels[0];
        SecondLowestBass = Audio.Levels[1];
        AverageBass = ((LowestBass + SecondLowestBass) / 2) * 2;
    }

    void CubeManupulation()
    {
        AutoCube.transform.localScale = new Vector3 (AutoCube.transform.localScale.x, AverageBass , AutoCube.transform.localScale.z);
        AutoCube.transform.localPosition = new Vector3(AutoCube.transform.localPosition.x, -0.55f + (AverageBass/2), AutoCube.transform.localPosition.z);
    }
}

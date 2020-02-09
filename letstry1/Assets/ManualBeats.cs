using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualBeats : MonoBehaviour
{
    public float StartOfBeat;
    public float EndOfBeat;
    public float BPM;
    public float[] OnOffBeatSwitches;
    private int BeatCheckOnOff = 0;
    [Space(15)]
    public float[] UniqueBeats;
    private float[] UniquePreBeats;
    private int UniqueBeatsPlayed = 0;
    public GameObject UniqueCube;

    private int amountofBeats;
    private int CurrentBeat = 0;
    private float[] beattimes;
    private float[] prebeattimes;
    float SecBetweenBeat;

    [Space(25)]
    public float BeatScaleMax = 1;
    public float BeasScaleMin = 0.01f;
    public AnimationCurve BeatFloatCurve;
    [Space (15)]
    public GameObject ManualCube;
    public Color NoBeatColor;
    public Color BeatColor;
    public float RotationSpeedMultiplier;
    private float RotScale;

    private AudioSource PlayerOfMusic;
    private bool Playing = false;
    private bool Beating = false;
    private float T;
    // Start is called before the first frame update
    void Start()
    {
        DetermineBeatAmount();
        SetBeats();
        SetPreBeats();
        SetUniquePreBeats();
        ManualCube.GetComponent<Renderer>().material.color = NoBeatColor;
    }

    void Switch()
    {
        if (!Beating)
        {
            ManualCube.GetComponent<Renderer>().material.color = BeatColor;
            Beating = true;
        }
        else
        {
            ManualCube.GetComponent<Renderer>().material.color = NoBeatColor;
            Beating = false;
        }
    }

    void DetermineBeatAmount()
    {
        SecBetweenBeat = 60 / BPM;
       
        float BeatTime = EndOfBeat - StartOfBeat;
        amountofBeats = Mathf.RoundToInt(BeatTime / SecBetweenBeat);
     
        beattimes = new float[amountofBeats];
        prebeattimes = new float[amountofBeats];
        
        Debug.Log(beattimes.Length);
    }
    void SetBeats()
    {
        float CurrentBeatTime = StartOfBeat;

        for (int BeatCurrentNumInLoop = 0; BeatCurrentNumInLoop < beattimes.Length; BeatCurrentNumInLoop++)
        {
            beattimes[BeatCurrentNumInLoop] = CurrentBeatTime;
            CurrentBeatTime += SecBetweenBeat;
        }
    }
    void SetPreBeats()
    {
        float CurrentBeatTime = StartOfBeat;
        float TimeBeforeBeat = SecBetweenBeat / 2;
        //make the actual animation curve that the damage and scale follows
        BeatFloatCurve = new AnimationCurve(
            new Keyframe(0, BeasScaleMin),
            new Keyframe(TimeBeforeBeat * 0.5f, BeasScaleMin),
            // new Keyframe(TimeBeforeBeat * 0.8f, BeatScaleMax ), 
            new Keyframe(TimeBeforeBeat, BeatScaleMax), 
            new Keyframe(TimeBeforeBeat * 1.2f, BeatScaleMax ), 
            new Keyframe(SecBetweenBeat, BeasScaleMin));

        for (int PreBeatCurrentNumInLoop = 0; PreBeatCurrentNumInLoop < beattimes.Length; PreBeatCurrentNumInLoop++)
        {
            prebeattimes[PreBeatCurrentNumInLoop] = CurrentBeatTime - TimeBeforeBeat;
            CurrentBeatTime += SecBetweenBeat;
        }
    }
    void SetUniquePreBeats()
    {
        UniquePreBeats = new float[UniqueBeats.Length];
        float TimeBeforeUBeat = SecBetweenBeat / 2;
        for (int i = 0; i < UniqueBeats.Length; i++)
        {
            UniquePreBeats[i] = UniqueBeats[i] - TimeBeforeUBeat;
            Debug.Log(UniquePreBeats[i]);
        }
    }


    void Update()
    {
        T = Time.time;
        PreBeatCheck();
            if (UniqueBeatsPlayed < UniquePreBeats.Length)
        UniqueBeatCheck();

        CubeMovement();
        
    }
    void CubeMovement()
    {
        if (Beating)
        ManualCube.transform.Rotate(Vector3.up * RotScale);

        RotScale = RotationSpeedMultiplier;
       

       
        if (T > OnOffBeatSwitches[BeatCheckOnOff])
        {
            Switch();
            BeatCheckOnOff++;
        }
    }
    void PreBeatCheck()
    {
       // float TimeSinceStart = Time.time;
        if (T >= prebeattimes[CurrentBeat])
        {
            //Debug.Log("Started Curve On PreBeat");
            StartCoroutine(BeatScaleChanger(SecBetweenBeat));
            CurrentBeat++;
        }


    }
    void UniqueBeatCheck()
    {
        //float TimeSinceStart2 = Time.time;
        if (T >= UniquePreBeats[UniqueBeatsPlayed])
        {
            StopCoroutine("UniqueBeatScaleChanger");
            StartCoroutine("UniqueBeatScaleChanger");
            UniqueBeatsPlayed++;
        }
    }

    IEnumerator UniqueBeatScaleChanger()
    {
        float timer = 0.0f;

        while (timer <= SecBetweenBeat)
        {
            float CurrentCubeScale2 = BeatFloatCurve.Evaluate(timer / SecBetweenBeat);
            //Debug.Log(CurrentCubeScale);
            UniqueCube.transform.localScale = new Vector3(UniqueCube.transform.localScale.x, CurrentCubeScale2, UniqueCube.transform.localScale.z);
            UniqueCube.transform.localPosition = new Vector3(UniqueCube.transform.localPosition.x, -0.53f + (UniqueCube.transform.localScale.y / 2), UniqueCube.transform.localPosition.z);

            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator BeatScaleChanger(float totaltime)
    {
        float timer = 0.0f;

        while (timer <= totaltime)
        {
            float CurrentCubeScale = BeatFloatCurve.Evaluate(timer/ totaltime );
           // Debug.Log(CurrentCubeScale);
            ManualCube.transform.localScale = new Vector3(ManualCube.transform.localScale.x, CurrentCubeScale, ManualCube.transform.localScale.z) ;
            ManualCube.transform.localPosition = new Vector3(ManualCube.transform.localPosition.x, -0.53f + (ManualCube.transform.localScale.y / 2), ManualCube.transform.localPosition.z);

            timer += Time.deltaTime;
            yield return null;
        }
    }
}

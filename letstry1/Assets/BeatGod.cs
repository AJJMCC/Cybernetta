using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGod : MonoBehaviour
{
    public static BeatGod instance;


    public float TimeBetweenBeatsGodVersion;
    public float[] BeatsInThisSong;
    public float CurrentDamage;
    public float UniqueDamage;
    public float RegularDamageKillThreshold;
    public float UniqueDamageKillThreshold;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

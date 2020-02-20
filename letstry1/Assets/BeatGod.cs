using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGod : MonoBehaviour
{
    public static BeatGod instance;
    public bool IsSongBeating;
    [Space(15)]
    public float TimeBetweenBeatsGodVersion;
    public float[] BeatsInThisSong;
    [Space(15)]
    public float CurrentDamage;
    public float UniqueDamage;
    public float RegularDamageKillThreshold;
    public float UniqueDamageKillThreshold;

    [Space(15)]
    private float CurrentAmmo;
    public float MaxAmmo;
    public GameObject BulletPrefab;
    public GameObject Muzzle;
    public float MinTimeBetweenShots;
    private float RealTimeBetweenShots;

    [Space(15)]
    public float CurrentHPStreak;


    void Awake()
    {
        CurrentAmmo = MaxAmmo;
           instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            PlayerClicked();
        }
    }

    void PlayerClicked()
    {
        // check the damage to the thresholds
        if (UniqueDamage > UniqueDamageKillThreshold)
        {

            Debug.Log("Fired a Special Bullet");
            FireSpecial();
            return;
        }
      
        else if (CurrentDamage > RegularDamageKillThreshold)
        {
            Debug.Log("Fired a High Damage Bullet");
            FireHighDamageRegular();
            return;
        }

        else
        {
            Debug.Log("Fired a Bullet");
            FireRegular();
            return;
        }
        
       
    }

    void FireSpecial()
    {
        GameObject SpecBullet = GameObject.Instantiate(BulletPrefab, Muzzle.transform.position, Muzzle.transform.rotation);
        SpecBullet.GetComponent<Bullet>().SpecialBullet = true;

    }

    void FireHighDamageRegular()
    {
        GameObject SpecBullet = GameObject.Instantiate(BulletPrefab, Muzzle.transform.position, Muzzle.transform.rotation);
        SpecBullet.GetComponent<Bullet>().HighPowerBullet = true;
        CurrentAmmo--;
        CurrentHPStreak++;
    }

    void FireRegular()
    {
        GameObject SpecBullet = GameObject.Instantiate(BulletPrefab, Muzzle.transform.position, Muzzle.transform.rotation);
        CurrentAmmo--;
        CurrentHPStreak = 0;
    }
}

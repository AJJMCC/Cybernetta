using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatGod : MonoBehaviour
{
    public static BeatGod instance;
    public bool IsSongBeating;
    [Space(15)]
    public float TimeBetweenBeatsGodVersion;
    public float[] BeatsInThisSong;
    public int CurrentBeatForChecks;
    [Space(15)]
    public float CurrentDamage;
    public float UniqueDamage;
    public float RegularDamageKillThreshold;
    public float UniqueDamageKillThreshold;

    [Space(15)]
    public float MinTimeBetweenShots;
    private float RealTimeBetweenShots;

    [Space(15)]
    private float CurrentAmmo;
    public float MaxAmmo;
    public GameObject BulletPrefab;
    public GameObject Muzzle;
   

    [Space(15)]
    public float CurrentHighPowerStreak;
    public Text StreakNumber;

    [Space(15)]
    public float ReloadMinPercentageOfBeat = 0.3f;
    public float RealReloadMinTIme;
    private float ReloadTime;
    bool Reloading;
    public GameObject ReloadingText;
    public AnimationCurve ReloadEffectsCurve;
    public Vector3 ReloadTextStartPosition;
    public Vector3 ReloadTextendPosition;
    public AnimationCurve ReloadEffectsMoveCurve;

    void Awake()
    {
        RealTimeBetweenShots = MinTimeBetweenShots;
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
        RealTimeBetweenShots -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && !Reloading && RealTimeBetweenShots <= 0)
        {
            PlayerClicked();
        }


        if (Reloading)
        {
            ReloadTime -= Time.deltaTime;
            if (ReloadTime <= 0)
            {
                Debug.Log("Stopped Reloading");
                Reloading = false;
            }
        }
        UIControl();
    }
    void UIControl()
    {
        //ui controls
        StreakNumber.text = CurrentHighPowerStreak.ToString();
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
            if (CurrentAmmo > 0)
            {
                Debug.Log("Fired a High Damage Bullet");
                FireHighDamageRegular();
                return;
            }

            else Reload();
        }

        else
        {
            if (CurrentAmmo > 0)
            {
                Debug.Log("Fired a Bullet");
                FireRegular();
                return;
            }
            else Reload();
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
        CurrentHighPowerStreak++;
        RealTimeBetweenShots = MinTimeBetweenShots;
    }

    void FireRegular()
    {
        GameObject SpecBullet = GameObject.Instantiate(BulletPrefab, Muzzle.transform.position, Muzzle.transform.rotation);
        CurrentAmmo--;
        CurrentHighPowerStreak = 0;
        RealTimeBetweenShots = MinTimeBetweenShots;
    }

    void Reload()
    {
        // decide whether reload fits within this beat. 
        float TimeForReload = BeatsInThisSong[CurrentBeatForChecks] - RealReloadMinTIme;
        if (Time.time > TimeForReload)
        {
            float t = BeatsInThisSong[CurrentBeatForChecks] - Time.time;
            ReloadTime = t;
        }

        else
        {
            float t = (BeatsInThisSong[CurrentBeatForChecks +1] - Time.time);
            ReloadTime = t;
        }
        Debug.Log("Started Reloading");
        Reloading = true;
        StartCoroutine("ReloadCoRot");
        Invoke("ReloadComplete", ReloadTime);
    }

    void ReloadComplete()
    {
        CurrentAmmo = MaxAmmo;
        Reloading = false;

    }
    IEnumerator ReloadCoRot()
    {
        float timer = 0.0f;

        while (timer <= ReloadTime)
        {
            float Curve = ReloadEffectsCurve.Evaluate(timer / ReloadTime);
            float MoveCurve = ReloadEffectsMoveCurve.Evaluate(timer / ReloadTime);
            ReloadingText.GetComponent<Text>().color = Color.Lerp(Color.clear, Color.red, Curve);
            ReloadingText.GetComponent<Transform>().localPosition = Vector3.Lerp(ReloadTextStartPosition, ReloadTextendPosition, MoveCurve);
            timer += Time.deltaTime;
            yield return null;
        }
    }

}

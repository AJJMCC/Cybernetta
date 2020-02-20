using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool SpecialBullet = false;
    public GameObject  SpecialTrail;
    public float SpecialTrailTime;
    public float SpecialRebounds;
    private float reboundcurrentcount;

    [Space(15)]
    public bool HighPowerBullet = false;
    public GameObject HighPowerTrail;
    public float HighPowerTrailTime;
    [Space(15)]
    public PhysicMaterial Bouncy;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        reboundcurrentcount = SpecialRebounds;
        if (SpecialBullet)
        {
            SpecialTrail.SetActive(true);
        }

        else if (HighPowerBullet)
        {
            HighPowerTrail.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Speed);
    }

    void OnCollisionEnter(Collision Other)
    {
            // tell enemy it got hit
            if (SpecialBullet)
            {
                if (Other.collider.tag == "Enemy")
                {
                }

                if (reboundcurrentcount < 0)
                {
                    reboundcurrentcount--;
                }
                else
                    Destroy(this.gameObject);
            }

            else if (HighPowerBullet)
            {
                 if (Other.collider.tag == "Enemy")
                 {
                 }
            Destroy(this.gameObject);
            }

            else
            {
              if (Other.collider.tag == "Enemy")
              {
              }
            Destroy(this.gameObject);
            }



        }
    }


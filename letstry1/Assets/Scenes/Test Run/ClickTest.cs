using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTest : MonoBehaviour
{
    public GameObject CubeManual;
    public GameObject CubeAuto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Damage = " + CubeManual.transform.localScale.y);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Damage Automatic = " + CubeAuto.transform.localScale.y);
        }
    }
}

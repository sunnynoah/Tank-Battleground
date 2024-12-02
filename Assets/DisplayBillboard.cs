using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBillboard : MonoBehaviour
{
    
    void Start()
    {
        Camera mainCam = Camera.main;

        transform.LookAt(mainCam.gameObject.transform.position);
        transform.localScale = new Vector3(-1, 1, 1);
    }   
}

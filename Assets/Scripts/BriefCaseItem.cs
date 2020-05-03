using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefCaseItem : MonoBehaviour
{
    public int pointValue;
    public bool golden;

    public bool validPlacement = true;
    public bool inBriefCase = false;

    public Material ogMat;
    //public Material goodMat;
    public Material badMat;

    //public bool grabbable = true;
    void Start()
    {
        ogMat = GetComponent<Renderer>().material;
        
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.transform.gameObject.layer == LayerMask.NameToLayer("BriefCaseItem"))
        {
            validPlacement = false;
            GetComponent<Renderer>().material = badMat;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("BriefCaseItem"))
        {
            validPlacement = true;
            GetComponent<Renderer>().material = ogMat;
        }
    }
}

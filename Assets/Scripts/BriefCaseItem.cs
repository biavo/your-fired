using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefCaseItem : MonoBehaviour
{
    public int pointValue;

    public bool validPlacement = true;

    public Material goodMat;
    public Material badMat;

    void OnCollisionStay(Collision collision)
    {
        if(collision.transform.gameObject.layer == LayerMask.NameToLayer("BriefCaseItem"))
        {
            validPlacement = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("BriefCaseItem"))
        {
            validPlacement = true;
        }
    }
}

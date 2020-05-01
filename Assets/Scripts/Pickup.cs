using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform theDestination;

    void onMouseDown(){
        print("clicked");
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = theDestination.position;
        this.transform.parent = GameObject.Find("Destination").transform;
    }

    void onMouseUp(){
        GetComponent<BoxCollider>().enabled = true;
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
    }
}

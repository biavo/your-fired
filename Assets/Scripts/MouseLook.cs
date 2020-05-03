using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MouseLook : MonoBehaviour
{
    float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    public Transform ItemParent;

    public Transform spawnPos1;
    public Transform spawnPos2;

    public Animator ArmAnimator;
    public int itemsStolenCount;
    public ButtonScript mainButtonScript;

    public StudioEventEmitter ItemStolenSound;

    void Start()
    {
        mainButtonScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ButtonScript>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void spawnItem(RaycastHit hit)
    {
        Vector3 spawnPos;
        var a = Random.Range(1, 3);
        //Debug.Log(a);
        if (a == 1)
        {
            spawnPos = new Vector3(spawnPos1.position.x + Random.Range(-1.5f, 1.5f), spawnPos1.position.y, spawnPos1.position.z + Random.Range(-0.5f, 0.5f));
        }
        else
        {
            spawnPos = new Vector3(spawnPos2.position.x + Random.Range(-1.5f, 1.5f), spawnPos2.position.y, spawnPos2.position.z + Random.Range(-0.5f, 0.5f));
        }
        //Vector3 spawnPos = ItemParent.position;
        var o = Instantiate(Resources.Load<GameObject>(hit.transform.gameObject.tag + "Inv"), spawnPos, Quaternion.identity, ItemParent);
        o.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
       
    void Update() {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 5;
        Debug.DrawRay(transform.position, forward, Color.red);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 5f))
        {
            //print("hit");
            if (Input.GetButtonDown("Fire1"))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Item"))
                {
                    ItemStolenSound.Play();
                    ArmAnimator.Play("Swipe");
                    Destroy(hit.transform.gameObject);
                    itemsStolenCount += 1;
                    mainButtonScript.setItemsStolenGameUI(itemsStolenCount);
                    spawnItem(hit);
                }
            }
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f , 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
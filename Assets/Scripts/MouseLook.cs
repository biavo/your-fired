using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public int stapler;
    public int printer;
    public int pen;
    public int fancyPen;
    public int mug;
    public int monitor;
    public int mouse;
    public int keyboard;
    public int computer;

    void Update() {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 5;
        Debug.DrawRay(transform.position, forward, Color.red);
        if(Physics.Raycast( Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 5f)){
            print("hit");
            if(Input.GetButtonDown("Fire1")){
                print("fire");
                if(hit.transform.gameObject.tag == ("Pen")){
                    pen ++;
                    Destroy(hit.transform.gameObject);
                }
                if(hit.transform.gameObject.tag == ("Printer")){
                    printer ++;
                    Destroy(hit.transform.gameObject);
                }
                if(hit.transform.gameObject.tag == ("Stapler")){
                    stapler ++;
                    Destroy(hit.transform.gameObject);
                }
                if(hit.transform.gameObject.tag == ("FancyPen")){
                    fancyPen ++;
                    Destroy(hit.transform.gameObject);
                }
                if(hit.transform.gameObject.tag == ("Mug")){
                    mug ++;
                    Destroy(hit.transform.gameObject);
                }
                if(hit.transform.gameObject.tag == ("Monitor")){
                    monitor ++;
                    Destroy(hit.transform.gameObject);
                }
                if(hit.transform.gameObject.tag == ("Mouse")){
                    mouse ++;
                    Destroy(hit.transform.gameObject);
                }
                if(hit.transform.gameObject.tag == ("Keyboard")){
                    keyboard ++;
                    Destroy(hit.transform.gameObject);
                }
                if(hit.transform.gameObject.tag == ("Computer")){
                    computer ++;
                    Destroy(hit.transform.gameObject);
                }
            }
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f , 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
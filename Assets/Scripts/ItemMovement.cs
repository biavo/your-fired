using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ItemMovement : MonoBehaviour
{
    public pointTally scoreArea;
    public Camera cam;
    public GameObject selectedItem = null;

    public customGrid grid;

    public Vector3 startPos;
    public Vector3 startRot;

    public StudioEventEmitter PlaceInBriefCaseSound;
    public StudioEventEmitter ItemSelectSound;
    public StudioEventEmitter ItemDestroySound;

    //public LayerMask cullingMask;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit/*, cullingMask*/))
            {
                Transform objectHit = hit.transform;
                if(objectHit.gameObject.layer == LayerMask.NameToLayer("BriefCaseItem") && objectHit.gameObject.tag != "BriefCaseWall")
                {
                    ItemSelectSound.Play();
                    selectedItem = objectHit.parent.gameObject;
                    startPos = selectedItem.transform.position;
                    startRot = selectedItem.transform.eulerAngles;

                    //grid.target = selectedItem.transform.GetChild(0).gameObject;
                    //grid.structure = selectedItem.transform.GetChild(1).gameObject;
                }
                else
                {
                    selectedItem = null;    
                }
            }
            else
            {
                selectedItem = null;
            }
        }

        if(Input.GetMouseButton(0) && selectedItem)
        {
            //move the item around
            //new raycast that ignores briefcaseitems....
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("BriefCase")))
            {
                //item position is at hit position?
                var pos = new Vector3(hit.point.x, hit.point.y + selectedItem.transform.GetChild(1).localScale.y / 2, hit.point.z);
                selectedItem.transform.position = pos;
                
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                //selectedItem.transform.rotation = Quaternion.Euler(selectedItem.transform.rotation.x, selectedItem.transform.rotation.y + 90, selectedItem.transform.rotation.z);
                var rot = selectedItem.transform.rotation;
                selectedItem.transform.rotation = rot * Quaternion.Euler(0, 90, 0);
            }
        }

        if(Input.GetMouseButtonDown(1) && selectedItem)
        {
            ItemDestroySound.Play();
            scoreArea.items.Remove(selectedItem);
            Destroy(selectedItem);
            selectedItem = null;
            
            scoreArea.calculateScore();
        }

        

        if(Input.GetMouseButtonUp(0) && selectedItem)
        {
            if(!selectedItem.GetComponentInChildren<BriefCaseItem>().validPlacement)
            {
                selectedItem.transform.position = startPos;
                selectedItem.transform.eulerAngles = startRot;
            }
            else
            {
                PlaceInBriefCaseSound.Play();
                scoreArea.calculateScore();
            }
            
            selectedItem = null;
            
        }
    }
}

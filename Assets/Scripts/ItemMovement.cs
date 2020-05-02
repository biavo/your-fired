using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public Camera cam;
    public GameObject selectedItem = null;

    public customGrid grid;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if(objectHit.gameObject.layer == LayerMask.NameToLayer("BriefCaseItem"))
                {
                    selectedItem = objectHit.parent.gameObject;
                    grid.target = selectedItem.transform.GetChild(0).gameObject;
                    grid.structure = selectedItem.transform.GetChild(1).gameObject;
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

        

        if(Input.GetMouseButtonUp(0))
        {
            selectedItem = null;
        }
    }
}

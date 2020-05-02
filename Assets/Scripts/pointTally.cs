using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pointTally : MonoBehaviour
{
    public TextMeshPro scoreText;
    public List<GameObject> items = new List<GameObject>();

    int pointsTotal = 0;


    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BriefCaseItem>())
        {
            items.Add(other.transform.parent.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BriefCaseItem>())
        {
            items.Remove(other.transform.parent.gameObject);
        }
    }

    public void calculateScore()
    {
        pointsTotal = 0;
        //do the score algorithm here... tally all points together, deduct for repeat items, and write score to canvas.
        foreach(var i in items)
        {
            pointsTotal += i.GetComponentInChildren<BriefCaseItem>().pointValue;
        }
        scoreText.text = "$" + pointsTotal;
    }
}

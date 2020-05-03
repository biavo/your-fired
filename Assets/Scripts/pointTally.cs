using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pointTally : MonoBehaviour
{
    public TextMeshPro scoreText;
    public List<GameObject> items = new List<GameObject>();

    public int pointsTotal = 0;
    public int goldenItems = 0;
    public int pens = 0;

    public ButtonScript mainButtonScript;
    void Start()
    {
        mainButtonScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ButtonScript>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BriefCaseItem>())
        {
            if(!other.GetComponent<BriefCaseItem>().inBriefCase/* && other.GetComponent<BriefCaseItem>().validPlacement*/)
            {
                other.GetComponent<BriefCaseItem>().inBriefCase = true;
                if(other.GetComponent<BriefCaseItem>().golden)
                {
                    goldenItems += 1;
                }

                if(other.name == "NormalPen")
                {
                    pens += 1;
                    if(pens >= 30)
                    {
                        mainButtonScript.SetAch50Pens();
                    }
                }
                    
                items.Add(other.transform.parent.gameObject);
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BriefCaseItem>())
        {
            if (other.GetComponent<BriefCaseItem>().inBriefCase)
            {
                other.GetComponent<BriefCaseItem>().inBriefCase = false;
                if (other.GetComponent<BriefCaseItem>().golden)
                {
                    goldenItems -= 1;
                }

                if (other.name == "NormalPen")
                {
                    pens -= 1;
                }
                items.Remove(other.transform.parent.gameObject);
            }
        }
    }

    public void calculateScore()
    {
        pointsTotal = 0;
        //do the score algorithm here... tally all points together, deduct for repeat items, and write score to canvas.
        foreach(var i in items)
        {
            if(i.GetComponentInChildren<BriefCaseItem>().validPlacement)
            {
                pointsTotal += i.GetComponentInChildren<BriefCaseItem>().pointValue;
            }
            
        }

        if (goldenItems == 5)
        {
            pointsTotal += 1000;
            mainButtonScript.SetGoldItemsAch();
            
        }
            

        scoreText.text = "$" + pointsTotal;
        mainButtonScript.setScoreGameUI(pointsTotal);
        mainButtonScript.currentScore = pointsTotal;
        mainButtonScript.setGoldenItemsGameUI(goldenItems);
    }
}

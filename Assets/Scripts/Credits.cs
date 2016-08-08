using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour {

    public Sprite credits;
    public Sprite credits1;
    public Sprite credits2;
    public Sprite creditsSounds;
    public Sprite credits3;
    public Sprite credits4;
    public Sprite credits5;
    public Sprite credits6;


    private Sprite[] allCredits;
    private double totalTime;
    private int i;

	// Use this for initialization
	void Start () {

        allCredits = new Sprite[] { credits, credits1, credits2, creditsSounds, credits3, credits4, credits5, credits6 };
        i = 1;

        GetComponent<Image>().sprite = allCredits[0];
        totalTime = 0.0;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.anyKey)
        {
            Application.Quit();
        }


        totalTime += Time.deltaTime;

        if (i < 5 && totalTime > 5.0) 
        {
            GetComponent<Image>().sprite = allCredits[i];
            i += 1;
            totalTime = 0.0;
        }
        else if (i < 8 && totalTime > i * 5.0)
        {
            GetComponent<Image>().sprite = allCredits[i];
            i += 1;
            totalTime = 0.0;
        }


        /*
        totalTime += Time.deltaTime;

        if (totalTime > 10.0)
        {
            GetComponent<Image>().sprite = credits2;
        }
        else if (totalTime > 5.0)
        {
            GetComponent<Image>().sprite = credits1;
        }
	    */
	}
}

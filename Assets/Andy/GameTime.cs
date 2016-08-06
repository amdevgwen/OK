using UnityEngine;
using System.Collections;

// Tracks how much time, in seconds, has passed in the game.
public class GameTime : MonoBehaviour {

    public Transform sun;
    public float sunsetDegrees;

    Quaternion degreeOrigin;


    public Color sunriseColor;          // 
    public Color noonColor;
    public Color eveningColor;

    Color currentColor;

    private float dayLength;      // Number of seconds in a single "day" phase.
    private float currentTime;    // Number of seconds since the start of the level.
    private float noonTime;     // Half of dayLength.  Used to transition sun colors.
    private double timeChange;   // Number of seconds since the last time Update() was called.



	// Use this for initialization
	void Start () {
        currentTime = 0;
        StartCoroutine( TimeTick() );
        degreeOrigin = sun.rotation;

        noonTime = (int)dayLength >> 1; // fancy way to divide by 2.

	}


    void UpdateSun()
    {
        Quaternion newsunpos = degreeOrigin;
        newsunpos.x = degreeOrigin.x + ( sunsetDegrees * (dayLength / currentTime) );
        sun.rotation = newsunpos;

        if ( currentTime / dayLength  < 0.5)
        {
            currentColor = Color.Lerp(sunriseColor, noonColor, currentTime / noonTime);
        }
        else
        {
            currentColor = Color.Lerp(noonColor, eveningColor, (currentTime - noonTime) / noonTime );
        }
        

    }


    public void InitializeGame()
    {
                
    }

    IEnumerator TimeTick()
    {
        currentTime = 0;
        while (currentTime > dayLength)
        {
            yield return new WaitForSeconds(1);
            currentTime++;
        }
        Debug.Log("Game end");
    }        

    public int getCurrentTime()
    {
        return (int)currentTime;
    }
}

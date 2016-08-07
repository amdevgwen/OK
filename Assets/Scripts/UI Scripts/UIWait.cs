using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIWait : MonoBehaviour {
    public float Wait;

    void Start()
    {
        StartCoroutine(mainth());
    }

    IEnumerator mainth()
    {

        yield return new WaitForSeconds(Wait);
        transform.GetComponent<Canvas>().enabled = true;

    }

    public void LoadThing(string k)
    {
        if ((k != "") && (k.ToLower() != "quit"))
        {
            SceneManager.LoadScene(1);
        }
    }

}

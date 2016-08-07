using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TotalSlimes : MonoBehaviour {
    public Text Total;
    public Text Current;

    public void Update()
    {
        Total.text = GameMain.instance.AllMinions.Count.ToString();
        Current.text = GameMain.instance.CurrentMinions.Count.ToString();

    }





}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PearlCount : MonoBehaviour
{
    public int pearlCount;
    public TMP_Text pearlText;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        pearlCount = player.GetPearlCount();
    }

    // Update is called once per frame
    void Update()
    {
        pearlCount = player.GetPearlCount();
        pearlText.text = pearlCount.ToString() + " :";
    }
}

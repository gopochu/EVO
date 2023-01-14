using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWar_manager : MonoBehaviour
{
    public GameObject fog;
    public int startxCord = -185;
    public int startyCord = 235;
    public int offSet = 55;
    void Start()
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (startxCord + x * offSet != 0 || startyCord - y * offSet != 1)
                {
                    var newPos = new Vector2(startxCord + x * offSet, startyCord - y * offSet);
                    Instantiate(fog, newPos, Quaternion.identity);
                }
            }
        }
    }
}

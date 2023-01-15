using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWindowMenu : MonoBehaviour
{
    [SerializeField] private GameObject _thisWindow;
    [SerializeField] private GameObject _nextWindow;

    public void NextWindow()
    {
        _nextWindow.SetActive(true);
        _thisWindow.SetActive(false);
    }
}

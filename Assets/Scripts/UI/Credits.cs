using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credits;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            credits.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}

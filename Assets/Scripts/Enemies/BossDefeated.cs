using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefeated : MonoBehaviour
{
    public GameObject victoryWindow;

    private void OnDisable()
    {
        victoryWindow.SetActive(true);
        Time.timeScale = 0f;
    }
}

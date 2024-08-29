using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMuzzelEffect : MonoBehaviour
{
    public void Show(bool isOn)
    {
        gameObject.gameObject.SetActive(isOn);
    }
}

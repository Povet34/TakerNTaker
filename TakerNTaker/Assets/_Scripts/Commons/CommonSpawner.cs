using CodeMonkey.Utils;
using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSpawner : Singleton<CommonSpawner>
{
    #region 3D UI Arrow Object

    [SerializeField] GameObject ui3DArrowPrefab;
    GameObject ui3DArrowObject;

    public GameObject GetUI3DArrow()
    {
        if (!ui3DArrowObject)
            ui3DArrowObject = Instantiate(ui3DArrowPrefab, transform.position, Quaternion.identity);

        return ui3DArrowObject;
    }

    public void ShowUI3DArrow(bool isOn)
    {
        GetUI3DArrow().SetActive(isOn);
    }

    public void SetUI3DArrowPosition(Vector2 pos)
    {
        GetUI3DArrow().transform.position = pos;
    }

    public void SetUI3DArrowRotation(Quaternion rotation)
    {
        GetUI3DArrow().transform.rotation = rotation;
    }

    public void SetDirectionAndPosition_FormPlayer(Vector2 dir)
    {
        SetUI3DArrowPosition(dir + GameManager.instance.player.GetPosXY());
        SetUI3DArrowRotation(Quaternion.Euler(new Vector3(0, 0, UtilsClass.GetAngleFromVector(dir) - 180)));
    }

    #endregion


    #region Charing Gauge

    #endregion
}

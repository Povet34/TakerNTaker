using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDataMngr : MonoBehaviour
{
    public static AttackDataMngr instance;
    [SerializeField] List<ClassAttackData> datas;

    private void Awake()
    {
        instance = this;
    }

    public List<ClassAttackData> GetAll() => datas;

    public List<ClassAttackData> GetClassCategory(Definitions.eClassType type)
    {
        List<ClassAttackData> result = new List<ClassAttackData>();
        foreach(var data in datas)
        {
            if(data.classType == type)
            {
                result.Add(data);
            }
        }
        return result;
    }
}

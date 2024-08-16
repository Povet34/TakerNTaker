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

    public ClassAttackData GetRandomClassAttack(Definitions.eClassType type)
    {
        if(type == Definitions.eClassType.None)
        {
            return datas[Random.Range(0, datas.Count - 1)];
        }
        else
        {
            var classes = GetClassCategory(type);
            return classes[Random.Range(0, classes.Count - 1)];
        }
    }
}

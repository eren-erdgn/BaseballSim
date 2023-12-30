using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCatcherManager : MonoBehaviour
{

    [SerializeField] private BaseCatcher[] baseCatchers;
    public static BaseCatcherManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public Vector3 GetPosOfNextBaseCathcer(int currentBaseCatcherIndex)
    {
        
        return baseCatchers[currentBaseCatcherIndex + 1].transform.position; 

    }
    public int GetIndexOfBaseCatcher(BaseCatcher baseCatcher)
    {
        for (int i = 0; i < baseCatchers.Length; i++)
        {
            if (baseCatchers[i] == baseCatcher)
            {
                return i;
            }
        }

        return -1;

    }
}

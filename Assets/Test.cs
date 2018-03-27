using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Reorderable]
    public Data[] data = new Data[0];

    [System.Serializable]
    public struct Data
    {
        public int ID;
        public string Name;
        [Reorderable]
        public int[] Numbers;
    }
}

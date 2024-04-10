using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demo : MonoBehaviour
{
    public static demo instance;

    private void Start()
    {
        instance = this;
    }
}

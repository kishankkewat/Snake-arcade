using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public void Purchase10Diamonds()
    {
        Purchaser.instance.Buy10Diamonds();
    }

    public void Purchase50Hearts()
    {
        Purchaser.instance.Buy50Hearts();
    }

    public void Purchase5Hearts()
    {
        Purchaser.instance.Buy5Hearts();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSwap : MonoBehaviour
{
    public Gun gun;

    public void SwapFinished()
    {
        gun.endSwap();
    }
}

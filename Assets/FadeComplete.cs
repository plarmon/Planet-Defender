using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeComplete : MonoBehaviour
{
    public GameManager gm;

    public void Complete()
    {
        gm.OnFadeComplete();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    protected internal Image Image { get; private set; }
    
    void Start()
    {
        Image = GetComponent<Image>();
    }
}

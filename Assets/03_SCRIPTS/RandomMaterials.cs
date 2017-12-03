using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterials : MonoBehaviour
{

    private Renderer m_Renderer = null;

    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponentInChildren<Renderer>();
    }

}

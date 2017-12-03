using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterials : MonoBehaviour
{

    private Renderer m_Renderer = null;
    public List<Material> m_MaterialList;

    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponentInChildren<Renderer>();
        m_Renderer.material = m_MaterialList[Random.Range(0, m_MaterialList.Count)];
    }

}

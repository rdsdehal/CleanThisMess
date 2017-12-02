using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{

    public List<GameObject> m_SpawnPoint;
    public GameObject m_ChildPrefab = null;

    private float m_Timer = 0;
    private float m_NextSpawnTime = 0;

    private void Awake()
    {
        m_NextSpawnTime = Random.Range(5f, 10f);
        if (m_ChildPrefab == null)
        {
            Debug.LogWarning("[EntryPoint] has no ChildPrefab.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_NextSpawnTime)
        {
            Instantiate(m_ChildPrefab, m_SpawnPoint[]);
        }
    }
}

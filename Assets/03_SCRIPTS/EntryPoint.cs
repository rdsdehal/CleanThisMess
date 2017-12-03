using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{

    public List<GameObject> m_SpawnPoint;
    public List<ChildBehaviour> m_ChildList;
    public GameObject m_ChildPrefab = null;
    public MayhemMeter m_MayhemMeter = null;
    public Vector3 LeavePoint = Vector3.zero;

    private float m_Timer = 0;
    private float m_NextSpawnTime = 0;
    public int m_SpawnIndex = 0;

    public bool m_MustReload = false;

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
            GameObject obj = Instantiate(m_ChildPrefab, m_SpawnPoint[m_SpawnIndex].transform.position, Quaternion.identity);
            m_ChildList.Add(obj.gameObject.GetComponent<ChildBehaviour>());
            m_ChildList[m_SpawnIndex].m_CurrentWaitPoint = m_SpawnIndex;
            m_SpawnIndex++;
            m_Timer = 0;
        }
    }
    public Vector3 GetNewSpawnPoint(int lastIndex)
    {
        return m_SpawnPoint[lastIndex--].transform.position;
    }

    public void ReloadChild()
    {
        m_MustReload = true;
    }
}

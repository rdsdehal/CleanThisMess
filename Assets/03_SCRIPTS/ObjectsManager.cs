using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public List<GameObject> m_ChairList;
    public List<GameObject> m_ThrowableList;

    public GameObject FindChair(GameObject m_Child)
    {
        GameObject m_Chair = null;
        m_Chair = m_ChairList[Random.Range(0, m_ChairList.Count)];
        return m_Chair;
    }

    public GameObject FindNearestThrowable(GameObject m_Child)
    {
        GameObject NearestThrowable = null;
        float lastDistance = Mathf.Infinity;
        foreach (GameObject element in m_ThrowableList)
        {
            float calculatedDistance = Mathf.Abs(Vector3.Distance(m_Child.transform.position, element.transform.position));
            if (lastDistance > calculatedDistance)
            {
                lastDistance = calculatedDistance;
                NearestThrowable = element;
            }
        }
        return NearestThrowable;
    }
}


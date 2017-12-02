using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    private List<Chair> m_ChairList;
    public List<GameObject> m_ThrowableList;

    private void Awake()
    {
        m_ChairList = FindObjectsOfType<Chair>().ToList();
    }

    public Chair FindChair(GameObject m_Child)
    {
        Chair m_Chair = null;
        Chair[] m_AvailableChair = m_ChairList.Where(m => CheckChairRotation(m) == true).ToArray();
        if (m_AvailableChair.Length > 0)
        {
            m_Chair = m_AvailableChair[Random.Range(0, m_AvailableChair.Length)];
        }
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

    public bool CheckChairRotation(Chair m_Chair)
    {
        bool m_IsValid = false;
        if (Mathf.Round(m_Chair.transform.localEulerAngles.x) <= 0.1f && Mathf.Round(m_Chair.transform.localEulerAngles.z) <= 0.1f)
        {
            m_IsValid = true;
        }
        return m_IsValid;
    }
}


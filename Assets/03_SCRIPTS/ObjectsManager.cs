using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public List<Chair> m_ChairList;
    public List<GameObject> m_ThrowableList;
    public Chair m_LastChair = null;

    public Chair FindChair(GameObject m_Child)
    {
        Chair m_Chair = null;
        List<Chair> m_AvailableChair = m_ChairList.Where(m => CheckChairRotation(m) == true && m.isOccupied == false).ToList();
        if (m_AvailableChair.Count() > 0)
        {
            if (m_LastChair != null)
            {
                m_AvailableChair.Remove(m_LastChair);
            }
            if (m_AvailableChair.Count() > 0)
            {
                m_Chair = m_AvailableChair[Random.Range(0, m_AvailableChair.Count())];
            }
        }
        m_LastChair = m_Chair;
        return m_Chair;
    }

    public GameObject FindNearestThrowable(GameObject m_Child)
    {
        GameObject NearestThrowable = null;
        float lastDistance = Mathf.Infinity;
        List<GameObject> m_AvailableToThrow = m_ThrowableList;
        if (m_LastChair != null)
        {
            m_AvailableToThrow.Remove(m_LastChair.gameObject);
        }
        foreach (GameObject element in m_AvailableToThrow)
        {
            if (element.gameObject.activeSelf == true)
            {
                float calculatedDistance = Mathf.Abs(Vector3.Distance(m_Child.transform.position, element.transform.position));
                if (lastDistance > calculatedDistance)
                {
                    lastDistance = calculatedDistance;
                    NearestThrowable = element;
                }
            }
        }
        return NearestThrowable;
    }

    public bool CheckChairRotation(Chair m_Chair)
    {
        bool m_IsValid = false;
        if (Vector3.Dot(m_Chair.transform.up, Vector3.up) > 0.8f)
        {
            m_IsValid = true;
        }
        return m_IsValid;
    }
}


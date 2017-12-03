using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public List<Chair> m_ChairList;
    public List<GameObject> m_ThrowableList;
    public Chair m_LastChair = null;

    private void Awake()
    {
        m_ThrowableList = FindObjectsOfType<MoveableObject>().Where(m => m.canBePickedUp).Select(m => m.gameObject).ToList();
    }

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

    public GameObject FindThrowable(GameObject m_Child)
    {
        GameObject RandomThrowable = null;
        List<GameObject> m_AvailableToThrow = m_ThrowableList;
        if (m_LastChair != null)
        {
            m_AvailableToThrow.Remove(m_LastChair.gameObject);
        }
        for (int i = m_AvailableToThrow.Count(); i <= 0; i--)
        {
            if (m_AvailableToThrow[i].activeSelf == false)
            {
                m_AvailableToThrow.Remove(m_AvailableToThrow[i]);
            }
        }
        RandomThrowable = m_AvailableToThrow[Random.Range(0, m_AvailableToThrow.Count())];
        return RandomThrowable;
    }

    public bool CheckChairRotation(Chair m_Chair)
    {
        bool m_IsValid = false;
        if (Vector3.Dot(m_Chair.transform.up, Vector3.up) > 0.8f && m_Chair.transform.position.y < 0.3f)
        {
            m_IsValid = true;
        }
        return m_IsValid;
    }
}


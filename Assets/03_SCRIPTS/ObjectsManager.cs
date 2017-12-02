using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public List<GameObject> m_ChairList;
    public List<GameObject> m_ThrowableList;

    public GameObject FindNearestChair(GameObject m_Child)
    {
        GameObject NeareastChair = null;
        foreach (GameObject element in m_ChairList)
        {
            float lastDistance = 0f;
            float calculatedDistance = Vector3.Distance(element.transform.position, m_Child.transform.position);
            if (lastDistance > calculatedDistance)
            {
                lastDistance = calculatedDistance;
                NeareastChair = element;
            }
        }
        return NeareastChair;
    }

    public GameObject FindNearestThrowable(GameObject m_Child)
    {
        GameObject NearestThrowable = null;
        foreach (GameObject element in m_ThrowableList)
        {
            float lastDistance = 0f;
            float calculatedDistance = Vector3.Distance(element.transform.position, m_Child.transform.position);
            if (lastDistance > calculatedDistance)
            {
                lastDistance = calculatedDistance;
                NearestThrowable = element;
            }
        }
        return NearestThrowable;
    }
}


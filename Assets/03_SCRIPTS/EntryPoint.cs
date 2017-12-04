using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{

	public GameObject m_SpawnPoint;
	public GameObject m_WaitingPoint;
	public GameObject LeavePoint;
	public List<ChildBehaviour> m_ChildList;
	public GameObject[] m_ChildPrefab = null;
	public MayhemMeter m_MayhemMeter = null;
	public Vector2 SpawnTime = Vector2.one;
	public AnimationCurve spawnCurve;
	public float timeToMaxCurve;

	private float m_Timer = 0;
	private float reductionTimer = 0;
	private float m_NextSpawnTime = 0;
	public int m_SpawnIndex = 0;
	public float decreaseEverySec = 180f;
	public float reduceMan;

	public bool m_MustReload = false;

	private void Awake()
	{
		m_NextSpawnTime = Random.Range( SpawnTime.x, SpawnTime.y );
		if ( m_ChildPrefab == null )
		{
			Debug.LogWarning( "[EntryPoint] has no ChildPrefab." );
		}
	}

	// Update is called once per frame
	void Update()
	{
		//m_Timer += Time.deltaTime;

		if ( reductionTimer < Time.time )
		{
			reductionTimer = Time.time + spawnCurve.Evaluate( Time.time / timeToMaxCurve );
			GameObject obj = Instantiate( m_ChildPrefab[Random.Range( 0, m_ChildPrefab.Length )], m_SpawnPoint.transform.position, Quaternion.identity );
			m_ChildList.Add( obj.gameObject.GetComponent<ChildBehaviour>() );
			m_ChildList[m_SpawnIndex].m_CurrentWaitPoint = m_SpawnIndex;
			m_SpawnIndex++;
		}

		//if ( m_Timer >= m_NextSpawnTime )
		//{
		//	GameObject obj = Instantiate( m_ChildPrefab[Random.Range( 0, m_ChildPrefab.Length )], m_SpawnPoint.transform.position, Quaternion.identity );
		//	m_ChildList.Add( obj.gameObject.GetComponent<ChildBehaviour>() );
		//	m_ChildList[m_SpawnIndex].m_CurrentWaitPoint = m_SpawnIndex;
		//	m_SpawnIndex++;
		//	m_Timer = 0;
		//	m_NextSpawnTime = Random.Range( SpawnTime.x, SpawnTime.y );
		//}
	}

	public void ReloadChild()
	{
		m_MustReload = true;
	}
}

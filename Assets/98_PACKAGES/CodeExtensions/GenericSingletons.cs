using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T m_instance;

	public static T Instance
	{
		get
		{
			if ( m_instance == null )
			{
				m_instance = FindObjectOfType<T>();

				if ( m_instance == null )
				{
					GameObject singleton = new GameObject( typeof( T ).ToString() );
					GameObject.DontDestroyOnLoad( singleton );

					m_instance = singleton.AddComponent<T>();
				}
			}

			return m_instance;
		}
	}
}

public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
	private static T m_instance;

	public static T Instance
	{
		get
		{
			if ( m_instance == null )
			{
				m_instance = ScriptableObject.CreateInstance<T>();
			}

			return m_instance;
		}
	}
}
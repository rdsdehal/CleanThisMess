using System;
using System.Linq;
using UnityEngine;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// Extensions for the unity GameObject Type
	/// </summary>
	public static class GameObjectExtensions
	{
		/// <summary>
		/// Returns a bitfield of all layers that has collision with this GameObject.
		/// </summary>
		public static int GetCollisionMask( this GameObject gameObject )
		{
			int layer = gameObject.layer;
			int mask = 0;

			for ( int i = 0 ; i < 32 ; i++ ) mask |= ( Physics.GetIgnoreLayerCollision( layer, i ) ? 0 : 1 ) << i;

			return mask;
		}

		/// <summary>
		/// Gets the component T or creates one if none was found.
		/// </summary>
		public static T GetComponentOrAdd<T>( this GameObject gameObject ) where T : Component
		{
			T component = gameObject.GetComponent<T>();
			if ( component == null ) component = gameObject.AddComponent<T>();

			return component;
		}

		/// <summary>
		/// Returns all MonoBehaviours implementing the interface T (casted to T)
		/// </summary>
		/// <typeparam name="T">interface type</typeparam>
		public static T[] GetInterfaces<T>( this GameObject gObj )
		{
			if ( !typeof( T ).IsInterface ) throw new SystemException( "Specified type is not an interface!" );
			var mObjs = gObj.GetComponents<MonoBehaviour>();

			return ( from a in mObjs where a.GetType().GetInterfaces().Any( k => k == typeof( T ) ) select (T)(object)a ).ToArray();
		}

		/// <summary>
		/// Returns the first MonoBehaviour that is of the interface type (casted to T)
		/// </summary>
		/// <typeparam name="T">Interface type</typeparam>
		public static T GetInterface<T>( this GameObject gObj )
		{
			if ( !typeof( T ).IsInterface ) throw new SystemException( "Specified type is not an interface!" );
			return gObj.GetInterfaces<T>().FirstOrDefault();
		}

		/// <summary>
		/// Returns the first instance of the MonoBehaviour that is of the interface type T (casted to T)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="gObj"></param>
		/// <returns></returns>
		public static T GetInterfaceInChildren<T>( this GameObject gObj )
		{
			if ( !typeof( T ).IsInterface ) throw new SystemException( "Specified type is not an interface!" );
			return gObj.GetInterfacesInChildren<T>().FirstOrDefault();
		}

		/// <summary>
		/// Gets all MonoBehaviour in children that implement the interface of type T (casted to T)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="gObj"></param>
		/// <returns></returns>
		public static T[] GetInterfacesInChildren<T>( this GameObject gObj )
		{
			if ( !typeof( T ).IsInterface ) throw new SystemException( "Specified type is not an interface!" );

			var mObjs = gObj.GetComponentsInChildren<MonoBehaviour>();

			return ( from a in mObjs where a.GetType().GetInterfaces().Any( k => k == typeof( T ) ) select (T)(object)a ).ToArray();
		}
	}
}
using UnityEngine;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// Holds parent, local position, rotation and scale of a transform
	/// </summary>
	[System.Serializable]
	public struct TransformData
	{
		[SerializeField] public Vector3 localPosition;
		[SerializeField] public Quaternion localRotation;
		[SerializeField] public Vector3 localScale;
		[SerializeField] public Transform parent;
	}

	/// <summary>
	/// Extensions for the Unity Transform type.
	/// </summary>
	public static class TransformExtensions
	{
		#region TransformData
		/// <summary>
		/// Returns the transform information (Position, Rotation, Scale and Parent) of a given transfom.
		/// </summary>
		public static TransformData GetTransformData( this UnityEngine.Transform transform )
		{
			TransformData data = new TransformData()
			{
				parent = transform.parent,
				localPosition = transform.localPosition,
				localRotation = transform.localRotation,
				localScale = transform.localScale
			};

			return data;
		}

		/// <summary>
		/// Applies a TransformData obtained from GetTranformData to a transform.
		/// </summary>
		public static void ApplyTransformData( this Transform transform, TransformData data )
		{
			transform.parent = data.parent;
			transform.localPosition = data.localPosition;
			transform.localRotation = data.localRotation;
			transform.localScale = data.localScale;
		}
		#endregion

		#region Set Position Shorthands
		/// <summary>
		/// Shorthand to set the world X position of a transform.
		/// </summary>
		public static void SetXPos( this Transform transform, float newX )
		{
			Vector3 newPos = transform.position;
			newPos.x = newX;
			transform.position = newPos;
		}

		/// <summary>
		/// Shorthand to set the world Y position of a transform.
		/// </summary>
		public static void SetYPos( this Transform transform, float newY )
		{
			Vector3 newPos = transform.position;
			newPos.y = newY;
			transform.position = newPos;
		}

		/// <summary>
		/// Shorthand to set the world Z position of a transform.
		/// </summary>
		public static void SetZPos( this Transform transform, float newZ )
		{
			Vector3 newPos = transform.position;
			newPos.z = newZ;
			transform.position = newPos;
		}

		/// <summary>
		/// Shorthand to set the local X position of a transform.
		/// </summary>
		public static void SetLocalXPos( this Transform transform, float newX )
		{

			Vector3 newPos = transform.localPosition;
			newPos.x = newX;
			transform.localPosition = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Y position of a transform.
		/// </summary>
		public static void SetLocalYPos( this Transform transform, float newY )
		{

			Vector3 newPos = transform.localPosition;
			newPos.y = newY;
			transform.localPosition = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Z position of a transform.
		/// </summary>
		public static void SetLocalZPos( this Transform transform, float newZ )
		{

			Vector3 newPos = transform.localPosition;
			newPos.z = newZ;
			transform.localPosition = newPos;
		}
		#endregion

		#region Set Rotation Shorthands
		/// <summary>
		/// Shorthand to set the world X euler angle of a transform.
		/// </summary>
		public static void SetXRot( this Transform transform, float newX )
		{

			Vector3 newPos = transform.eulerAngles;
			newPos.x = newX;
			transform.eulerAngles = newPos;
		}

		/// <summary>
		/// Shorthand to set the world Y euler angle of a transform.
		/// </summary>
		public static void SetYRot( this Transform transform, float newY )
		{
			Vector3 newPos = transform.eulerAngles;
			newPos.y = newY;
			transform.eulerAngles = newPos;
		}

		/// <summary>
		/// Shorthand to set the world Z euler angle of a transform.
		/// </summary>
		public static void SetZRot( this Transform transform, float newZ )
		{
			Vector3 newPos = transform.eulerAngles;
			newPos.z = newZ;
			transform.eulerAngles = newPos;
		}

		/// <summary>
		/// Shorthand to set the local X euler angle of a transform.
		/// </summary>
		public static void SetLocalXRot( this Transform transform, float newX )
		{
			Vector3 newPos = transform.localEulerAngles;
			newPos.x = newX;
			transform.localEulerAngles = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Y euler angle of a transform.
		/// </summary>
		public static void SetLocalYRot( this Transform transform, float newY )
		{
			Vector3 newPos = transform.localEulerAngles;
			newPos.y = newY;
			transform.localEulerAngles = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Z euler angle of a transform.
		/// </summary>
		public static void SetLocalZRot( this Transform transform, float newZ )
		{
			Vector3 newPos = transform.localEulerAngles;
			newPos.z = newZ;
			transform.localEulerAngles = newPos;
		}
		#endregion

		#region Set Scale Shorthands
		/// <summary>
		/// Shorthand to set the local X scale of a transform.
		/// </summary>
		public static void SetLocaXlScl( this Transform transform, float newX )
		{
			Vector3 newPos = transform.localScale;
			newPos.x = newX;
			transform.localScale = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Y scale of a transform.
		/// </summary>
		public static void SetLocalYScl( this Transform transform, float newY )
		{
			Vector3 newPos = transform.localScale;
			newPos.y = newY;
			transform.localScale = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Z scale of a transform.
		/// </summary>
		public static void SetLocalZScl( this Transform transform, float newZ )
		{
			Vector3 newPos = transform.localScale;
			newPos.z = newZ;
			transform.localScale = newPos;
		}
		#endregion

		/// <summary>
		/// Returns the amount of parents to the scene root.
		/// </summary>
		public static int GetParentCount( this Transform t )
		{
			Transform parent = t;
			int i = 0;
			do
			{
				parent = parent.parent;
				i++;
			}
			while ( parent.parent != null );

			return i;
		}

		/// <summary>
		/// Gets the parent of the transform at a certain index.
		/// </summary>
		public static Transform GetParent( this Transform t, int indiceFromObject )
		{
			if ( indiceFromObject > t.GetParentCount() ) return null;

			Transform parent = t;

			for ( int i = 0 ; i < indiceFromObject ; i++ )
			{
				parent = parent.parent;
			}

			return parent;
		}

		/// <summary>
		/// Returns the total amount of children and sub-children of this Transform.
		/// </summary>
		public static int RecursiveChildCount( this UnityEngine.Transform t )
		{
			int amount = 1;

			foreach ( Transform child in t )
			{
				amount += child.RecursiveChildCount();
			}

			return amount;
		}

		/// <summary>
		/// Disables all children of this transform.
		/// </summary>
		public static void DisableAllChild( this Transform t )
		{
			for ( int i = 0 ; i < t.childCount ; i++ )
			{
				t.GetChild( i ).gameObject.SetActive( false );
			}
		}

		/// <summary>
		/// Enables all children of this transform.
		/// </summary>
		public static void EnableAllChild( this Transform t )
		{
			for ( int i = 0 ; i < t.childCount ; i++ )
			{
				t.GetChild( i ).gameObject.SetActive( true );
			}
		}

		/// <summary>
		/// Resets the position and rotation to 0, and the scale to 1.
		/// </summary>
		public static void Reset( this Transform t, bool resetScale = true )
		{
			t.position = Vector3.zero;
			t.localRotation = Quaternion.identity;
			if ( resetScale ) t.localScale = new Vector3( 1, 1, 1 );
		}
	}
}

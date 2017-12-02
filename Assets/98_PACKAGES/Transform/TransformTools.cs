using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bTools.CodeExtensions;

namespace bTools.TransformComponent
{
	public static class TransformTools
	{
		#region AlignTools
		/// <summary>
		/// Aligns toAlign Transforms to Target Transform according to alignOptions
		/// This operation is quite expensive as it needs to unparent and reparent GameObjects
		/// </summary>
		/// <param name="target">The Transform to align to</param>
		/// <param name="toAlign">The Transforms to align</param>
		/// <param name="alignOptions">Align options, in order: xPos, yPos, zPos, xRot, yRot, zRot, xScale, yScale, zScale</param>
		public static void AlignTransforms( UnityEngine.Transform target, UnityEngine.Transform[] toAlign, bool[] alignOptions )
		{
			if ( target == null )
			{
				throw new System.Exception( "AlignTransforms needs a target" );
			}

			if ( toAlign.Length <= 0 )
			{
				throw new System.Exception( "AlignTransforms toAlign array is empty" );
			}

			foreach ( UnityEngine.Transform t in toAlign )
			{
				Vector3 startPos = t.position;
				Vector3 startRot = t.eulerAngles;
				Vector3 startScl = t.localScale;

				UnityEngine.Transform cachedParent = t.parent;
				int cachedSiblingIndex = t.GetSiblingIndex();
				t.parent = null;


				if ( alignOptions[0] )
				{
					startPos.x = target.position.x;
				}
				if ( alignOptions[1] )
				{
					startPos.y = target.position.y;
				}
				if ( alignOptions[2] )
				{
					startPos.z = target.position.z;
				}
				if ( alignOptions[3] )
				{
					startRot.x = target.eulerAngles.x;
				}
				if ( alignOptions[4] )
				{
					startRot.y = target.eulerAngles.y;
				}
				if ( alignOptions[5] )
				{
					startRot.z = target.eulerAngles.z;
				}
				if ( alignOptions[6] )
				{
					startScl.x = target.lossyScale.x;
				}
				if ( alignOptions[7] )
				{
					startScl.y = target.lossyScale.y;
				}
				if ( alignOptions[8] )
				{
					startScl.z = target.lossyScale.z;
				}

				t.position = startPos;
				t.eulerAngles = startRot;
				t.localScale = startScl;

				t.parent = cachedParent;
				t.SetSiblingIndex( cachedSiblingIndex );
			}
		}

		/// <summary>
		/// Aligns toAlign Transform to Target Transform according to alignOptions
		/// This operation is quite expensive as it needs to unparent and reparent GameObjects
		/// </summary>
		/// <typeparam name="target">The Transform to align to</typeparam>
		/// <param name="toAlign">The Transform to align</param>
		/// <param name="alignOptions">Align options, in order: xPos, yPos, zPos, xRot, yRot, zRot, xScale, yScale, zScale</param>
		public static void AlignTransform( UnityEngine.Transform target, UnityEngine.Transform toAlign, bool[] alignOptions )
		{
			if ( target == null )
			{
				throw new System.Exception( "AlignTransform needs a target" );
			}

			if ( toAlign == null )
			{
				throw new System.Exception( "AlignTransforms toAlign is null" );
			}

			Vector3 targetCalcSize = target.GetComponent<Renderer>().bounds.size;

			Vector3 startPos = toAlign.position;
			Vector3 startRot = toAlign.eulerAngles;
			Vector3 startScl = toAlign.localScale;

			UnityEngine.Transform cachedParent = toAlign.parent;
			int cachedSiblingIndex = toAlign.GetSiblingIndex();
			toAlign.parent = null;

			if ( alignOptions[0] )
			{
				startPos.x = target.position.x;
			}
			if ( alignOptions[1] )
			{
				startPos.y = target.position.y;
			}
			if ( alignOptions[2] )
			{
				startPos.z = target.position.z;
			}
			if ( alignOptions[3] )
			{
				startRot.x = target.eulerAngles.x;
			}
			if ( alignOptions[4] )
			{
				startRot.y = target.eulerAngles.y;
			}
			if ( alignOptions[5] )
			{
				startRot.z = target.eulerAngles.z;
			}
			if ( alignOptions[6] )
			{
				startScl.x = target.lossyScale.x;
			}
			if ( alignOptions[7] )
			{
				startScl.y = target.lossyScale.y;
			}
			if ( alignOptions[8] )
			{
				startScl.z = target.lossyScale.z;
			}

			toAlign.position = startPos;
			toAlign.eulerAngles = startRot;
			toAlign.localScale = startScl;

			toAlign.parent = cachedParent;
			toAlign.SetSiblingIndex( cachedSiblingIndex );
		}
		#endregion

		#region DistributeTools
		/// <summary>
		/// Distributes transforms along the x axis between StartPosition and EndPosition
		/// </summary>
		/// <param name="startPosition">Start point of the distribution</param>
		/// <param name="endPosition">End point of the distribution</param>
		/// <param name="toDistribute">Array of transforms to distribute</param>
		public static void DistributeXPosition( Vector3 startPosition, Vector3 endPosition, UnityEngine.Transform[] toDistribute )
		{
			if ( toDistribute.Length <= 0 )
			{
				throw new System.Exception( "toDistribute array is empty" );
			}

			float xDistance = endPosition.x - startPosition.x;
			float xAdd = xDistance / ( toDistribute.Length + 1 );
			Vector3 currentPos = new Vector3();

			for ( int i = 0 ; i < toDistribute.Length ; i++ )
			{
				currentPos.x += xAdd;

				Vector3 cache = toDistribute[i].position;
				cache.x = startPosition.x;
				toDistribute[i].position = cache;

				toDistribute[i].position += currentPos;
			}
		}

		/// <summary>
		/// Distributes transforms along the y axis between StartPosition and EndPosition
		/// </summary>
		/// <param name="startPosition">Start point of the distribution</param>
		/// <param name="endPosition">End point of the distribution</param>
		/// <param name="toDistribute">Array of transforms to distribute</param>
		public static void DistributeYPosition( Vector3 startPosition, Vector3 endPosition, UnityEngine.Transform[] toDistribute )
		{
			if ( toDistribute.Length <= 0 )
			{
				throw new System.Exception( "toDistribute array is empty" );
			}

			float yDistance = endPosition.y - startPosition.y;
			float yAdd = yDistance / ( toDistribute.Length + 1 );
			Vector3 currentPos = new Vector3();

			for ( int i = 0 ; i < toDistribute.Length ; i++ )
			{
				currentPos.y += yAdd;

				Vector3 cache = toDistribute[i].position;
				cache.y = startPosition.y;
				toDistribute[i].position = cache;

				toDistribute[i].position += currentPos;
			}
		}

		/// <summary>
		/// Distributes transforms along the z axis between StartPosition and EndPosition
		/// </summary>
		/// <param name="startPosition">Start point of the distribution</param>
		/// <param name="endPosition">End point of the distribution</param>
		/// <param name="toDistribute">Array of transforms to distribute</param>
		public static void DistributeZPosition( Vector3 startPosition, Vector3 endPosition, UnityEngine.Transform[] toDistribute )
		{
			if ( toDistribute.Length <= 0 )
			{
				throw new System.Exception( "toDistribute array is empty" );
			}

			float zDistance = endPosition.z - startPosition.z;
			float zAdd = zDistance / ( toDistribute.Length + 1 );
			Vector3 currentPos = new Vector3();

			for ( int i = 0 ; i < toDistribute.Length ; i++ )
			{
				currentPos.z += zAdd;

				Vector3 cache = toDistribute[i].position;
				cache.z = startPosition.z;
				toDistribute[i].position = cache;

				toDistribute[i].position += currentPos;
			}
		}

		/// <summary>
		/// Distributes transforms between StartPosition and EndPosition
		/// </summary>
		/// <param name="startPosition">Start point of the distribution</param>
		/// <param name="endPosition">End point of the distribution</param>
		/// <param name="toDistribute">Array of transforms to distribute</param>
		public static void DistributePosition( Vector3 startPosition, Vector3 endPosition, UnityEngine.Transform[] toDistribute )
		{
			DistributeZPosition( startPosition, endPosition, toDistribute );
			DistributeYPosition( startPosition, endPosition, toDistribute );
			DistributeXPosition( startPosition, endPosition, toDistribute );
		}

		/// <summary>
		/// Distributes the given transform rotations between two quaternions.
		/// </summary>
		/// <param name="startRot">Start point of the distribution</param>
		/// <param name="endRot">End point of the distribution</param>
		/// <param name="toDistribute">Array of transforms to distribute</param>
		public static void DistributeRotation( Quaternion startRot, Quaternion endRot, UnityEngine.Transform[] toDistribute )
		{
			if ( toDistribute.Length <= 0 )
			{
				throw new System.Exception( "toDistribute array is empty" );
			}

			float lerpAdd = 1.0f / toDistribute.Length;
			float currentLerp = lerpAdd;

			for ( int i = 0 ; i < toDistribute.Length ; i++ )
			{
				toDistribute[i].rotation = Quaternion.Slerp( startRot, endRot, currentLerp );
				currentLerp += lerpAdd;
			}
		}

		/// <summary>
		/// Distributes local x scale between startScale and endScale
		/// </summary>
		/// <param name="startPosition">Lowest scale</param>
		/// <param name="endPosition">Highest scale</param>
		/// <param name="toDistribute">Array of transforms to distribute</param>
		public static void DistributeXScale( float startScale, float endScale, UnityEngine.Transform[] toDistribute )
		{
			if ( toDistribute.Length <= 0 )
			{
				throw new System.Exception( "toDistribute array is empty" );
			}

			float distance = endScale - startScale;
			float add = distance / ( toDistribute.Length + 1 );
			float currentScale = 0;

			for ( int i = 0 ; i < toDistribute.Length ; i++ )
			{
				currentScale += add;

				Vector3 cache = toDistribute[i].localScale;
				cache.x = startScale + currentScale;
				toDistribute[i].localScale = cache;
			}
		}

		/// <summary>
		/// Distributes local y scale between startScale and endScale
		/// </summary>
		/// <param name="startPosition">Lowest scale</param>
		/// <param name="endPosition">Highest scale</param>
		/// <param name="toDistribute">Array of transforms to distribute</param>
		public static void DistributeYScale( float startScale, float endScale, UnityEngine.Transform[] toDistribute )
		{
			if ( toDistribute.Length <= 0 )
			{
				throw new System.Exception( "toDistribute array is empty" );
			}

			float distance = endScale - startScale;
			float add = distance / ( toDistribute.Length + 1 );
			float currentScale = 0;

			for ( int i = 0 ; i < toDistribute.Length ; i++ )
			{
				currentScale += add;

				Vector3 cache = toDistribute[i].localScale;
				cache.y = startScale + currentScale;
				toDistribute[i].localScale = cache;
			}
		}

		/// <summary>
		/// Distributes local z scale between startScale and endScale
		/// </summary>
		/// <param name="startPosition">Lowest scale</param>
		/// <param name="endPosition">Highest scale</param>
		/// <param name="toDistribute">Array of transforms to distribute</param>
		public static void DistributeZScale( float startScale, float endScale, UnityEngine.Transform[] toDistribute )
		{
			if ( toDistribute.Length <= 0 )
			{
				throw new System.Exception( "toDistribute array is empty" );
			}

			float distance = endScale - startScale;
			float add = distance / ( toDistribute.Length + 1 );
			float currentScale = 0;

			for ( int i = 0 ; i < toDistribute.Length ; i++ )
			{
				currentScale += add;

				Vector3 cache = toDistribute[i].localScale;
				cache.z = startScale + currentScale;
				toDistribute[i].localScale = cache;
			}
		}

		/// <summary>
		/// Distributes local scale between startScale and endScale
		/// </summary>
		/// <param name="startPosition">Lowest scale</param>
		/// <param name="endPosition">Highest scale</param>
		/// <param name="toDistribute">Array of transforms to distribute</param>
		public static void DistributeScale( Vector3 startScale, Vector3 endScale, UnityEngine.Transform[] toDistribute )
		{
			DistributeXScale( startScale.x, endScale.x, toDistribute );
			DistributeYScale( startScale.y, endScale.y, toDistribute );
			DistributeZScale( startScale.z, endScale.z, toDistribute );
		}
		#endregion

		#region NoiseTools
		public enum NoiseMode
		{
			Absolute,
			Additive
		}

		/// <summary>
		/// Adds noise to the local X position of the specified Transforms
		/// </summary>
		public static void AddXPosNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			if ( toNoisify.Length <= 0 )
			{
				throw new System.Exception( "Noisify array is empty" );
			}

			float amount;

			foreach ( UnityEngine.Transform t in toNoisify )
			{
				amount = Random.Range( min, max );

				switch ( noiseMode )
				{
					case NoiseMode.Absolute:

						t.SetLocalXPos( amount );
						break;
					case NoiseMode.Additive:

						t.SetLocalXPos( t.localPosition.x + amount );
						break;
					default:
						throw new System.Exception( "Add Noise got a wrong NoiseMode type" );
				}
			}
		}
		/// <summary>
		/// Adds noise to the local Y position of the specified Transforms
		/// </summary>
		public static void AddYPosNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			if ( toNoisify.Length <= 0 )
			{
				throw new System.Exception( "Noisify array is empty" );
			}

			float amount;

			foreach ( UnityEngine.Transform t in toNoisify )
			{
				amount = Random.Range( min, max );

				switch ( noiseMode )
				{
					case NoiseMode.Absolute:

						t.SetLocalYPos( amount );
						break;
					case NoiseMode.Additive:

						t.SetLocalYPos( t.localPosition.y + amount );
						break;
					default:
						throw new System.Exception( "Add Noise got a wrong NoiseMode type" );
				}
			}
		}
		/// <summary>
		/// Adds noise to the local Z position of the specified Transforms
		/// </summary>
		public static void AddZPosNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			if ( toNoisify.Length <= 0 )
			{
				throw new System.Exception( "Noisify array is empty" );
			}

			float amount;

			foreach ( UnityEngine.Transform t in toNoisify )
			{
				amount = Random.Range( min, max );

				switch ( noiseMode )
				{
					case NoiseMode.Absolute:

						t.SetLocalZPos( amount );
						break;
					case NoiseMode.Additive:

						t.SetLocalZPos( t.localPosition.z + amount );
						break;
					default:
						throw new System.Exception( "Add Noise got a wrong NoiseMode type" );
				}
			}
		}
		/// <summary>
		/// Adds noise to the local position of the specified Transforms
		/// </summary>
		public static void AddPosNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			AddXPosNoise( min, max, toNoisify, noiseMode );
			AddYPosNoise( min, max, toNoisify, noiseMode );
			AddZPosNoise( min, max, toNoisify, noiseMode );
		}

		/// <summary>
		/// Adds noise to the local X euler angle of the specified Transforms
		/// </summary>
		public static void AddXRotNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			if ( toNoisify.Length <= 0 )
			{
				throw new System.Exception( "Noisify array is empty" );
			}

			float amount;

			foreach ( UnityEngine.Transform t in toNoisify )
			{
				amount = Random.Range( min, max );

				switch ( noiseMode )
				{
					case NoiseMode.Absolute:

						t.SetLocalXRot( amount );
						break;
					case NoiseMode.Additive:

						t.SetLocalXRot( t.localEulerAngles.x + amount );
						break;
					default:
						break;
				}
			}
		}
		/// <summary>
		/// Adds noise to the local Y euler angle of the specified Transforms
		/// </summary>
		public static void AddYRotNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			if ( toNoisify.Length <= 0 )
			{
				throw new System.Exception( "Noisify array is empty" );
			}

			float amount;

			foreach ( UnityEngine.Transform t in toNoisify )
			{
				amount = Random.Range( min, max );

				switch ( noiseMode )
				{
					case NoiseMode.Absolute:

						t.SetLocalYRot( amount );
						break;
					case NoiseMode.Additive:

						t.SetLocalYRot( t.localEulerAngles.y + amount );
						break;
					default:
						break;
				}
			}
		}
		/// <summary>
		/// Adds noise to the local Z euler angle of the specified Transforms
		/// </summary>
		public static void AddZRotNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			if ( toNoisify.Length <= 0 )
			{
				throw new System.Exception( "Noisify array is empty" );
			}

			float amount;

			foreach ( UnityEngine.Transform t in toNoisify )
			{
				amount = Random.Range( min, max );

				switch ( noiseMode )
				{
					case NoiseMode.Absolute:

						t.SetLocalZRot( amount );
						break;
					case NoiseMode.Additive:

						t.SetLocalZRot( t.localEulerAngles.z + amount );
						break;
					default:
						break;
				}
			}
		}
		/// <summary>
		/// Adds noise to the local euler angles of the specified Transforms
		/// </summary>
		public static void AddRotNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			AddXRotNoise( min, max, toNoisify, noiseMode );
			AddYRotNoise( min, max, toNoisify, noiseMode );
			AddZRotNoise( min, max, toNoisify, noiseMode );
		}

		/// <summary>
		/// Adds noise to the local X scale of the specified Transforms
		/// </summary>
		public static void AddXSclNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			if ( toNoisify.Length <= 0 )
			{
				throw new System.Exception( "Noisify array is empty" );
			}

			float amount;

			foreach ( UnityEngine.Transform t in toNoisify )
			{
				amount = Random.Range( min, max );

				switch ( noiseMode )
				{
					case NoiseMode.Absolute:

						t.SetLocaXlScl( amount );
						break;
					case NoiseMode.Additive:

						t.SetLocaXlScl( t.localScale.x + amount );
						break;
					default:
						break;
				}
			}
		}
		/// <summary>
		/// Adds noise to the local Y scale of the specified Transforms
		/// </summary>
		public static void AddYSclNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			if ( toNoisify.Length <= 0 )
			{
				throw new System.Exception( "Noisify array is empty" );
			}

			float amount;

			foreach ( UnityEngine.Transform t in toNoisify )
			{
				amount = Random.Range( min, max );

				switch ( noiseMode )
				{
					case NoiseMode.Absolute:

						t.SetLocalYScl( amount );
						break;
					case NoiseMode.Additive:

						t.SetLocalYScl( t.localScale.y + amount );
						break;
					default:
						break;
				}
			}
		}
		/// <summary>
		/// Adds noise to the local Z scale of the specified Transforms
		/// </summary>
		public static void AddZSclNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			if ( toNoisify.Length <= 0 )
			{
				throw new System.Exception( "Noisify array is empty" );
			}

			float amount;

			foreach ( UnityEngine.Transform t in toNoisify )
			{
				amount = Random.Range( min, max );

				switch ( noiseMode )
				{
					case NoiseMode.Absolute:

						t.SetLocalZScl( amount );
						break;
					case NoiseMode.Additive:

						t.SetLocalZScl( t.localScale.z + amount );
						break;
					default:
						break;
				}
			}
		}
		/// <summary>
		/// Adds noise to the local scale of the specified Transforms
		/// </summary>
		public static void AddSclNoise( float min, float max, UnityEngine.Transform[] toNoisify, NoiseMode noiseMode = NoiseMode.Additive )
		{
			AddXSclNoise( min, max, toNoisify, noiseMode );
			AddYSclNoise( min, max, toNoisify, noiseMode );
			AddZSclNoise( min, max, toNoisify, noiseMode );
		}

		#endregion
	}
}

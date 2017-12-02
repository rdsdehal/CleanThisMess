using System;
using UnityEngine;
using UnityEngine.Internal;

namespace bTools
{
	/// <summary>
	/// Base class to derive from when creating new Settings assets.
	/// </summary>
	[Serializable]
	public abstract class ToolsSettingsBase : ScriptableObject
	{
		/// <summary>
		/// <p>How this module should be ordered according to to others in the settings window.</p>
		/// </summary>
		[SerializeField]
		public int order { get; protected set; }

		/// <summary>
		/// <p>The name of the module this settings wrapper describes, it needs to be modfied in the constructor of your derived class.</p>
		/// <p> Used by the settings window as tab name. </p>
		/// </summary>
		[SerializeField]
		public string moduleName { get; protected set; }

		/// <summary>
		/// <p>List of methods that draw subgroups of the settings asset, initialise it with at least one method for it to work properly. </p>
		/// <p>The settings window uses the name of the method as the tab name. </p> 
		/// </summary>
		[SerializeField] public Action[] subCategories { get; protected set; }

		[ExcludeFromDocs]
		public delegate void SettingsChangedCallback();

		/// <summary>
		/// Called when the user changes something in the Settings window for this asset.
		/// </summary>
		public SettingsChangedCallback OnSettingsChanged;
	}
}
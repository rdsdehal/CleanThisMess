using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class GlowingOutlineRenderer : MonoBehaviour
{
	[Range( 0, 10 )]
	public float Intensity = 2;
	public List<GlowingObject> glowingObjects;
	public Material compositeMat;

	private CommandBuffer _commandBuffer;
	private Material _glowMat;
	private Material _blurMaterial;
	private Vector2 _blurTexelSize;
	private int _glowColorID;
	private int _prePassRenderTexID;
	private int _blurPassRenderTexID;
	private int _tempRenderTexID;
	private int _blurSizeID;

	private void Awake()
	{
		_glowMat = new Material( Shader.Find( "Hidden/GlowCmdShader" ) );
		_blurMaterial = new Material( Shader.Find( "Hidden/Blur" ) );

		_prePassRenderTexID = Shader.PropertyToID( "_GlowPrePassTex" );
		_blurPassRenderTexID = Shader.PropertyToID( "_GlowBlurredTex" );
		_tempRenderTexID = Shader.PropertyToID( "_TempTex0" );
		_blurSizeID = Shader.PropertyToID( "_BlurSize" );
		_glowColorID = Shader.PropertyToID( "_GlowColor" );

		_commandBuffer = new CommandBuffer();
		_commandBuffer.name = "Glowing Objects Buffer"; // This name is visible in the Frame Debugger, so make it a descriptive!
		GetComponent<Camera>().AddCommandBuffer( CameraEvent.BeforeImageEffects, _commandBuffer );
	}

	private void Update()
	{
		_commandBuffer.Clear();

		_commandBuffer.GetTemporaryRT( _prePassRenderTexID, Screen.width, Screen.height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, QualitySettings.antiAliasing );
		_commandBuffer.SetRenderTarget( _prePassRenderTexID );
		_commandBuffer.ClearRenderTarget( true, true, Color.clear );

		for ( int i = 0 ; i < glowingObjects.Count ; i++ )
		{
			_commandBuffer.SetGlobalColor( _glowColorID, glowingObjects[i].outlineColor );

			for ( int j = 0 ; j < glowingObjects[i].renderers.Length ; j++ )
			{
				_commandBuffer.DrawRenderer( glowingObjects[i].renderers[j], _glowMat );
			}
		}

		_commandBuffer.GetTemporaryRT( _blurPassRenderTexID, Screen.width >> 1, Screen.height >> 1, 0, FilterMode.Bilinear );
		_commandBuffer.GetTemporaryRT( _tempRenderTexID, Screen.width >> 1, Screen.height >> 1, 0, FilterMode.Bilinear );
		_commandBuffer.Blit( _prePassRenderTexID, _blurPassRenderTexID );

		_blurTexelSize = new Vector2( 1.5f / ( Screen.width >> 1 ), 1.5f / ( Screen.height >> 1 ) );
		_commandBuffer.SetGlobalVector( _blurSizeID, _blurTexelSize );

		for ( int i = 0 ; i < 2 ; i++ )
		{
			_commandBuffer.Blit( _blurPassRenderTexID, _tempRenderTexID, _blurMaterial, 0 );
			_commandBuffer.Blit( _tempRenderTexID, _blurPassRenderTexID, _blurMaterial, 1 );
		}
	}

	void OnRenderImage( RenderTexture src, RenderTexture dst )
	{
		compositeMat.SetFloat( "_Intensity", Intensity );
		Graphics.Blit( src, dst, compositeMat, 0 );
	}
}

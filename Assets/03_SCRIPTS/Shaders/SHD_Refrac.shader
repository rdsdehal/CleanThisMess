// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|alpha-3955-OUT,refract-7684-OUT;n:type:ShaderForge.SFN_Tex2d,id:1215,x:30944,y:32812,ptovrint:False,ptlb:node_1215,ptin:_node_1215,varname:node_1215,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b3c4e827085e2c9469d9a32c954ba8fd,ntxv:3,isnm:True|UVIN-2683-UVOUT;n:type:ShaderForge.SFN_Multiply,id:7684,x:31945,y:33068,varname:node_7684,prsc:2|A-2178-OUT,B-9411-OUT;n:type:ShaderForge.SFN_Slider,id:9411,x:31564,y:33239,ptovrint:False,ptlb:node_9411,ptin:_node_9411,varname:node_9411,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:4901,x:30944,y:33038,ptovrint:False,ptlb:node_4901,ptin:_node_4901,varname:node_4901,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True|UVIN-1202-UVOUT;n:type:ShaderForge.SFN_NormalBlend,id:7065,x:31227,y:32947,varname:node_7065,prsc:2|BSE-1215-RGB,DTL-4901-RGB;n:type:ShaderForge.SFN_ComponentMask,id:2178,x:31454,y:32947,varname:node_2178,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-7065-OUT;n:type:ShaderForge.SFN_Time,id:722,x:29688,y:32713,varname:node_722,prsc:2;n:type:ShaderForge.SFN_Slider,id:838,x:29531,y:32876,ptovrint:False,ptlb:node_838,ptin:_node_838,varname:node_838,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.1;n:type:ShaderForge.SFN_TexCoord,id:9241,x:29688,y:32550,varname:node_9241,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:3527,x:29880,y:32750,varname:node_3527,prsc:2|A-722-T,B-838-OUT;n:type:ShaderForge.SFN_OneMinus,id:5510,x:30072,y:32750,varname:node_5510,prsc:2|IN-3527-OUT;n:type:ShaderForge.SFN_Panner,id:2683,x:30319,y:32642,varname:node_2683,prsc:2,spu:-1,spv:0|UVIN-9241-UVOUT,DIST-5510-OUT;n:type:ShaderForge.SFN_Panner,id:1202,x:30472,y:33447,varname:node_1202,prsc:2,spu:-1,spv:0|UVIN-2808-UVOUT,DIST-9187-OUT;n:type:ShaderForge.SFN_OneMinus,id:9187,x:30226,y:33554,varname:node_9187,prsc:2|IN-3238-OUT;n:type:ShaderForge.SFN_Multiply,id:3238,x:30034,y:33554,varname:node_3238,prsc:2|A-5156-T,B-8585-OUT;n:type:ShaderForge.SFN_Time,id:5156,x:29842,y:33518,varname:node_5156,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:2808,x:29842,y:33354,varname:node_2808,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:8585,x:29674,y:33696,ptovrint:False,ptlb:node_8585,ptin:_node_8585,varname:node_8585,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:3955,x:32316,y:32885,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_3955,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:1215-9411-4901-838-8585-3955;pass:END;sub:END;*/

Shader "Shader Forge/SHD_Refrac" {
    Properties {
        _node_1215 ("node_1215", 2D) = "bump" {}
        _node_9411 ("node_9411", Range(0, 1)) = 0
        _node_4901 ("node_4901", 2D) = "bump" {}
        _node_838 ("node_838", Range(0, 0.1)) = 0
        _node_8585 ("node_8585", Range(0, 1)) = 0
        _Opacity ("Opacity", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _node_1215; uniform float4 _node_1215_ST;
            uniform float _node_9411;
            uniform sampler2D _node_4901; uniform float4 _node_4901_ST;
            uniform float _node_838;
            uniform float _node_8585;
            uniform float _Opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_722 = _Time;
                float2 node_2683 = (i.uv0+(1.0 - (node_722.g*_node_838))*float2(-1,0));
                float3 _node_1215_var = UnpackNormal(tex2D(_node_1215,TRANSFORM_TEX(node_2683, _node_1215)));
                float4 node_5156 = _Time;
                float2 node_1202 = (i.uv0+(1.0 - (node_5156.g*_node_8585))*float2(-1,0));
                float3 _node_4901_var = UnpackNormal(tex2D(_node_4901,TRANSFORM_TEX(node_1202, _node_4901)));
                float3 node_7065_nrm_base = _node_1215_var.rgb + float3(0,0,1);
                float3 node_7065_nrm_detail = _node_4901_var.rgb * float3(-1,-1,1);
                float3 node_7065_nrm_combined = node_7065_nrm_base*dot(node_7065_nrm_base, node_7065_nrm_detail)/node_7065_nrm_base.z - node_7065_nrm_detail;
                float3 node_7065 = node_7065_nrm_combined;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (node_7065.rg*_node_9411);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,_Opacity),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

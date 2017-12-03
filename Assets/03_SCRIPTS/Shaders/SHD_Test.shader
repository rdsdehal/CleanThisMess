// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-4655-R,alpha-4655-R,voffset-1360-OUT;n:type:ShaderForge.SFN_NormalVector,id:772,x:32100,y:33033,prsc:2,pt:False;n:type:ShaderForge.SFN_Tex2d,id:4655,x:32100,y:32802,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_4655,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e8c7d2734a69b9d44ae4380461b97773,ntxv:0,isnm:False|UVIN-6579-UVOUT;n:type:ShaderForge.SFN_Multiply,id:1360,x:32413,y:33045,varname:node_1360,prsc:2|A-772-OUT,B-4655-R,C-9078-OUT;n:type:ShaderForge.SFN_Slider,id:9078,x:31965,y:33365,ptovrint:False,ptlb:node_9078,ptin:_node_9078,varname:node_9078,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Time,id:2414,x:31149,y:33109,varname:node_2414,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:3449,x:31161,y:32946,varname:node_3449,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:5179,x:31021,y:33283,ptovrint:False,ptlb:node_5179,ptin:_node_5179,varname:node_5179,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:5104,x:31367,y:33164,varname:node_5104,prsc:2|A-2414-T,B-5179-OUT;n:type:ShaderForge.SFN_OneMinus,id:1528,x:31551,y:33164,varname:node_1528,prsc:2|IN-5104-OUT;n:type:ShaderForge.SFN_Panner,id:6579,x:31733,y:33035,varname:node_6579,prsc:2,spu:1,spv:0|UVIN-3449-UVOUT,DIST-1528-OUT;proporder:9078-5179-4655;pass:END;sub:END;*/

Shader "Shader Forge/SHD_Test" {
    Properties {
        _node_9078 ("node_9078", Range(0, 10)) = 0
        _node_5179 ("node_5179", Range(0, 1)) = 0
        _Noise ("Noise", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
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
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _node_9078;
            uniform float _node_5179;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_2414 = _Time;
                float2 node_6579 = (o.uv0+(1.0 - (node_2414.g*_node_5179))*float2(1,0));
                float4 _Noise_var = tex2Dlod(_Noise,float4(TRANSFORM_TEX(node_6579, _Noise),0.0,0));
                v.vertex.xyz += (v.normal*_Noise_var.r*_node_9078);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_2414 = _Time;
                float2 node_6579 = (i.uv0+(1.0 - (node_2414.g*_node_5179))*float2(1,0));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_6579, _Noise));
                float3 emissive = float3(_Noise_var.r,_Noise_var.r,_Noise_var.r);
                float3 finalColor = emissive;
                return fixed4(finalColor,_Noise_var.r);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _node_9078;
            uniform float _node_5179;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_2414 = _Time;
                float2 node_6579 = (o.uv0+(1.0 - (node_2414.g*_node_5179))*float2(1,0));
                float4 _Noise_var = tex2Dlod(_Noise,float4(TRANSFORM_TEX(node_6579, _Noise),0.0,0));
                v.vertex.xyz += (v.normal*_Noise_var.r*_node_9078);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

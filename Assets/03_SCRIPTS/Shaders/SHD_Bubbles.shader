// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-3424-OUT,voffset-461-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32147,y:32562,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_NormalVector,id:3698,x:32147,y:33081,prsc:2,pt:False;n:type:ShaderForge.SFN_Tex2d,id:3474,x:32147,y:32911,ptovrint:False,ptlb:Displacement Noise,ptin:_DisplacementNoise,varname:node_3474,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6e087528f2b462e4da9649f35b6816f1,ntxv:0,isnm:False|UVIN-3737-UVOUT;n:type:ShaderForge.SFN_Multiply,id:461,x:32474,y:33024,varname:node_461,prsc:2|A-3474-R,B-3698-OUT,C-8710-OUT;n:type:ShaderForge.SFN_Slider,id:8710,x:31886,y:33321,ptovrint:False,ptlb:Displacement,ptin:_Displacement,varname:node_8710,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.1;n:type:ShaderForge.SFN_Time,id:8876,x:31174,y:32786,varname:node_8876,prsc:2;n:type:ShaderForge.SFN_Slider,id:5047,x:31017,y:32930,ptovrint:False,ptlb:Displacement Pan,ptin:_DisplacementPan,varname:node_5047,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_TexCoord,id:2928,x:31174,y:32635,varname:node_2928,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2349,x:31356,y:32845,varname:node_2349,prsc:2|A-8876-T,B-5047-OUT;n:type:ShaderForge.SFN_OneMinus,id:9091,x:31516,y:32845,varname:node_9091,prsc:2|IN-2349-OUT;n:type:ShaderForge.SFN_Panner,id:3737,x:31685,y:32809,varname:node_3737,prsc:2,spu:1,spv:0|UVIN-2928-UVOUT,DIST-9091-OUT;n:type:ShaderForge.SFN_Multiply,id:3424,x:32406,y:32690,varname:node_3424,prsc:2|A-7241-RGB,B-1703-R;n:type:ShaderForge.SFN_Tex2d,id:1703,x:32147,y:32730,ptovrint:False,ptlb:Color Variant,ptin:_ColorVariant,varname:node_1703,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a22c12960ca7e414aa3b139db75c8ce1,ntxv:0,isnm:False;proporder:7241-3474-8710-5047-1703;pass:END;sub:END;*/

Shader "Shader Forge/SHD_Bubbles" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _DisplacementNoise ("Displacement Noise", 2D) = "white" {}
        _Displacement ("Displacement", Range(0, 0.1)) = 0
        _DisplacementPan ("Displacement Pan", Range(0, 1)) = 0
        _ColorVariant ("Color Variant", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _DisplacementNoise; uniform float4 _DisplacementNoise_ST;
            uniform float _Displacement;
            uniform float _DisplacementPan;
            uniform sampler2D _ColorVariant; uniform float4 _ColorVariant_ST;
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
                float4 node_8876 = _Time;
                float2 node_3737 = (o.uv0+(1.0 - (node_8876.g*_DisplacementPan))*float2(1,0));
                float4 _DisplacementNoise_var = tex2Dlod(_DisplacementNoise,float4(TRANSFORM_TEX(node_3737, _DisplacementNoise),0.0,0));
                v.vertex.xyz += (_DisplacementNoise_var.r*v.normal*_Displacement);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _ColorVariant_var = tex2D(_ColorVariant,TRANSFORM_TEX(i.uv0, _ColorVariant));
                float3 emissive = (_Color.rgb*_ColorVariant_var.r);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
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
            uniform sampler2D _DisplacementNoise; uniform float4 _DisplacementNoise_ST;
            uniform float _Displacement;
            uniform float _DisplacementPan;
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
                float4 node_8876 = _Time;
                float2 node_3737 = (o.uv0+(1.0 - (node_8876.g*_DisplacementPan))*float2(1,0));
                float4 _DisplacementNoise_var = tex2Dlod(_DisplacementNoise,float4(TRANSFORM_TEX(node_3737, _DisplacementNoise),0.0,0));
                v.vertex.xyz += (_DisplacementNoise_var.r*v.normal*_Displacement);
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

// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32988,y:32720,varname:node_3138,prsc:2|emission-1502-OUT,alpha-1724-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31797,y:32521,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Multiply,id:2071,x:32296,y:32594,varname:node_2071,prsc:2|A-7241-RGB,B-2087-OUT;n:type:ShaderForge.SFN_Slider,id:2087,x:31767,y:32736,ptovrint:False,ptlb:Emissive,ptin:_Emissive,varname:node_2087,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:5;n:type:ShaderForge.SFN_Time,id:8656,x:31893,y:32181,varname:node_8656,prsc:2;n:type:ShaderForge.SFN_Slider,id:7280,x:31736,y:32099,ptovrint:False,ptlb:Pan Speed,ptin:_PanSpeed,varname:node_7280,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_TexCoord,id:360,x:31893,y:31928,varname:node_360,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:1473,x:32435,y:32089,varname:node_1473,prsc:2,spu:1,spv:0|UVIN-360-UVOUT,DIST-6590-OUT;n:type:ShaderForge.SFN_OneMinus,id:6590,x:32271,y:32141,varname:node_6590,prsc:2|IN-2469-OUT;n:type:ShaderForge.SFN_Multiply,id:2469,x:32094,y:32141,varname:node_2469,prsc:2|A-7280-OUT,B-8656-T;n:type:ShaderForge.SFN_Tex2d,id:4306,x:32594,y:32236,ptovrint:False,ptlb:node_4306,ptin:_node_4306,varname:node_4306,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dd7fc7ecefe25a945a83f0e558423983,ntxv:0,isnm:False|UVIN-1473-UVOUT;n:type:ShaderForge.SFN_Blend,id:1502,x:32518,y:32505,varname:node_1502,prsc:2,blmd:10,clmp:True|SRC-4306-RGB,DST-2071-OUT;n:type:ShaderForge.SFN_Slider,id:6176,x:31719,y:33103,ptovrint:False,ptlb:node_6176,ptin:_node_6176,varname:node_6176,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Rotator,id:6352,x:32435,y:31930,varname:node_6352,prsc:2|UVIN-360-UVOUT;n:type:ShaderForge.SFN_Fresnel,id:1724,x:32195,y:32934,varname:node_1724,prsc:2|NRM-4932-OUT,EXP-6176-OUT;n:type:ShaderForge.SFN_NormalVector,id:4932,x:31887,y:32934,prsc:2,pt:False;proporder:7241-2087-7280-4306-6176;pass:END;sub:END;*/

Shader "Shader Forge/SHD_Hologram" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _Emissive ("Emissive", Range(0, 5)) = 1
        _PanSpeed ("Pan Speed", Range(0, 1)) = 0
        _node_4306 ("node_4306", 2D) = "white" {}
        _node_6176 ("node_6176", Range(0, 10)) = 1
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
            uniform float4 _Color;
            uniform float _Emissive;
            uniform float _PanSpeed;
            uniform sampler2D _node_4306; uniform float4 _node_4306_ST;
            uniform float _node_6176;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_8656 = _Time;
                float2 node_1473 = (i.uv0+(1.0 - (_PanSpeed*node_8656.g))*float2(1,0));
                float4 _node_4306_var = tex2D(_node_4306,TRANSFORM_TEX(node_1473, _node_4306));
                float3 emissive = saturate(( (_Color.rgb*_Emissive) > 0.5 ? (1.0-(1.0-2.0*((_Color.rgb*_Emissive)-0.5))*(1.0-_node_4306_var.rgb)) : (2.0*(_Color.rgb*_Emissive)*_node_4306_var.rgb) ));
                float3 finalColor = emissive;
                return fixed4(finalColor,pow(1.0-max(0,dot(i.normalDir, viewDirection)),_node_6176));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

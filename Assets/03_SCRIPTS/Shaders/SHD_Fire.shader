// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-4052-RGB,clip-9949-OUT;n:type:ShaderForge.SFN_Tex2d,id:4052,x:32318,y:32577,varname:node_4052,prsc:2,tex:271f5ee3273dd7f4fae6e204d4f8c4bf,ntxv:0,isnm:False|UVIN-263-OUT,TEX-9218-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:9218,x:31983,y:32763,ptovrint:False,ptlb:Fire Ramp,ptin:_FireRamp,varname:node_9218,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:271f5ee3273dd7f4fae6e204d4f8c4bf,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:218,x:31222,y:32319,ptovrint:False,ptlb:Fire Noise,ptin:_FireNoise,varname:node_218,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:337d435099cc11346b698507cb2f577b,ntxv:0,isnm:False|UVIN-6631-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3269,x:31268,y:32733,ptovrint:False,ptlb:Fire Mask,ptin:_FireMask,varname:node_3269,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cfe3a6bac29b139438182fc8f1e65538,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:3156,x:30659,y:32717,varname:node_3156,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:1121,x:30378,y:32489,varname:node_1121,prsc:2;n:type:ShaderForge.SFN_Panner,id:6631,x:30980,y:32241,varname:node_6631,prsc:2,spu:0,spv:1|UVIN-3649-UVOUT,DIST-4049-OUT;n:type:ShaderForge.SFN_TexCoord,id:3649,x:30630,y:32196,varname:node_3649,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:4961,x:30285,y:32399,ptovrint:False,ptlb:Fire pan speed,ptin:_Firepanspeed,varname:node_4961,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:3;n:type:ShaderForge.SFN_Multiply,id:4948,x:30630,y:32404,varname:node_4948,prsc:2|A-4961-OUT,B-1121-T;n:type:ShaderForge.SFN_OneMinus,id:4049,x:30792,y:32404,varname:node_4049,prsc:2|IN-4948-OUT;n:type:ShaderForge.SFN_OneMinus,id:9949,x:32025,y:32600,varname:node_9949,prsc:2|IN-6941-OUT;n:type:ShaderForge.SFN_Multiply,id:2159,x:30944,y:32772,varname:node_2159,prsc:2|A-3156-V,B-2414-OUT;n:type:ShaderForge.SFN_Slider,id:2414,x:30371,y:32893,ptovrint:False,ptlb:Fire dissolve power,ptin:_Firedissolvepower,varname:node_2414,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:5;n:type:ShaderForge.SFN_Append,id:263,x:32218,y:32335,varname:node_263,prsc:2|A-6941-OUT,B-19-OUT;n:type:ShaderForge.SFN_Vector1,id:19,x:32056,y:32425,varname:node_19,prsc:2,v1:0;n:type:ShaderForge.SFN_Blend,id:604,x:31427,y:32551,varname:node_604,prsc:2,blmd:1,clmp:True|SRC-218-R,DST-2159-OUT;n:type:ShaderForge.SFN_Blend,id:5408,x:31790,y:32583,varname:node_5408,prsc:2,blmd:6,clmp:True|SRC-2198-OUT,DST-604-OUT;n:type:ShaderForge.SFN_OneMinus,id:12,x:31441,y:32750,varname:node_12,prsc:2|IN-3269-R;n:type:ShaderForge.SFN_Multiply,id:2198,x:31669,y:32783,varname:node_2198,prsc:2|A-12-OUT,B-9800-OUT;n:type:ShaderForge.SFN_Slider,id:9800,x:31252,y:32927,ptovrint:False,ptlb:Fire size,ptin:_Firesize,varname:node_9800,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_SwitchProperty,id:6941,x:31790,y:32431,ptovrint:False,ptlb:Is Mask ?,ptin:_IsMask,varname:node_6941,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-604-OUT,B-5408-OUT;proporder:4961-2414-9800-218-9218-6941-3269;pass:END;sub:END;*/

Shader "Shader Forge/SHD_Fire" {
    Properties {
        _Firepanspeed ("Fire pan speed", Range(0, 3)) = 1
        _Firedissolvepower ("Fire dissolve power", Range(0, 5)) = 1
        _Firesize ("Fire size", Range(0, 2)) = 1
        _FireNoise ("Fire Noise", 2D) = "white" {}
        _FireRamp ("Fire Ramp", 2D) = "white" {}
        [MaterialToggle] _IsMask ("Is Mask ?", Float ) = 0
        _FireMask ("Fire Mask", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
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
            uniform sampler2D _FireRamp; uniform float4 _FireRamp_ST;
            uniform sampler2D _FireNoise; uniform float4 _FireNoise_ST;
            uniform sampler2D _FireMask; uniform float4 _FireMask_ST;
            uniform float _Firepanspeed;
            uniform float _Firedissolvepower;
            uniform float _Firesize;
            uniform fixed _IsMask;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_1121 = _Time;
                float2 node_6631 = (i.uv0+(1.0 - (_Firepanspeed*node_1121.g))*float2(0,1));
                float4 _FireNoise_var = tex2D(_FireNoise,TRANSFORM_TEX(node_6631, _FireNoise));
                float node_604 = saturate((_FireNoise_var.r*(i.uv0.g*_Firedissolvepower)));
                float4 _FireMask_var = tex2D(_FireMask,TRANSFORM_TEX(i.uv0, _FireMask));
                float _IsMask_var = lerp( node_604, saturate((1.0-(1.0-((1.0 - _FireMask_var.r)*_Firesize))*(1.0-node_604))), _IsMask );
                clip((1.0 - _IsMask_var) - 0.5);
////// Lighting:
////// Emissive:
                float2 node_263 = float2(_IsMask_var,0.0);
                float4 node_4052 = tex2D(_FireRamp,TRANSFORM_TEX(node_263, _FireRamp));
                float3 emissive = node_4052.rgb;
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
            uniform sampler2D _FireNoise; uniform float4 _FireNoise_ST;
            uniform sampler2D _FireMask; uniform float4 _FireMask_ST;
            uniform float _Firepanspeed;
            uniform float _Firedissolvepower;
            uniform float _Firesize;
            uniform fixed _IsMask;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_1121 = _Time;
                float2 node_6631 = (i.uv0+(1.0 - (_Firepanspeed*node_1121.g))*float2(0,1));
                float4 _FireNoise_var = tex2D(_FireNoise,TRANSFORM_TEX(node_6631, _FireNoise));
                float node_604 = saturate((_FireNoise_var.r*(i.uv0.g*_Firedissolvepower)));
                float4 _FireMask_var = tex2D(_FireMask,TRANSFORM_TEX(i.uv0, _FireMask));
                float _IsMask_var = lerp( node_604, saturate((1.0-(1.0-((1.0 - _FireMask_var.r)*_Firesize))*(1.0-node_604))), _IsMask );
                clip((1.0 - _IsMask_var) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

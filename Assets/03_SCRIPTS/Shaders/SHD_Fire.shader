// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-6411-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32471,y:32812,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:4052,x:32220,y:32617,varname:node_4052,prsc:2,ntxv:0,isnm:False|TEX-9218-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:9218,x:31983,y:32763,ptovrint:False,ptlb:Fire Ramp,ptin:_FireRamp,varname:node_9218,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:218,x:31353,y:32330,ptovrint:False,ptlb:Fire Noise,ptin:_FireNoise,varname:node_218,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:369f4c1b94a8db94d9ed0b42f8eef917,ntxv:0,isnm:False|UVIN-6631-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3269,x:31319,y:32514,ptovrint:False,ptlb:Fire Noise 2,ptin:_FireNoise2,varname:node_3269,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:3156,x:31189,y:32747,varname:node_3156,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6411,x:31601,y:32460,varname:node_6411,prsc:2|A-218-R,B-3156-V;n:type:ShaderForge.SFN_Time,id:1121,x:30502,y:32483,varname:node_1121,prsc:2;n:type:ShaderForge.SFN_Panner,id:6631,x:31104,y:32235,varname:node_6631,prsc:2,spu:1,spv:1|UVIN-3649-UVOUT,DIST-4049-OUT;n:type:ShaderForge.SFN_TexCoord,id:3649,x:30754,y:32190,varname:node_3649,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:4961,x:30409,y:32393,ptovrint:False,ptlb:Fire pan speed,ptin:_Firepanspeed,varname:node_4961,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:3;n:type:ShaderForge.SFN_Multiply,id:4948,x:30754,y:32398,varname:node_4948,prsc:2|A-4961-OUT,B-1121-T;n:type:ShaderForge.SFN_OneMinus,id:4049,x:30916,y:32398,varname:node_4049,prsc:2|IN-4948-OUT;proporder:7241-218-4961;pass:END;sub:END;*/

Shader "Shader Forge/SHD_Fire" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _FireNoise ("Fire Noise", 2D) = "white" {}
        _Firepanspeed ("Fire pan speed", Range(0, 3)) = 1
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
            uniform sampler2D _FireNoise; uniform float4 _FireNoise_ST;
            uniform float _Firepanspeed;
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
////// Lighting:
////// Emissive:
                float4 node_1121 = _Time;
                float2 node_6631 = (i.uv0+(1.0 - (_Firepanspeed*node_1121.g))*float2(1,1));
                float4 _FireNoise_var = tex2D(_FireNoise,TRANSFORM_TEX(node_6631, _FireNoise));
                float node_6411 = (_FireNoise_var.r*i.uv0.g);
                float3 emissive = float3(node_6411,node_6411,node_6411);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

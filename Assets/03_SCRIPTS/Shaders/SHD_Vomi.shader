// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-1920-RGB,voffset-1822-OUT;n:type:ShaderForge.SFN_Color,id:1920,x:31701,y:32704,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1920,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4952893,c2:0.5808823,c3:0.1879325,c4:1;n:type:ShaderForge.SFN_Multiply,id:1822,x:32344,y:32993,varname:node_1822,prsc:2|A-8922-R,B-7226-OUT,C-4871-OUT;n:type:ShaderForge.SFN_Slider,id:4871,x:31929,y:33266,ptovrint:False,ptlb:Displacement,ptin:_Displacement,varname:node_4871,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_NormalVector,id:7226,x:32049,y:33074,prsc:2,pt:False;n:type:ShaderForge.SFN_Tex2d,id:8922,x:31671,y:32512,ptovrint:False,ptlb:Noise Vomi,ptin:_NoiseVomi,varname:node_8922,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:de679359006064945a799c918ec3af8b,ntxv:0,isnm:False|UVIN-1233-UVOUT;n:type:ShaderForge.SFN_Blend,id:6454,x:32057,y:32533,varname:node_6454,prsc:2,blmd:7,clmp:True|SRC-8922-R,DST-1920-RGB;n:type:ShaderForge.SFN_Slider,id:257,x:30712,y:32573,ptovrint:False,ptlb:node_257,ptin:_node_257,varname:node_257,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Time,id:136,x:30869,y:32424,varname:node_136,prsc:2;n:type:ShaderForge.SFN_Panner,id:1233,x:31463,y:32482,varname:node_1233,prsc:2,spu:1,spv:1|UVIN-8952-UVOUT,DIST-6282-OUT;n:type:ShaderForge.SFN_TexCoord,id:8952,x:31224,y:32340,varname:node_8952,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:9259,x:31045,y:32496,varname:node_9259,prsc:2|A-136-T,B-257-OUT;n:type:ShaderForge.SFN_OneMinus,id:6282,x:31224,y:32496,varname:node_6282,prsc:2|IN-9259-OUT;proporder:1920-4871-8922-257;pass:END;sub:END;*/

Shader "Shader Forge/SHD_Vomi" {
    Properties {
        _Color ("Color", Color) = (0.4952893,0.5808823,0.1879325,1)
        _Displacement ("Displacement", Range(0, 1)) = 0
        _NoiseVomi ("Noise Vomi", 2D) = "white" {}
        _node_257 ("node_257", Range(0, 1)) = 0
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
            uniform float _Displacement;
            uniform sampler2D _NoiseVomi; uniform float4 _NoiseVomi_ST;
            uniform float _node_257;
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
                float4 node_136 = _Time;
                float2 node_1233 = (o.uv0+(1.0 - (node_136.g*_node_257))*float2(1,1));
                float4 _NoiseVomi_var = tex2Dlod(_NoiseVomi,float4(TRANSFORM_TEX(node_1233, _NoiseVomi),0.0,0));
                v.vertex.xyz += (_NoiseVomi_var.r*v.normal*_Displacement);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
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
            uniform float _Displacement;
            uniform sampler2D _NoiseVomi; uniform float4 _NoiseVomi_ST;
            uniform float _node_257;
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
                float4 node_136 = _Time;
                float2 node_1233 = (o.uv0+(1.0 - (node_136.g*_node_257))*float2(1,1));
                float4 _NoiseVomi_var = tex2Dlod(_NoiseVomi,float4(TRANSFORM_TEX(node_1233, _NoiseVomi),0.0,0));
                v.vertex.xyz += (_NoiseVomi_var.r*v.normal*_Displacement);
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

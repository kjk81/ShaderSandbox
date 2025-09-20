Shader "Custom/My First Shader" {

	// utilize Properties to use the material to configure color
	// property name must be followed by a string and a type, then a default value
	Properties {
		_Tint ("Tint", Color) = (1, 1, 1, 1)
	}

	SubShader {

		Pass {
			// pass is where an object is rendered
			// can pass multiple times
			CGPROGRAM

			// special directives telling compiler which programs to use
			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

			#include "UnityCG.cginc"

			float4 _Tint;

			struct Interpolators {
				float4 position : SV_POSITION;
				float3 localPosition: TEXCOORD0;
			};

			// tell GPU what to do with data, use 4x4 matrices to maintain offset and scale too
			// POSITION - object pos, SV - System Value, SV_POSITION - display pos
			// distorted if returning only position because the position is given in object-space, not display space
			Interpolators MyVertexProgram(float4 position : POSITION) {
				Interpolators i; // utilizing structs to cleanly handle data
				// projecting sphere onto display
				i.localPosition = position.xyz; // sending vertex mesh data to fragment shader
				i.position = UnityObjectToClipPos(position); 
				return i;
			}

			// takes in data straight from vertex shader
			// return 0 returns black, would be transparent if not for the fact this is an opaque shade (doesn't support transparency)
			// return _Tint to return color selected by material
			// localPosition - the interpolated local position (pos within a triangle)
			// So to my understanding, the fragment shader receives the transformed position of the fragment, 
			// but localPos makes it so it also receives the interpolated local position of the specific pixel on the mesh. 
			// And the vertex shader just receives the position of the mesh in object-space pre-transformation?
			float4 MyFragmentProgram(Interpolators i) : SV_TARGET {
				return float4(i.localPosition + 0.5, 1) * _Tint; // green tint, only y remains, which is why black at bottom
			}

			ENDCG
		}
	}	
	
} 


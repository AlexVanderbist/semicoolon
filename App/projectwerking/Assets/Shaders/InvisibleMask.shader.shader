Shader "Custom/InvisibleMask" {
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	_Mask("Culling Mask", 2D) = "white" {}
	_Cutoff("Alpha cutoff", Range(0,1)) = 0.1
	}
	
	SubShader{
		// draw after all opaque objects (queue = 2001):
		Tags{ "Queue" = "Geometry+1" }
		Pass{
		Blend Zero One // keep the image behind it
	}
	}
}

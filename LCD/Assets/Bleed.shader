Shader "Bleed" {

	Properties{
		_Bleed("Bleed", Range(0, 1)) = 0.5
		_Old("Old", 2D) = ""
		_New("New", 2D) = ""
	}

	SubShader{
		Pass{
			SetTexture[_Old]
			SetTexture[_New]{
				ConstantColor(0,0,0,[_Bleed])
				Combine texture Lerp(constant) previous
			}
		}
	}
}
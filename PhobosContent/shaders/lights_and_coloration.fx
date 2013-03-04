float4  AmbiantColor ;
float4  ShiftColorMagenta = {1,0,1,1} ;
float4  ShiftColorCyan = {0,1,1,1} ;
sampler TextureSampler: register(s0) ;
float ondulation = 0.01;



float4 InvertColor(float2 Tex: TEXCOORD0) : COLOR0
{
	float4 Color;
	Color = tex2D(TextureSampler, Tex);
	
	//do not touch transparent pixels
    float a = Color.a;
	if(a != 0) {
		Color.rgb = 1 - Color.rgb;
	}
	return Color;

}

float4 ToGrey(float2 Tex: TEXCOORD0) : COLOR0
{
	float4 Color;
	Color = tex2D(TextureSampler, Tex);
	
	//do not touch transparent pixels
    float a = Color.a;
	if(a != 0) {
		Color.r = Color.rgb;
		Color.g = Color.rgb;
		Color.b = Color.rgb;
	}
	return Color;

}

float4 BurkRelief(float2 Tex: TEXCOORD0) : COLOR0
{
	float4 Color;
	Color = tex2D(TextureSampler, Tex);
    float a = Color.a;
	if(a != 0) {
		Color.a = 1.0f;
		Color.rgb = 0.5f;
		Color -= tex2D( TextureSampler, Tex.xy-0.001)*2.0f;
		Color += tex2D( TextureSampler, Tex.xy+0.001)*2.0f;
		Color.rgb = (Color.r+Color.g+Color.b)/3.0f;
	}
	
	return Color;
}

float4 ApplyAmbiantColor(float2 Tex: TEXCOORD0) : COLOR0
{
	float4 Color = tex2D(TextureSampler, Tex);
	
    float r = Color.r;
    float g = Color.g;
    float b = Color.b;
    float a = Color.a;
	
	//do not touch transparent pixels
	if(a > 0) {
	    
		//replace color magenta
		if(r > 0.5 && g == 0 && b > 0.5) {
			Color.g = r ;
			Color *= ShiftColorMagenta ;
		}
		if(r == 0 && g > 0.5 && b > 0.5) {
			Color.r = g ;
			Color *= ShiftColorCyan ;
		}

		Color *= AmbiantColor ;

	}


	return Color ;
}

technique hit
{

	pass InvertColor
	{
		PixelShader = compile ps_2_0 ApplyAmbiantColor();
	}
}
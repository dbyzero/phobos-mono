float4  AmbiantColor ;
sampler TextureSampler: register(s0) ;

void ApplyAmbiantColor(inout float4 color : COLOR0, in float2 texCoord : TEXCOORD0)
{
	//color of the pixel
	float4 pixel_color = tex2D(TextureSampler, texCoord);

	//if no alpha
	if(pixel_color.a == 1) {
		float4 color_list[256] ;
		color_list[0] = float4(0.969, 0.627, 0.145,1) ;		//red hair
		color_list[1] = float4(1, 0.894, 0.173,1) ;			//golden red
		color_list[2] = float4(0.953, 0.953, 0.953,1) ;		//white hair
		color_list[3] = float4(0.1, 0.1, 0.1,1) ;			//black hair
		color_list[4] = float4(0.424, 0.314, 0.082,1) ;		//brown
		color_list[5] = float4(0.5,0.5,0.5,1) ;				//grey
		color_list[6] = float4(1,0.906,0.098,1) ;			//gold
		color_list[7] = float4(0.651, 0.643, 0.584,1) ;		//pure iron
		color_list[8] = float4(0.294, 0.459, 0.6,1) ;		//bluejeans
		color_list[9] = float4(0.714, 0.439, 0.114,1) ;		//leather1
		color_list[10] = float4(0.714, 0.243, 0.114,1) ;	//leather2
		color_list[11] = float4(1, 1, 1,1) ;	//white
		color_list[12] = float4(0.027, 0.49, 0.102,1) ;		//greenpants
		color_list[13] = float4(1,0,0,1) ;					//red
		color_list[14] = float4(1,1,1,0.3) ;				//ghost

		//unset colors
		for(int i=15;i<255;i++) {
			color_list[i] = float4(1,1,1,1) ; //unset
		}
	
		/*
		 * hijack color desciption :
		 * r : offset color of refill magenta
		 * g : offset color of refill cyan
		 * b : offset color of local light
		 * a : 0 - 255 intensity of light color, usefull for smoothy light
		 */
		float4 hijack_color = color ; 


		/*
		 * if no blue / >50% green / >50% red
		 * Recoloration magenta trig !!!
		 */
		if(pixel_color.r > 0.5 && pixel_color.g == 0 && pixel_color.b > 0.5) {
			pixel_color.g = pixel_color.r ; //put back green
			float offset = min(15,floor(hijack_color.r*255.0)) ;
			pixel_color *= color_list[offset] ;
			/*pixel_color.r = hijack_color.r ;
			pixel_color.g = hijack_color.r ;
			pixel_color.b = hijack_color.r ;*/
		}
	
		/*
		 * if no red / >50% green / >50% blue
		 * Recoloration cyan trig !!!
		 */
		if(pixel_color.r == 0 && pixel_color.g > 0.5 && pixel_color.b > 0.5) {
			pixel_color.r = pixel_color.g ; //put back red
			float offset = min(4,floor(hijack_color.g*255.0)) ;
			pixel_color *= color_list[offset] ;
		}
		color = pixel_color * AmbiantColor;
	} else {
		color = pixel_color ;
	}
}



technique hit
{

	pass InvertColor
	{
		PixelShader = compile ps_2_0 ApplyAmbiantColor();
	}
}
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
		color_list[11] = float4(1, 1, 1,1) ;				//white
		color_list[12] = float4(0.027, 0.49, 0.102,1) ;		//greenpants
		color_list[13] = float4(1,0,0,1) ;					//red
		color_list[14] = float4(1,1,1,0.3) ;				//ghost
		color_list[15] = float4(1,1,1,0) ;					//nothing
		
		//unseted colors
		for(int i=16;i<255;i++) {
			color_list[i] = float4(1,1,1,1) ; //unset
		}

		/*float4 light_color ;

		//convert color.b || color.a to 16bits colors
		bool lightbits_color[16] ;

		int working_color_8to15 = color.b * 255 ;
		int working_color_0to7 = color.a * 255 ;
		int red_4bits	= 0;
		int green_4bits	= 0; 
		int blue_4bits  = 0; 
		int alpha_4bits = 0;

		lightbits_color[15] = (working_color_8to15 > 127) ? 1 : 0 ;
		if(working_color_8to15 > 127) working_color_8to15 -= 128 ;
		lightbits_color[14] = (working_color_8to15 > 63) ? 1 : 0 ;
		if(working_color_8to15 > 63) working_color_8to15 -= 64 ;
		lightbits_color[13] = (working_color_8to15 > 31) ? 1 : 0 ;
		if(working_color_8to15 > 31) working_color_8to15 -= 32 ;
		lightbits_color[12] = (working_color_8to15 > 15) ? 1 : 0 ;
		if(working_color_8to15 > 15) working_color_8to15 -= 16 ;
		lightbits_color[11] = (working_color_8to15 > 7) ? 1 : 0 ;
		if(working_color_8to15 > 7) working_color_8to15 -= 8 ;
		lightbits_color[10] = (working_color_8to15 > 3) ? 1 : 0 ;
		if(working_color_8to15 > 3) working_color_8to15 -= 4 ;
		lightbits_color[9] = (working_color_8to15 > 1) ? 1 : 0 ;
		if(working_color_8to15 > 1) working_color_8to15 -= 2 ;
		lightbits_color[8] = (working_color_8to15 > 0) ? 1 : 0 ;

		lightbits_color[7] = (working_color_0to7 > 127) ? 1 : 0 ;
		if(working_color_0to7 > 127) working_color_0to7 -= 128 ;
		lightbits_color[6] = (working_color_0to7 > 63) ? 1 : 0 ;
		if(working_color_0to7 > 63) working_color_0to7 -= 64 ;
		lightbits_color[5] = (working_color_0to7 > 31) ? 1 : 0 ;
		if(working_color_0to7 > 31) working_color_0to7 -= 32 ;
		lightbits_color[4] = (working_color_0to7 > 15) ? 1 : 0 ;
		if(working_color_0to7 > 15) working_color_0to7 -= 16 ;
		lightbits_color[3] = (working_color_0to7 > 7) ? 1 : 0 ;
		if(working_color_0to7 > 7) working_color_0to7 -= 8 ;
		lightbits_color[2] = (working_color_0to7 > 3) ? 1 : 0 ;
		if(working_color_0to7 > 3) working_color_0to7 -= 4 ;
		lightbits_color[1] = (working_color_0to7 > 1) ? 1 : 0 ;
		if(working_color_0to7 > 1) working_color_0to7 -= 2 ;
		lightbits_color[0] = (working_color_0to7 > 0) ? 1 : 0 ;
		
		//make 4bits red from half of origin blue
		red_4bits += (working_color_8to15 > 127) ? 8 : 0 ;
		if(working_color_8to15 > 127) working_color_8to15 -= 128 ;
		red_4bits += (working_color_8to15 > 63) ? 4 : 0 ;
		if(working_color_8to15 > 63) working_color_8to15 -= 64 ;
		red_4bits += (working_color_8to15 > 31) ? 2 : 0 ;
		if(working_color_8to15 > 31) working_color_8to15 -= 32 ;
		red_4bits += (working_color_8to15 > 15) ? 1 : 0 ;
		if(working_color_8to15 > 31) working_color_8to15 -= 16 ;

		//make 4bits green from half of origin blue
		green_4bits += (working_color_8to15 > 7) ? 8 : 0 ;
		if(working_color_8to15 > 7) working_color_8to15 -= 8 ;
		green_4bits += (working_color_8to15 > 3) ? 4 : 0 ;
		if(working_color_8to15 > 3) working_color_8to15 -= 4 ;
		green_4bits += (working_color_8to15 > 1) ? 2 : 0 ;
		if(working_color_8to15 > 1) working_color_8to15 -= 2 ;
		green_4bits += (working_color_8to15 > 0) ? 1 : 0 ;
		
		//make 4bits blue from half of origin alpha
		blue_4bits += (working_color_0to7 > 127) ? 8 : 0 ;
		if(working_color_0to7 > 127) working_color_0to7 -= 128 ;
		blue_4bits += (working_color_0to7 > 63) ? 4 : 0 ;
		if(working_color_0to7 > 63) working_color_0to7 -= 64 ;
		blue_4bits += (working_color_0to7 > 31) ? 2 : 0 ;
		if(working_color_0to7 > 31) working_color_0to7 -= 32 ;
		blue_4bits += (working_color_0to7 > 15) ? 1 : 0 ;
		if(working_color_0to7 > 31) working_color_0to7 -= 16 ;

		//make 4bits alpha from half of origin alpha
		alpha_4bits += (working_color_0to7 > 7) ? 8 : 0 ;
		if(working_color_0to7 > 7) working_color_0to7 -= 8 ;
		alpha_4bits += (working_color_0to7 > 3) ? 4 : 0 ;
		if(working_color_0to7 > 3) working_color_0to7 -= 4 ;
		alpha_4bits += (working_color_0to7 > 1) ? 2 : 0 ;
		if(working_color_0to7 > 1) working_color_0to7 -= 2 ;
		alpha_4bits += (working_color_0to7 > 0) ? 1 : 0 ;

		light_color.r = 255 * red_4bits/15 ;
		light_color.b = 255 * green_4bits/15 ;
		light_color.g = 255 * blue_4bits/15 ;
		light_color.a = 255 * alpha_4bits/15 ;*/

	
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
			pixel_color *= color_list[floor(min(15,hijack_color.a*255.0))] ;
		}
	
		/*
		 * if no red / >50% green / >50% blue
		 * Recoloration cyan trig !!!
		if(pixel_color.r == 0 && pixel_color.g > 0.5 && pixel_color.b > 0.5) {
			pixel_color.r = pixel_color.g ; //put back red
			float offset = min(4,floor(hijack_color.g*255.0)) ;
			pixel_color *= color_list[offset] ;
		}
		 */
		
		/*hijack_color.a = 1 ;*/

		//true apply color
		//color.r = (1 - (1 - hijack_color.r) * (1 - color.r * AmbiantColor.r)) ;
		//color.g = (1 - (1 - hijack_color.g) * (1 - color.g * AmbiantColor.g)) ;
		//color.b = (1 - (1 - hijack_color.b) * (1 - color.b * AmbiantColor.b)) ;
		//color.a = 1 ;

		//old java color
		/*color.r = max(hijack_color.r*pixel_color.r,AmbiantColor.r*pixel_color.r) ;
		color.g = max(hijack_color.g*pixel_color.g,AmbiantColor.g*pixel_color.g) ;
		color.b = max(hijack_color.b*pixel_color.b,AmbiantColor.b*pixel_color.b) ;*/
	
		color.r = min(pixel_color.r * 3,hijack_color.r + AmbiantColor.r*pixel_color.r) ;
		color.g = min(pixel_color.g * 3,hijack_color.g + AmbiantColor.g*pixel_color.g) ;
		color.b = min(pixel_color.b * 3,hijack_color.b + AmbiantColor.b*pixel_color.b) ;

	    color.a = 1 ;
				
	} else {
		color = pixel_color ;
	}
}



technique hit
{
	pass ApplyColor
	{
		PixelShader = compile ps_2_0 ApplyAmbiantColor();
	}
}
void ExpandUV_float(float2 coordsIN, float increase, out float2 coordsOUT)
{
    float2 mid = coordsIN - 0.5;
    coordsOUT = coordsIN + (mid*-increase);
}

void Grayscale_half(half3 colorIN, out half colorOUT)
{
    colorOUT = colorIN.r*0.299 + colorIN.g*0.587 + colorIN.b*0.114; 
}
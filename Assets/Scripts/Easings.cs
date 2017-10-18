using System;
using UnityEngine;
using System.Collections;

public static class Easings
{
	#region Linear
	
	/// <summary>
	/// Easing equation function for a simple linear tweening, with no easing.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float Linear( float t, float b, float c, float d )
	{
		return c * t / d + b;
	}
	
	#endregion
	
	#region Sine
	
	/// <summary>
	/// Easing equation function for a sinusoidal (sin(t)) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float SineOut( float t, float b, float c, float d )
	{
		return c * (float)Math.Sin( t / d * ( (float)Math.PI / 2 ) ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a sinusoidal (sin(t)) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float SineIn( float t, float b, float c, float d )
	{
		return -c * (float)Math.Cos( t / d * ( (float)Math.PI / 2 ) ) + c + b;
	}
	
	/// <summary>
	/// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float SineInOut( float t, float b, float c, float d )
	{
		if ( ( t /= d / 2 ) < 1 )
			return c / 2 * ( (float)Math.Sin( (float)Math.PI * t / 2 ) ) + b;
		
		return -c / 2 * ( (float)Math.Cos( (float)Math.PI * --t / 2 ) - 2 ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float SineOutIn( float t, float b, float c, float d )
	{
		if ( t < d / 2 )
			return SineOut( t * 2, b, c / 2, d );
		
		return SineIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
	}
	
	#endregion
	
	#region Cubic
	
	/// <summary>
	/// Easing equation function for a cubic (t^3) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float CubicOut( float t, float b, float c, float d )
	{
		return c * ( ( t = t / d - 1 ) * t * t + 1 ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a cubic (t^3) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float CubicIn( float t, float b, float c, float d )
	{
		return c * ( t /= d ) * t * t + b;
	}
	
	/// <summary>
	/// Easing equation function for a cubic (t^3) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float CubicInOut( float t, float b, float c, float d )
	{
		if ( ( t /= d / 2 ) < 1 )
			return c / 2 * t * t * t + b;
		
		return c / 2 * ( ( t -= 2 ) * t * t + 2 ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a cubic (t^3) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float CubicOutIn( float t, float b, float c, float d )
	{
		if ( t < d / 2 )
			return CubicOut( t * 2, b, c / 2, d );
		
		return CubicIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
	}

    #endregion

    #region Quad

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float QuadOut(float t, float b, float c, float d)
    {
        return -c * (t /= d) * (t - 2) + b;
    }

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float QuadIn(float t, float b, float c, float d)
    {
        return c * (t /= d) * t + b;
    }

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float QuadInOut(float t, float b, float c, float d)
    {
        if((t /= d / 2) < 1)
            return c / 2 * t * t + b;

        return -c / 2 * ((--t) * (t - 2) - 1) + b;
    }

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing out/in: 
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float QuadOutIn(float t, float b, float c, float d)
    {
        if(t < d / 2)
            return QuadOut(t * 2, b, c / 2, d);

        return QuadIn((t * 2) - d, b + c / 2, c / 2, d);
    }

    #endregion

    #region Quartic

    /// <summary>
    /// Easing equation function for a quartic (t^4) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float QuartOut( float t, float b, float c, float d )
	{
		return -c * ( ( t = t / d - 1 ) * t * t * t - 1 ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a quartic (t^4) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float QuartIn( float t, float b, float c, float d )
	{
		return c * ( t /= d ) * t * t * t + b;
	}
	
	/// <summary>
	/// Easing equation function for a quartic (t^4) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float QuartInOut( float t, float b, float c, float d )
	{
		if ( ( t /= d / 2 ) < 1 )
			return c / 2 * t * t * t * t + b;
		
		return -c / 2 * ( ( t -= 2 ) * t * t * t - 2 ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a quartic (t^4) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float QuartOutIn( float t, float b, float c, float d )
	{
		if ( t < d / 2 )
			return QuartOut( t * 2, b, c / 2, d );
		
		return QuartIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
	}
	
	#endregion
	
	#region Quintic
	
	/// <summary>
	/// Easing equation function for a quintic (t^5) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float QuintOut( float t, float b, float c, float d )
	{
		return c * ( ( t = t / d - 1 ) * t * t * t * t + 1 ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a quintic (t^5) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float QuintIn( float t, float b, float c, float d )
	{
		return c * ( t /= d ) * t * t * t * t + b;
	}
	
	/// <summary>
	/// Easing equation function for a quintic (t^5) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float QuintInOut( float t, float b, float c, float d )
	{
		if ( ( t /= d / 2 ) < 1 )
			return c / 2 * t * t * t * t * t + b;
		return c / 2 * ( ( t -= 2 ) * t * t * t * t + 2 ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a quintic (t^5) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float QuintOutIn( float t, float b, float c, float d )
	{
		if ( t < d / 2 )
			return QuintOut( t * 2, b, c / 2, d );
		return QuintIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
	}

    #endregion
    
    #region Expo

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float ExpoOut(float t, float b, float c, float d)
    {
        return (t == d) ? b + c : c * (-(float)Math.Pow(2, -10 * t / d) + 1) + b;
    }

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float ExpoIn(float t, float b, float c, float d)
    {
        return (t == 0) ? b : c * (float)Math.Pow(2, 10 * (t / d - 1)) + b;
    }

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float ExpoInOut(float t, float b, float c, float d)
    {
        if(t == 0)
            return b;

        if(t == d)
            return b + c;

        if((t /= d / 2) < 1)
            return c / 2 * (float)Math.Pow(2, 10 * (t - 1)) + b;

        return c / 2 * (-(float)Math.Pow(2, -10 * --t) + 2) + b;
    }

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing out/in: 
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float ExpoOutIn(float t, float b, float c, float d)
    {
        if(t < d / 2)
            return ExpoOut(t * 2, b, c / 2, d);

        return ExpoIn((t * 2) - d, b + c / 2, c / 2, d);
    }

    #endregion

    #region Circular

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float CircOut(float t, float b, float c, float d)
    {
        return c * (float)Math.Sqrt(1 - (t = t / d - 1) * t) + b;
    }

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing in: 
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float CircIn(float t, float b, float c, float d)
    {
        return -c * ((float)Math.Sqrt(1 - (t /= d) * t) - 1) + b;
    }

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float CircInOut(float t, float b, float c, float d)
    {
        if((t /= d / 2) < 1)
            return -c / 2 * ((float)Math.Sqrt(1 - t * t) - 1) + b;

        return c / 2 * ((float)Math.Sqrt(1 - (t -= 2) * t) + 1) + b;
    }

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float CircOutIn(float t, float b, float c, float d)
    {
        if(t < d / 2)
            return CircOut(t * 2, b, c / 2, d);

        return CircIn((t * 2) - d, b + c / 2, c / 2, d);
    }

    #endregion

    #region Elastic

    /// <summary>
    /// Easing equation function for an elastic (exponentially decaying sine wave) easing out: 
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float ElasticOut( float t, float b, float c, float d )
	{
		if ( ( t /= d ) == 1 )
			return b + c;
		
		float p = d * 0.3f;
		float s = p / 4;
		
		return ( c * (float)Math.Pow( 2, -10 * t ) * (float)Math.Sin( ( t * d - s ) * ( 2 * (float)Math.PI ) / p ) + c + b );
	}
	
	/// <summary>
	/// Easing equation function for an elastic (exponentially decaying sine wave) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float ElasticIn( float t, float b, float c, float d )
	{
		if ( ( t /= d ) == 1 )
			return b + c;
		
		float p = d * 0.3f;
		float s = p / 4;
		
		return -( c * (float)Math.Pow( 2, 10 * ( t -= 1 ) ) * (float)Math.Sin( ( t * d - s ) * ( 2 * (float)Math.PI ) / p ) ) + b;
	}
	
	/// <summary>
	/// Easing equation function for an elastic (exponentially decaying sine wave) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float ElasticInOut( float t, float b, float c, float d )
	{
		if ( ( t /= d / 2 ) == 2 )
			return b + c;
		
		float p = d * (0.3f * 1.5f);
		float s = p / 4;
		
		if ( t < 1 )
			return -0.5f * ( c * (float)Math.Pow( 2, 10 * ( t -= 1 ) ) * (float)Math.Sin( ( t * d - s ) * ( 2 * (float)Math.PI ) / p ) ) + b;
		return c * (float)Math.Pow( 2, -10 * ( t -= 1 ) ) * (float)Math.Sin( ( t * d - s ) * ( 2 * (float)Math.PI ) / p ) * 0.5f + c + b;
	}
	
	/// <summary>
	/// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float ElasticOutIn( float t, float b, float c, float d )
	{
		if ( t < d / 2 )
			return ElasticOut( t * 2, b, c / 2, d );
		return ElasticIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
	}
	
	#endregion
	
	#region Bounce
	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float BounceOut( float t, float b, float c, float d )
	{
		if ( ( t /= d ) < ( 1 / 2.75f ) )
			return c * ( 7.5625f * t * t ) + b;
		else if ( t < ( 2 / 2.75f ) )
			return c * ( 7.5625f * ( t -= ( 1.5f / 2.75f ) ) * t + .75f ) + b;
		else if ( t < ( 2.5 / 2.75f ) )
			return c * ( 7.5625f * ( t -= ( 2.25f / 2.75f ) ) * t + .9375f ) + b;
		else
			return c * ( 7.5625f * ( t -= ( 2.625f / 2.75f ) ) * t + .984375f ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float BounceIn( float t, float b, float c, float d )
	{
		return c - BounceOut( d - t, 0, c, d ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float BounceInOut( float t, float b, float c, float d )
	{
		if ( t < d / 2 )
			return BounceIn( t * 2, 0, c, d ) * .5f + b;
		else
			return BounceOut( t * 2 - d, 0, c, d ) * .5f + c * .5f + b;
	}
	
	/// <summary>
	/// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float BounceOutIn( float t, float b, float c, float d )
	{
		if ( t < d / 2 )
			return BounceOut( t * 2, b, c / 2, d );
		return BounceIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
	}
	
	#endregion
	
	#region Back
	
	/// <summary>
	/// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float BackOut( float t, float b, float c, float d )
	{
		return c * ( ( t = t / d - 1 ) * t * ( ( 1.70158f + 1 ) * t + 1.70158f ) + 1 ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in: 
	/// accelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float BackIn( float t, float b, float c, float d )
	{
		return c * ( t /= d ) * t * ( ( 1.70158f + 1 ) * t - 1.70158f ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out: 
	/// acceleration until halfway, then deceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float BackInOut( float t, float b, float c, float d )
	{
		float s = 1.70158f;
		if ( ( t /= d / 2 ) < 1 )
			return c / 2 * ( t * t * ( ( ( s *= ( 1.525f ) ) + 1 ) * t - s ) ) + b;
		return c / 2 * ( ( t -= 2 ) * t * ( ( ( s *= ( 1.525f ) ) + 1 ) * t + s ) + 2 ) + b;
	}
	
	/// <summary>
	/// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: 
	/// deceleration until halfway, then acceleration.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	public static float BackOutIn( float t, float b, float c, float d )
	{
		if ( t < d / 2 )
			return BackOut( t * 2, b, c / 2, d );
		return BackIn( ( t * 2 ) - d, b + c / 2, c / 2, d );
	}
	
	#endregion
}

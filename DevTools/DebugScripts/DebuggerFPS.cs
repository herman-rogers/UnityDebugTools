using UnityEngine;

public class DebuggerFPS : MonoBehaviour
{
	private float f_mAccumulation;
	private float f_mTimeLeftCurrentInterval;
	private int i_mNumberOfFrames;
	private const float f_mUpdateInterval = 0.5f;

	void Update( )
	{
		UpdateFPS( );
	}

	public string FramesPerSecond( )
	{
		string fpsFormula = "FPS: " + ( f_mAccumulation / i_mNumberOfFrames );
		return fpsFormula;
	}
	
	private void InitializeFpsLabel( )
	{
		f_mTimeLeftCurrentInterval = f_mUpdateInterval;
		f_mAccumulation = 0.0f;
		i_mNumberOfFrames = 0;
	}
	
	private void UpdateFPS( )
	{
		f_mTimeLeftCurrentInterval = f_mTimeLeftCurrentInterval - Time.deltaTime;
		f_mAccumulation = f_mAccumulation + ( Time.timeScale / Time.deltaTime );
		i_mNumberOfFrames = i_mNumberOfFrames + 1;
		if( f_mTimeLeftCurrentInterval > 0.0f )
		{
			return;
		}
		InitializeFpsLabel( );
	}
}

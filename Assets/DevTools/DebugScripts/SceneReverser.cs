using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneReverser : UnityObserver {
	public GameObject testGameObject;
	public GameObject mainCamera;
	public bool activateSceneReverser = false;
    private int currentPositionInList;
    private List< Vector3 > transformSnapShot;
	private List< Quaternion > quaternionSnapShot;
	private List< Vector3 > cameraSnapShot;
	private Dictionary< GameObject, Vector3 > transformSnapShots;
	private const int stepForward = 1;
	private const int stepBack = -1;
    
	void Start ( )
	{
		transformSnapShot = new List< Vector3 >( );
		quaternionSnapShot = new List<Quaternion>( );
		cameraSnapShot = new List< Vector3 >( );
	}

	void Update ( )
	{
		if( !activateSceneReverser )
		{
			return;
		}
		RecordTransform( );
	}

	public override void OnNotify (Object sender, EventArguments e)
	{
		switch( e.eventMessage )
		{
			case SceneStepper.STATE_IS_PAUSED:
			    currentPositionInList = transformSnapShot.Count;
			    break;
		    case SceneStepper.STATE_IS_RESUMED:
			    ResetState( );
			    break;
		}
	}

	public Vector3 GetTargetPosition( )
	{
		return testGameObject.gameObject.transform.position;
	}
	
	private void RecordTransform( )
	{
		if( SceneStepper.currentPauseState == PauseState.SCENE_IS_PAUSED )
		{
			StepThroughReverser( );
            return;
		}
		cameraSnapShot.Add( mainCamera.transform.position );
		transformSnapShot.Add( testGameObject.transform.position );
		quaternionSnapShot.Add( testGameObject.transform.rotation );
	}

	private void StepThroughReverser( )
	{
		if( Input.GetKey( KeyCode.B ) )
		{
			Reverser( stepForward );
		}
		if( Input.GetKey( KeyCode.N ) )
		{
			Reverser( stepBack );
		}
	}

	private void Reverser( int stepDirection )
	{ 
		currentPositionInList = ( currentPositionInList - ( stepDirection ) );
		if( transformSnapShot.Count <= currentPositionInList
		    || currentPositionInList <= 0 )
		{
			currentPositionInList = transformSnapShot.Count;
			return;
		}
	    testGameObject.transform.position = transformSnapShot[ currentPositionInList ];
		testGameObject.transform.rotation = quaternionSnapShot[ currentPositionInList ];
		mainCamera.transform.position = cameraSnapShot[ currentPositionInList ];
    }

	private void ResetState( )
	{
		transformSnapShot.Clear( );
		quaternionSnapShot.Clear( );
		cameraSnapShot.Clear( );
    }
}
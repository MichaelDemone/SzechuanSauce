using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwipingHandler : MonoBehaviour 
{

	public enum SwipeDirection
	{
		Up,
		Down,
		Left,
		Right,
	}

	public float MinMagnitudeForSwipe = 5f;
	public Action<Vector2> UserSwiped;
	public Action UserTapped;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		CheckMouseInput();
		//CheckMobileInput();
	}

	private bool swiping = false;
	private List<Vector2> swipingPoints = new List<Vector2>();

	private int? currentTouch;
	private void CheckMobileInput()
	{
		foreach (Touch touch in Input.touches)
		{
			if (currentTouch == null) currentTouch = touch.fingerId;

			if (currentTouch != touch.fingerId) continue;
			
			swipingPoints.Add(touch.position);
			return;
		}

		if (currentTouch == null) return;
		
		currentTouch = null;
		Vector2 direction = GetDirection(swipingPoints);
		UserSwiped(direction);
	}
	
	private void CheckMouseInput()
	{
		if (Input.GetMouseButton(0))
		{
			if (!swiping)
			{
				swipingPoints.Clear();
				swiping = true;
			} 
			
			swipingPoints.Add(Input.mousePosition);
		}
		else if (swiping)
		{
			swiping = false;
			Vector2 direction = GetDirection(swipingPoints);
			print(direction.magnitude);
			if (direction.magnitude < MinMagnitudeForSwipe)
			{
				if(UserTapped != null) UserTapped();
			}
			else
			{
				UserSwiped(direction);
			}
		}
	}

	private Vector2 directionSwipe = new Vector2();
	private Vector2 GetDirection(List<Vector2> swipingPoints)
	{
		float xAmount = swipingPoints.Last().x - swipingPoints.First().x;
		float yAmount = swipingPoints.Last().y - swipingPoints.First().y;
		
		directionSwipe.Set(xAmount, yAmount);
		return directionSwipe;

	}

    private Vector2 SwipingLikeOnTinderButIfTheGirlsWereNugs() {
        Vector2 DylansSexyVector = new Vector2(0, 0) ;
        if(Input.touches[0].phase == TouchPhase.Moved) {
            DylansSexyVector=Input.touches[0].deltaPosition.normalized;
        }
        return DylansSexyVector;
    }
}

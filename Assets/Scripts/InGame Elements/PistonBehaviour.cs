// Authors : François Deramaux
// Creation : 12/2015

using UnityEngine;
using System.Collections;

public class PistonBehaviour : MonoBehaviour 
{

	// PistonBehaviour describe manage pistons. It have to be applied to global piston prefab


	#region public properties

		public float m_speed;


	#endregion


	#region main methods

		void Start () 
		{
        setPistonHeight(countSize());

        Vector2 maxDown = transform.position;
        Vector2 maxUp = new Vector2(transform.position.x, transform.position.y + getPistonHeight());
        setStartPosition(maxDown);
        setEndPosition(maxUp);

        setMoveUp(true);
        setStartTime(Time.time);

        player = GameObject.FindGameObjectWithTag("Player");



    }

		void FixedUpdate () 
		{
        if (Mathf.Approximately(transform.position.y, getEndPosition().y))
        {
            print("SWITCH");
            switchMoveUp();
            setStartTime(Time.time);
            Vector2 newStart = getEndPosition();
            Vector2 newEnd = getStartPosition();
            setStartPosition(newStart);
            setEndPosition(newEnd);
        }

        movePiston();


    }







    private float countSize()
    {
        Renderer[] pistonComponents = GetComponentsInChildren<Renderer>();
        float pistonCompArraySize = pistonComponents.Length;
        float maxX = pistonComponents[0].bounds.max.y;
        float minX = pistonComponents[0].bounds.min.y;
        float maxXComponent;
        float minXComponent;

        for (int j = 1; j < pistonCompArraySize; j++)
        {
            // Update min and max x-position checking each component
            maxXComponent = pistonComponents[j].bounds.max.y;
            minXComponent = pistonComponents[j].bounds.min.y;

            if (maxXComponent > maxX)
            {
                maxX = maxXComponent;
            }

            if (minXComponent < minX)
            {
                minX = minXComponent;
            }
        }

        return (maxX - minX);
    }

    private void movePiston()
    {
        coveredDist = (Time.time - getStartTime()) * m_speed;
        fracJourney = coveredDist / pistonHeight;
        transform.position = Vector2.Lerp(startPosition, endPosition, fracJourney);
    }

    #endregion

    #region accessors

    public float getPistonHeight()
    {
        return pistonHeight;
    }

    public void setPistonHeight(float newPistonHeight)
    {
        if (newPistonHeight >= 0)
        {
            pistonHeight = newPistonHeight;
        }
    }

    public bool isMoveUp()
    {
        return moveUp;
    }

    public void setMoveUp(bool newMoveUp)
    {
        moveUp = newMoveUp;
    }

    public void switchMoveUp()
    {
        moveUp = !moveUp;
    }

    public Vector2 getStartPosition()
    {
        return startPosition;
    }

    public void setStartPosition(Vector2 newStartPosition)
    {
        startPosition = newStartPosition;
    }

    public Vector2 getEndPosition()
    {
        return endPosition;
    }

    public void setEndPosition(Vector2 newEndPosition)
    {
        endPosition = newEndPosition;
    }

    public float getStartTime()
    {
        return startTime;
    }

    public void setStartTime(float newStartTime)
    {
        startTime = newStartTime;
    }

    #endregion


    #region private properties


    private float pistonHeight;
    private bool moveUp;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float startTime;
    private float coveredDist;
    private float fracJourney;

    private GameObject player;


    Vector2 m_BaseInitialPosition;

	#endregion

}

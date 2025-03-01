using System.Collections.Generic;
using UnityEngine;

namespace SeedSnatcher.Movement
{
    public class SnatcherDive : MonoBehaviour, ISnatcherMovement
    {
        [SerializeField] private int diveStage = 0;
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private float speed = 0.1f;
        [SerializeField] private float positionTolerance = 0.5f;
        private List<Vector3> controlPoints;
        private List<Vector3> path;

        // creates the curve which the bird should dive
        // more control points may increase the dive steepness
        private void SetupDiveCurve()
        {
            var midpointTop = (startPosition + endPosition) / 2;
            midpointTop.y = startPosition.y;
            var midpointBottom = midpointTop;
            midpointBottom.y = endPosition.y;
            var midpointMidpoint = (midpointBottom + midpointTop) / 2;
            //var midpointTopMidpoint = (midpointMidpoint + midpointTop) / 2;
            //var midpointBottomMidpoint = (midpointMidpoint + midpointBottom) / 2;

            controlPoints = new List<Vector3>()
            {
                startPosition,
                midpointTop,
                //midpointTopMidpoint,
                midpointMidpoint,
                //midpointBottomMidpoint,
                midpointBottom,
                endPosition
            };
        }

        private void GeneratePath(int precision = 100)
        {
            path = new List<Vector3>();
            for (var i = 0; i < precision; i++)
            {
                var percentage = i / (precision * 1.0f);
                var targetPosition = BezierCurve.CalculateBezierPoint(controlPoints, percentage);
                path.Add(targetPosition);
                if (path.Count > 1)
                {
                    Debug.DrawLine(path[i], path[i - 1], Color.red, 90);
                }
            }
        }

        private void ReverseDiveCurve()
        {
            controlPoints.Reverse();
        }

        private void Dive()
        {
            if (diveStage >= path.Count - 1)
            {
                diveStage = 0;
                ReverseDiveCurve();
                GeneratePath();
            }

            var targetPosition = path[diveStage];
            var currentPosition = transform.position;
            if (Vector3.Distance(currentPosition, targetPosition) < positionTolerance) ++diveStage;
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
        }

        public void Init(Vector3 startPos, Vector3 endPos)
        {
            startPosition = startPos;
            endPosition = endPos;
            SetupDiveCurve();
            GeneratePath();
            Debug.DrawLine(startPosition, endPosition, Color.green, 90);
        }
        
        public void Loop()
        {
            Dive();
        }
    }
}
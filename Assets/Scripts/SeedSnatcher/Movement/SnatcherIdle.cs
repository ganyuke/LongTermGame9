using UnityEngine;

namespace SeedSnatcher.Movement
{
    public class SnatcherIdle : MonoBehaviour, ISnatcherMovement
    {
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 endPosition;

        [SerializeField] private float speed = 0.5f;

        // make sure the endPosition is way larger than this
        [SerializeField] private float positionTolerance = 0.5f;

        private bool HasReachedTarget()
        {
            var currentPosition = transform.position;
            return Vector3.Distance(currentPosition, endPosition) < positionTolerance;
        }

        // for flipping the bird (not *that* kind) when it's looping back
        private void FlipSprite()
        {
            // I'm using two separate objects to represent the bird
            // and I don't want to bother manually flipping both sprites.
            // gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            var localScale = transform.localScale;
            var flippedX = -localScale.x;
            transform.localScale = new Vector3(flippedX, localScale.y, localScale.z);
        }

        private void SwapPositions()
        {
            (startPosition, endPosition) = (endPosition, startPosition);
        }

        public void Init(Vector3 startPos, Vector3 endPos)
        {
            startPosition = startPos;
            endPosition = endPos;
        }
        
        public void Loop()
        {
            if (HasReachedTarget())
            {
                SwapPositions();
                FlipSprite();
            }

            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        }
    }
}
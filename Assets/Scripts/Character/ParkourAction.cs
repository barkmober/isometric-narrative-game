using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "Parkour/Parkour Action")]
    public class ParkourAction : ScriptableObject
    {
        public string animName;
        public bool rotateToObstacle;

        [SerializeField] float minHeight;
        [SerializeField] float maxHeight;

        [Header("Target Matching")]
        public bool targetMatching = true;
        public AvatarTarget matchBodyPart;
        public float matchStartTime;
        public float matchTargetTime;

        public Quaternion TargetRotation { get; set; }
        public Vector3 MatchPos { get; set; }

        public bool CheckIfPossible(ObstacleHitData hitData, Transform player)
        {
            float height = hitData.heightHit.point.y - player.position.y;

            if (height < minHeight || height > maxHeight)
                return false;

            if (rotateToObstacle)
                TargetRotation = Quaternion.LookRotation(-hitData.forwardHit.normal);

            if (targetMatching)
                MatchPos = hitData.heightHit.point;

            return true;
        }
    }
}
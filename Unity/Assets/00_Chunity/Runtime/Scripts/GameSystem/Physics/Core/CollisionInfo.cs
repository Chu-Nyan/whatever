namespace Chunity.GameSystem.Physics
{
    public readonly struct CollisionInfo
    {
        public readonly int PrimaryID;
        public readonly int TargetID;
        public readonly CollisionState State;

        public CollisionInfo(int primaryID, int targetID, CollisionState state)
        {
            PrimaryID = primaryID;
            TargetID = targetID;
            State = state;
        }
    }
}

namespace Chunity.GameSystem.Physics
{
    public interface INyanCollisionProvider
    {
        public NyanCollider Collider { get; }
        public void OnNyanCollisionEnter(INyanCollisionProvider provider);
        public void OnNyanCollisionExit(INyanCollisionProvider provider);
    }
}

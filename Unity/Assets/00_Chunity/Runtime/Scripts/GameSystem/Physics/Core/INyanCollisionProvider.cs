namespace Chunity.GameSystem.Physics
{
    public interface INyanCollisionProvider
    {
        public void OnNyanCollisionEnter(INyanCollisionProvider provider);
        public void OnNyanCollisionExit(INyanCollisionProvider provider);
    }
}

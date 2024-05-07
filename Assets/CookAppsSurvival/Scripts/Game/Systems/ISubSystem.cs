namespace CookApps.Game
{
    public interface ISubSystem
    {
        void Initialize(StageTemplate stage);
        void Deinitialize();
    }
}
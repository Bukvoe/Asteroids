namespace _Asteroids.CodeBase.Services.SceneLoad
{
    public interface ISceneLoadService
    {
        public void LoadScene(GameScene scene);

        public void ReloadCurrentScene();
    }
}

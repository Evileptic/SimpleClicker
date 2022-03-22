using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    [RequireComponent(typeof(SceneData))]
    sealed class Game : MonoBehaviour
    {
        private EcsWorld _ecsWorld;
        private EcsSystems _ecsSystems;

        [SerializeField] private RuntimeData _runtimeData;
        [SerializeField] private StaticData _staticData;
        [SerializeField] private SceneData _sceneData;

        void Start()
        {
            _ecsWorld = new EcsWorld();
            _ecsSystems = new EcsSystems(_ecsWorld);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_ecsWorld);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_ecsSystems);
#endif
            _runtimeData = new RuntimeData();

            _ecsSystems
                .Add(new InitializeSystem())
                .Add(new GenerateMenuSystem())
                .Add(new LevelPreviewSystem())
                .Add(new StartGameSystem())
                .Add(new SpawnTargetSystem())
                .Add(new TargetDamageSystem())
                .Add(new TimerSystem())
                .Add(new EndLevelSystem()).OneFrame<EndLevelEvent>()
                .Add(new SaveLeaderBoardSystem()).OneFrame<SaveLeaderBoardEvent>()
                .Add(new LoadLeaderBoardSystem()).OneFrame<LoadLeaderBoardEvent>()
                .Add(new ConfigMenuSystem())

                .Inject(_runtimeData)
                .Inject(_staticData)
                .Inject(_sceneData)
                .Init();
        }

        void Update() => _ecsSystems?.Run();

        void OnDestroy()
        {
            if (_ecsSystems != null)
            {
                _ecsSystems.Destroy();
                _ecsSystems = null;
                _ecsWorld.Destroy();
                _ecsWorld = null;
            }
        }
    }
}
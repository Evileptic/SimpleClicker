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
                .Add(new LoadPlayerDataSystem()).OneFrame<LoadPlayerDataEvent>()
                .Add(new SavePlayerDataSystem()).OneFrame<SavePlayerDataEvent>()
                .Add(new GenerateMenuSystem()).OneFrame<GenerateMenuEvent>()
                .Add(new OpenLevelPreviewSystem()).OneFrame<OpenLevelPreviewEvent>()
                .Add(new CloseLevelPreviewSystem()).OneFrame<CloseLevelPreviewEvent>()
                .Add(new ViewPlayerStatsSystem()).OneFrame<ViewPlayerStatsEvent>()
                .Add(new SetLevelsProgressSystem()).OneFrame<SetLevelProgressEvent>()
                
                .Add(new StartGameSystem())
                
                .Add(new PlayTimerSystem())
                .Add(new SpawnTargetSystem())
                .Add(new TargetDamageSystem()).OneFrame<TargetDamageEvent>()
                .Add(new EndLevelSystem()).OneFrame<EndLevelEvent>()
                .Add(new SpawnBonusSystem()).OneFrame<SpawnBonusEvent>()
                .Add(new BonusSystem()).OneFrame<BonusClickEvent>()
                
                .Add(new ConfigMenuSystem())
                
                .Add(new LoadLeaderBoardSystem()).OneFrame<LoadLeaderBoardEvent>()
                .Add(new SaveLeaderBoardSystem()).OneFrame<SaveLeaderBoardEvent>()
                
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
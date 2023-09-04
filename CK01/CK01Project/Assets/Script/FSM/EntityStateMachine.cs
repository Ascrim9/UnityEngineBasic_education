public class EntityStateMachine

{
    public EnemyState enemy_CurState { get; private set; }
    public PlayerState player_CurState { get; private set; }
    public BossState boss_CurState { get; private set; }


    public void Initialize(EnemyState _Enemy_startstate, PlayerState _Player_startState, BossState _Boss_startState)
    {
        if (_Enemy_startstate != null)
        {
            enemy_CurState = _Enemy_startstate;
            enemy_CurState.Enter();
        }
        if (_Player_startState != null)
        {
            player_CurState = _Player_startState;
            player_CurState.Enter();
        }
        if (_Boss_startState != null)
        {
            boss_CurState = _Boss_startState;
            boss_CurState.Enter();
        }

    }

    public void ChangeState(EnemyState _Enemy_newState, PlayerState _Player_newState, BossState _Boss_newState)
    {
        if (_Enemy_newState != null)
        {
            enemy_CurState.Exit();
            enemy_CurState = _Enemy_newState;
            enemy_CurState.Enter();
        }
        if (_Player_newState != null)
        {
            player_CurState.Exit();
            player_CurState = _Player_newState;
            player_CurState.Enter();
        }
        if (_Boss_newState != null)
        {
            boss_CurState.Exit();
            boss_CurState = _Boss_newState;
            boss_CurState.Enter();
        }
    }
}
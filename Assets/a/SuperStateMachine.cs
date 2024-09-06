using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>currentState管理基底クラス</summary>
public class SuperStateMachine1 : MonoBehaviour
{
    protected float timeEnteredState;

    public class State
    {
        /// <summary>状態ごとに異なる更新処理を保持するための Action デリゲート</summary>
        public Action DoSuperUpdate = DoNothing;
        /// <summary>状態遷移時に呼び出される Action デリゲート</summary>
        public Action EnterState = DoNothing;
        /// <summary>状態遷移時に呼び出される Action デリゲート</summary>
        public Action ExitState = DoNothing;
        /// <summary>現在の状態を表す Enum </summary>
        public Enum CurrentState;
    }

    public State _state = new();

    public Enum currentState
    {
        get
        {
            return _state.CurrentState;
        }
        set
        {
            if (_state.CurrentState == value) { return; }

            ChangingState();
            _state.CurrentState = value;
            ConfigureCurrentState();
        }
    }
    public Enum lastState;

    /// <summary>状態が変わる際に、現在の状態を lastState に保存し、
    /// 状態が変わった時点の時間を timeEnteredState に記録 </summary>
    void ChangingState()
    {
        lastState = _state.CurrentState;
        timeEnteredState = Time.time;
    }

    /// <summary>状態遷移を正しく行うための設定と初期化処理</summary>
    void ConfigureCurrentState()
    {
        //exitState の呼び出し: 現在の状態から抜けるときに実行される処理
        if (_state.ExitState is not null)
        {
            _state.ExitState();
        }
        ///新しい状態に応じて DoSuperUpdate や enterState などのデリゲートを再設定
        _state.DoSuperUpdate = ConfigureDelegate<Action>("SuperUpdate", DoNothing);
        _state.EnterState = ConfigureDelegate<Action>("EnterState", DoNothing);
        _state.ExitState = ConfigureDelegate<Action>("ExitState", DoNothing);

        //enterStateの呼び出し:現在の状態に入る時に実行される処理
        if (_state.EnterState is not null)
        {
            _state.EnterState();
        }
    }

    //辞書(キャッシュ)
    /// <summary>
    /// 引数>>
    /// Enum:　currentState
    /// string:　currentState_methodRootの形    　*methodRoot:SuperUpdateその状態時
    ///                                                      :EnterState入った時
    ///                                               　     :ExitState出た時
    ///Delegate: メソッド情報が入る。             *宣言がない場合Donothingがデフォルト値として入る。
    ///
    /// 戻り値はT型Delegateメソッド
    /// </summary>
    Dictionary<Enum, Dictionary<string, Delegate>> _cache = new();

    T ConfigureDelegate<T>(string methodRoot, T Default) where T : class
    {
        if (!_cache.TryGetValue(_state.CurrentState, out Dictionary<string,Delegate> lookup))
        {
            _cache[_state.CurrentState] = lookup = new();
        }
        if (!lookup.TryGetValue(methodRoot, out Delegate returnValue))
        {
            //リフレクション検索
            var mtd = GetType().GetMethod(_state.CurrentState.ToString() + "_" + methodRoot, System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

            if (mtd is not null)
            {
                returnValue = Delegate.CreateDelegate(typeof(T),this, mtd);
            }
            else
            {
                //null且つメソッドが見つからない
                //デフォルト設定
                returnValue = Default as Delegate;
            }
            lookup[methodRoot] = returnValue;
        }
        return returnValue as T;
    }
    /// <summary>状態時の更新 </summary>
    void SuperUpdate()
    {
        Debug.Log("SuperUpdate");
        EarlyGlobalSuperUpdate();

        _state.DoSuperUpdate();

        LateGlobalSuperUpdate();
    }

    /// <summary>DoSuperUpdateの前に実行可能なメソッド </summary>
    protected virtual void EarlyGlobalSuperUpdate() { }
    /// <summary> DoSuperUpdateの後に実行可能なメソッド</summary>
    protected virtual void LateGlobalSuperUpdate() { }


    /// <summary>ConfigureDelegate()で見つからないメソッドがある場合のデフォルト値 </summary>
    private static void DoNothing() { }
}

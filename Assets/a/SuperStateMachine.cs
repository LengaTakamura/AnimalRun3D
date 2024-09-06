using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>currentState�Ǘ����N���X</summary>
public class SuperStateMachine1 : MonoBehaviour
{
    protected float timeEnteredState;

    public class State
    {
        /// <summary>��Ԃ��ƂɈقȂ�X�V������ێ����邽�߂� Action �f���Q�[�g</summary>
        public Action DoSuperUpdate = DoNothing;
        /// <summary>��ԑJ�ڎ��ɌĂяo����� Action �f���Q�[�g</summary>
        public Action EnterState = DoNothing;
        /// <summary>��ԑJ�ڎ��ɌĂяo����� Action �f���Q�[�g</summary>
        public Action ExitState = DoNothing;
        /// <summary>���݂̏�Ԃ�\�� Enum </summary>
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

    /// <summary>��Ԃ��ς��ۂɁA���݂̏�Ԃ� lastState �ɕۑ����A
    /// ��Ԃ��ς�������_�̎��Ԃ� timeEnteredState �ɋL�^ </summary>
    void ChangingState()
    {
        lastState = _state.CurrentState;
        timeEnteredState = Time.time;
    }

    /// <summary>��ԑJ�ڂ𐳂����s�����߂̐ݒ�Ə���������</summary>
    void ConfigureCurrentState()
    {
        //exitState �̌Ăяo��: ���݂̏�Ԃ��甲����Ƃ��Ɏ��s����鏈��
        if (_state.ExitState is not null)
        {
            _state.ExitState();
        }
        ///�V������Ԃɉ����� DoSuperUpdate �� enterState �Ȃǂ̃f���Q�[�g���Đݒ�
        _state.DoSuperUpdate = ConfigureDelegate<Action>("SuperUpdate", DoNothing);
        _state.EnterState = ConfigureDelegate<Action>("EnterState", DoNothing);
        _state.ExitState = ConfigureDelegate<Action>("ExitState", DoNothing);

        //enterState�̌Ăяo��:���݂̏�Ԃɓ��鎞�Ɏ��s����鏈��
        if (_state.EnterState is not null)
        {
            _state.EnterState();
        }
    }

    //����(�L���b�V��)
    /// <summary>
    /// ����>>
    /// Enum:�@currentState
    /// string:�@currentState_methodRoot�̌`    �@*methodRoot:SuperUpdate���̏�Ԏ�
    ///                                                      :EnterState��������
    ///                                               �@     :ExitState�o����
    ///Delegate: ���\�b�h��񂪓���B             *�錾���Ȃ��ꍇDonothing���f�t�H���g�l�Ƃ��ē���B
    ///
    /// �߂�l��T�^Delegate���\�b�h
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
            //���t���N�V��������
            var mtd = GetType().GetMethod(_state.CurrentState.ToString() + "_" + methodRoot, System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

            if (mtd is not null)
            {
                returnValue = Delegate.CreateDelegate(typeof(T),this, mtd);
            }
            else
            {
                //null�����\�b�h��������Ȃ�
                //�f�t�H���g�ݒ�
                returnValue = Default as Delegate;
            }
            lookup[methodRoot] = returnValue;
        }
        return returnValue as T;
    }
    /// <summary>��Ԏ��̍X�V </summary>
    void SuperUpdate()
    {
        Debug.Log("SuperUpdate");
        EarlyGlobalSuperUpdate();

        _state.DoSuperUpdate();

        LateGlobalSuperUpdate();
    }

    /// <summary>DoSuperUpdate�̑O�Ɏ��s�\�ȃ��\�b�h </summary>
    protected virtual void EarlyGlobalSuperUpdate() { }
    /// <summary> DoSuperUpdate�̌�Ɏ��s�\�ȃ��\�b�h</summary>
    protected virtual void LateGlobalSuperUpdate() { }


    /// <summary>ConfigureDelegate()�Ō�����Ȃ����\�b�h������ꍇ�̃f�t�H���g�l </summary>
    private static void DoNothing() { }
}

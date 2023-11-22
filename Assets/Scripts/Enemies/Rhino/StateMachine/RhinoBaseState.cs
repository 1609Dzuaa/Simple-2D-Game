﻿using UnityEngine;

public abstract class RhinoBaseState 
{
    protected RhinoStateManager _rhinoStateManager;

    //Mỗi lần EnterState ở class con chỉ cần gọi base.EnterState
    public virtual void EnterState(RhinoStateManager rhinoStateManager) { _rhinoStateManager = rhinoStateManager; }

    public virtual void ExitState() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }
}

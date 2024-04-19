using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDisplayer
{
    public bool IsPlaying { get; set; }
    public void DestroySelf();
    public AnimationStateSO AnimationDisplaying { get; }


}

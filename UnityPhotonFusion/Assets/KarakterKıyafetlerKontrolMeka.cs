using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class KarakterKıyafetlerKontrolMeka : MonoBehaviour
{
    public SkeletonAnimation skeleton;

    private void Start()
    {
        skeleton = this.gameObject.transform.GetComponent<SkeletonAnimation>();
        skeleton.skeleton.SetSkin("Base");
        skeleton.initialSkinName = "Base";
    }
}

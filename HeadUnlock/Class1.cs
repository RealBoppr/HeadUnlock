using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using VRC.DataModel;

namespace HeadUnlock
{
    public class Class1
    {
        public class MyMod : MelonMod
        {
            private NeckRange orgin;
            public override void OnApplicationStart()
            {
                MelonLogger.Msg("Hold Alt to unlock head");
            }
            public override void OnApplicationLateStart()
            {
                //MelonLogger.Msg("Why are you still playing this game?");
            }

            public override void OnUpdate()
            {
                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    //Enables funny

                    this.orgin = ((LocomotionInputController)((Component)VRCPlayer.field_Internal_Static_VRCPlayer_0).GetComponent<GamelikeInputController>()).field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0;
                    ((LocomotionInputController)((Component)VRCPlayer.field_Internal_Static_VRCPlayer_0).GetComponent<GamelikeInputController>()).field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = new NeckRange(float.MinValue, float.MaxValue, 0.0f);
                    
                    MelonLogger.Msg("Unlocked Head Rotation");
                }

                if (Input.GetKeyUp(KeyCode.LeftAlt))
                {
                    //No more funny :(

                    ((LocomotionInputController)((Component)VRCPlayer.field_Internal_Static_VRCPlayer_0).GetComponent<GamelikeInputController>()).field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = this.orgin;

                    MelonLogger.Msg("Locked Head Rotation");
                }
            }

        }
    }
}

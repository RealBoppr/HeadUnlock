using System;
using MelonLoader;
using UnityEngine;
using ComfyUtils;
using VRC;
using VRC.DataModel;

[assembly: MelonColor(ConsoleColor.DarkBlue)]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonInfo(typeof(HeadUnlock.Main), "HeadUnlocker", "1.3", "Boppr (Original By Cloudie)")]

namespace HeadUnlock
{
    public class Main : MelonMod
    {
        private NeckMouseRotator Neck => Player.prop_Player_0._vrcplayer.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0;
        private Config Config => Helper.Config;
        private ConfigHelper<Config> Helper;
        private bool Unlocked = false;
        private KeyCode PressKeybind;
        private KeyCode HoldKeybind;
        private NeckRange Origin;
        public override void OnApplicationStart()
        {
            Helper = new ConfigHelper<Config>($"{MelonUtils.UserDataDirectory}\\HeadUnlockConfig.json");
            Helper.OnConfigUpdated += delegate
            {
                KeyCode lastHoldKeybind = HoldKeybind;
                HoldKeybind = Enum.TryParse(Config.HoldKeybind, out HoldKeybind) ? HoldKeybind : lastHoldKeybind;
                KeyCode lastPressKeybind = PressKeybind;
                PressKeybind = Enum.TryParse(Config.PressKeybind, out PressKeybind) ? PressKeybind : lastPressKeybind;
            };
            HoldKeybind = Enum.TryParse(Config.HoldKeybind, out HoldKeybind) ? HoldKeybind : KeyCode.LeftAlt;
            PressKeybind = Enum.TryParse(Config.PressKeybind, out PressKeybind) ? PressKeybind : KeyCode.H;
        }
        public override void OnUpdate()
        {
            if (Player.prop_Player_0 == null)
            {
                return;
            }
            if (Config.Toggle)
            {
                if (Input.GetKey(HoldKeybind) && Input.GetKeyDown(PressKeybind))
                {
                    ToggleUnlock(!Unlocked);
                }
            }
            else
            {
                if (Input.GetKeyDown(HoldKeybind))
                {
                    ToggleUnlock(true);
                }
                if (Input.GetKeyUp(HoldKeybind))
                {
                    ToggleUnlock(false);
                }
            }
        }
        private void ToggleUnlock(bool toggle)
        {
            if (toggle)
            {
                Origin = Neck.field_Public_NeckRange_0;
                Neck.field_Public_NeckRange_0 = new NeckRange(float.MinValue, float.MaxValue, 0.0f);
                Unlocked = true;
            }
            else
            {
                Neck.field_Public_NeckRange_0 = Origin;
                Unlocked = false;
            }
        }
    }
    public class Config
    {
        public string HoldKeybind { get; set; } = "LeftAlt";
        public string PressKeybind { get; set; } = "H";
        public bool Toggle { get; set; } = true;
    }
}

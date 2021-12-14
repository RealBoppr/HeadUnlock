using System;
using System.IO;
using MelonLoader;
using UnityEngine;
using VRC;
using VRC.DataModel;

[assembly: MelonColor(ConsoleColor.DarkBlue)]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonInfo(typeof(HeadUnlock.Main), "HeadUnlocker", "1.1", "Cloudie")]

namespace HeadUnlock
{
    public class Main : MelonMod
    {
        private NeckMouseRotator Neck => Player.prop_Player_0._vrcplayer.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0;
        private string KeybindFile = $"{MelonUtils.UserDataDirectory}\\HeadUnlock.txt";
        private NeckRange Orgin;
        private KeyCode Keybind;
        public override void OnApplicationStart()
        {
            if (!File.Exists(KeybindFile))
            {
                File.WriteAllText(KeybindFile, "LeftAlt");
            }
            Keybind = Enum.TryParse(File.ReadAllText(KeybindFile), out Keybind) ? Keybind : KeyCode.LeftAlt;
            FileSystemWatcher watcher = new FileSystemWatcher(Path.GetDirectoryName(KeybindFile), Path.GetFileName(KeybindFile))
            {
                NotifyFilter = NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };
            watcher.Changed += delegate (object obj, FileSystemEventArgs args)
            {
                KeyCode lastKeybind = Keybind;
                Keybind = Enum.TryParse(File.ReadAllText(KeybindFile), out Keybind) ? Keybind : lastKeybind;
            };
        }
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(Keybind) && Player.prop_Player_0 != null)
            {
                Orgin = Neck.field_Public_NeckRange_0;
                Neck.field_Public_NeckRange_0 = new NeckRange(float.MinValue, float.MaxValue, 0.0f);
            }
            if (Input.GetKeyUp(Keybind) && Player.prop_Player_0 != null)
            {
                Neck.field_Public_NeckRange_0 = Orgin;
            }
        }
    }
}

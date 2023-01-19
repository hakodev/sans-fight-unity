using ProtoBuf;
using System;

[ProtoContract, Serializable]
public class SaveData {
    [ProtoMember(1)]
    public bool IntroWatched { get; set; } = false;
    [ProtoMember(2)]
    public int DeathCount { get; set; } = 0;
    [ProtoMember(3)]
    public int SansKillCount { get; set; } = 0;
}

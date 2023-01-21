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
    [ProtoMember(4)]
    public int PlayMinutes { get; set; } = 0;
    [ProtoMember(5)]
    public int PlaySeconds { get; set; } = 0;
}

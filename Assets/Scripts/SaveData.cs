using ProtoBuf;
using System;

[ProtoContract, Serializable]
public class SaveData
{
    [ProtoMember(1)]
    public bool IntroWatched { get; set; }
}

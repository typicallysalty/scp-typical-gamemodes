using System.ComponentModel;
using Exiled.API.Interfaces;

namespace MurderMystery
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Sets the message Cassie says at the beginning of the game")]
        public string CassieMessageSCPRespawn { get; set; } = "Attention all personnel. One of you is a Chaos Insurgency intruder. No nearby MTFUNIT is available.";

        [Description("Sets how many Scouts can spawn [0 will disable scouts]")]
        public int ScoutNumber { get; set; } = 1;

        [Description("Sets if Bystanders/Scouts who shot another Bystander/Scout dies from an SCP-049 effect [false = USP]")]
        public bool DieBySCP049 { get; set; } = true;

        [Description("Sets the message broadcasted to Bystanders/Scouts when they shoot another innocent")]
        public string KillByInnocent { get; set; } = "You tried to kill an innocent, but ended up dying.";

        [Description("Sets the message broadcasted to Bystanders")]
        public string BystanderBroadcast { get; set; } = "You are a Bystander! Shoot who do you think is the intruder, but if you shoot the wrong person, you'll die!";

        [Description("Sets the message broadcasted to Scouts")]
        public string ScoutBroadcast { get; set; } = "You are a Scout! You have SCP-268, so use it when you hear gunfire and go check out who's shooting! You also have a USP!";

        [Description("Sets the message broadcasted to Intruders")]
        public string IntruderBroadcast { get; set; } = "You are the Chaos Insurgency intruder! You have a silenced MP7, plus a USP. Note that the pistol does not damage anyone. Upon decontamination, you will die immediately and lose!";

        [Description("Sets if Intruder loses by decontamination [if false, then he wins.]")]
        public bool IntruderLoseByDecontamination { get; set; } = true;

        [Description("Sets if some doors are locked [true will make the playground restricted to LCZ]")]
        public bool DoorsLocked { get; set; } = true;

        [Description("Sets the broadcast sent to a player when they try to shoot someone with the COM15.")]
        public string ComShootErrorMessage { get; set; } = "Nice try, but you can't use other guns than the USP!";
    }
}

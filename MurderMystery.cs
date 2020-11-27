using Exiled.API.Features;
using Exiled.API.Enums;

using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;
using Map = Exiled.Events.Handlers.Map;
using System;

namespace MurderMystery
{
    public class MurderMystery : Plugin<Config>
    {
        private static readonly Lazy<MurderMystery> LazyInstance = new Lazy<MurderMystery>(valueFactory: () => new MurderMystery());
        public static MurderMystery Instance => LazyInstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;
        private Handlers.Server server;
        private Handlers.Player player;

        private MurderMystery()
        {
        }

        public override void OnEnabled()
        {
        }

        public override void OnDisabled()
        {
        }

        public void RegisterEvents()
        {
            player = new Handlers.Player();
            server = new Handlers.Server();

            Player.Hurting += player.OnHurting;

            Server.RoundStarted += server.OnRoundStarted;
            Server.RespawningTeam += server.Respawning;

            Map.Decontaminating += server.OnDecontaminating;
        }
    }
}

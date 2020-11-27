using System.Linq;
using Exiled.API.Features;
using EPlayer = Exiled.API.Features.Player;
using Exiled.Events.EventArgs;

namespace MurderMystery.Handlers
{
    class Player
    {
        public void OnHurting(HurtingEventArgs ev) {
            if (ev.DamageType == DamageTypes.Usp)
            {
                var config = MurderMystery.Instance.Config;
                if (Server.intruders.Contains(ev.Target)) {
                    ev.Target.Kill(damageType: DamageTypes.Usp);
                } else if (ev.DamageType == DamageTypes.Com15)
                {
                    ev.IsAllowed = false;
                    ev.Attacker.Broadcast(duration: 5, message: config.ComShootErrorMessage);
                } else
                {
                    ev.IsAllowed = false;
                    if (config.DieBySCP049 == true)
                    {
                        ev.Attacker.Kill(damageType: DamageTypes.Scp049);
                    } else
                    {
                        ev.Attacker.Kill(damageType: DamageTypes.Usp);
                    }
                    ev.Attacker.Broadcast(duration: 5, message: config.KillByInnocent);
                }
            }
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (EPlayer.List.Where(x => x.Team != Team.RIP).Count() == 1) // This will be changed in future update
            {
                if (Server.intruders.Contains(EPlayer.List.Where(x => x.Team != Team.RIP).ElementAt(0))) // For later update
                {
                    Map.Broadcast(duration: 5, message: $"The CI Intruder wins! [{ev.Killer.Nickname}]");
                    Round.Restart();
                } else
                {
                    Map.Broadcast(duration: 5, message: $"{ev.Target.Nickname} was the CI Intruder and was killed! SCP Foundation wins!");
                    Round.Restart();
                }
            } else if (Server.intruders.Contains(ev.Target))
            {
                Map.Broadcast(duration: 5, message: $"{ev.Target.Nickname} was the CI Intruder and was killed! SCP Foundation wins!");
                Round.Restart();
            }
        }
    }
}

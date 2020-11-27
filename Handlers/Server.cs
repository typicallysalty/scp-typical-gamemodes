using System;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using EPlayer = Exiled.API.Features.Player;
using System.Collections.Generic;

namespace MurderMystery.Handlers
{
    class Server
    {

        public static List<EPlayer> scouts = EPlayer.List.ToList();
        public static List<EPlayer> intruders = EPlayer.List.ToList();
        public static List<EPlayer> bystanders = EPlayer.List.ToList();

        public void OnRoundStarted()
        {
            var config = MurderMystery.Instance.Config;
            var random = new Random();

            scouts = EPlayer.List.ToList();
            bystanders = EPlayer.List.ToList();
            intruders = EPlayer.List.ToList();

            for (int i = 0; i == 1; i++) // the reason for this for loop is for a later update
            {
                var random_index = random.Next(EPlayer.List.Count());
                intruders[random_index].Broadcast(duration: 5, message: config.IntruderBroadcast);
                bystanders.Remove(intruders[random_index]);
                scouts.Remove(intruders[random_index]);
                intruders[random_index].Inventory.AddNewItem(ItemType.GunMP7, s: 0, b: 1, o: 0);
                intruders[random_index].Inventory.AddNewItem(ItemType.GunUSP, s: 0, b: 0, o: 0);
                intruders[random_index].SetRole(newRole: RoleType.Scientist);
            }

            for (int s = 0; s == config.ScoutNumber; s++)
            {
                var random_index = random.Next(bystanders.Count());
                intruders.Remove(scouts[random_index]);
                bystanders.Remove(scouts[random_index]);
                scouts[random_index].Broadcast(duration: 5, message: config.ScoutBroadcast);
                scouts[random_index].Inventory.AddNewItem(ItemType.SCP268);
                scouts[random_index].Inventory.AddNewItem(ItemType.GunUSP, s: 0, b: 0, o: 0);
            }

            foreach (EPlayer player in bystanders)
            {
                player.SetRole(newRole: RoleType.Scientist);
                player.Broadcast(duration: 5, message: config.BystanderBroadcast);
                player.Inventory.AddNewItem(ItemType.GunUSP, s: 0, b: 0, o: 0);
                scouts.Remove(player);
                intruders.Remove(player);
            }
            foreach (Door door in Map.Doors)
            {
                if (door.DoorName == "CHECKPOINT_LCZ_A" || door.DoorName == "CHECKPOINT_LCZ_B" || door.DoorName == "914" || door.DoorName == "LCZ_ARMORY" || door.DoorName == "012")
                {
                    door.Networklocked = true;
                }
            }
        }

        public void OnDecontaminating(DecontaminatingEventArgs ev)
        {
            if (MurderMystery.Instance.Config.IntruderLoseByDecontamination == true) {
                intruders.ElementAt(0).Kill(damageType: DamageTypes.Asphyxiation);
            } else
            {
                foreach (EPlayer bystander in bystanders)
                {
                    bystander.Kill(damageType: DamageTypes.Asphyxiation);
                }
                foreach (EPlayer scout in scouts)
                {
                    scout.Kill(damageType: DamageTypes.Asphyxiation);
                }
            }
        }

        public void Respawning(RespawningTeamEventArgs ev)
        {
            ev.IsAllowed = false;
        }

    }
}
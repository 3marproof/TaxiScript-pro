﻿using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkServer;
using GTANetworkShared;

    public class TaxiCommands : Script
    {
        [Command("taxi")]
        public void calltaxi(Client sender)
        {
            API.call("Taxi", "useTaxis", sender);
            API.sendChatMessageToPlayer(sender, "~b~Calling a taxi for you");
        }

        [Command("job")]
        public void startjob(Client sender)
        {
            if ((bool)API.call("Taxi", "isincircle", (NetHandle)sender.handle))
            {

                VehicleHash taxi = API.vehicleNameToModel("taxi");
                Vehicle myvehicle = API.createVehicle(taxi, new Vector3(917.0233, -163.6854, 74.70861), new Vector3(0, 0, 143.9855), 0, 0, 0);
                API.setPlayerIntoVehicle(sender, myvehicle, -1);
                API.setEntityData(sender, "TAXI", true);
                API.sendChatMessageToPlayer(sender, "~g~You are now a Taxi driver, once you recieve a notification, type /accept to take the task");
            }

            else { API.setEntityPosition(sender, new Vector3(917.0233, -163.6854, 74.70861)); }

        }

        [Command("accept")] 
        public void acceptthetask(Client sender)
        {
            if (API.getEntityData(sender.handle, "TAXI"))
            {
                double id = API.random();
                API.call("Taxi", "accepted", (Client)sender, id);
            }
            else {
                API.sendChatMessageToPlayer(sender, "~r~You are not a job");
            }
        }

        [Command("done")]
        public void finishthetask(Client sender)
        {
            API.setEntityData(sender, "TASK",1.623482);
            API.sendChatMessageToPlayer(sender, "~g~You are now available to take Tasks");

        }

    }


﻿using Backend.Models;
using Backend.Models.BoardTypes;
using Backend.Models.Loggers;
using Backend.Models.Rooms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Backend
{


    public static class Apphost
    { 

        //global variables
        public static Logger logger;

        public static string CUR_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory;
        public static string DB_PATH = Path.Combine(CUR_DIRECTORY, "db.sqlite");

        public static ListRepositry ListOfResidents = new ListRepositry();
        public static ListRepositry ListOfPrivilegedWorkers = new ListRepositry();
        public static ListRepositry ListOfRoomServices = new ListRepositry();
        public static ListRepositry ListOfBookingInformation = new ListRepositry();
        public static ListRepositry ListOfRooms = new ListRepositry();

        private static Logger GetChainOfLoggers()
        {
            Logger fileLogger = new FileLogger(Logger.FileLogger);
            Logger consoleLogger = new ConsoleLogger(Logger.ConsoleLogger);
            consoleLogger.SetNextLogger(fileLogger);
            return consoleLogger;
        }
        public static void InitializeAllRooms()
        {
            for(int room = 1; room <= 90; room++)
            {
                if (room <= 30)
                {
                    ListOfRooms.list.Add((Room)new SingleRoom(
                            RoomTypes.Single,
                            2700,
                            RoomStatus.Available
                        ));
                }
                else if (room <= 60 && room > 30)
                {
                    ListOfRooms.list.Add((Room)new DoubleRoom(
                            RoomTypes.Double,
                            4000,
                            RoomStatus.Available
                        ));
                }
                else if (room <= 90 && room > 60)
                {
                    ListOfRooms.list.Add((Room)new TripleRoom(
                            RoomTypes.Triple,
                            4800,
                            RoomStatus.Available
                        ));
                }
            }
        }

        private static void InitializeFakeData()
        {
            ListOfBookingInformation.list = new List<object>()
            {
                new BookingInformation(ListOfRooms.list[0] as Room, BoardingTypesCache.GetBoardingType(BoardingTypes.Full), 1, TimeHandler.GetLastWeekInEpoch(), TimeHandler.GetTodayInEpoch()),
                new BookingInformation(ListOfRooms.list[33] as Room, BoardingTypesCache.GetBoardingType(BoardingTypes.Half), 2, TimeHandler.GetLastWeekInEpoch(), TimeHandler.GetTodayInEpoch()),
                new BookingInformation(ListOfRooms.list[63] as Room, BoardingTypesCache.GetBoardingType(BoardingTypes.BedAndBreakfast), 3, TimeHandler.GetLastWeekInEpoch(), TimeHandler.GetTodayInEpoch())
            };

            ListOfRoomServices.list = new List<object>()
            {
                new RoomService("Salma", 21, "salma@gmail.com", "0111991111", 5000, JobTitle.RoomService, "Monthly" ),
                new RoomService("Rawan", 21, "rawan@gmail.com", "01155551111", 6000, JobTitle.Manager , "Weekly"),
                new RoomService("Kareem", 21, "kareem@gmail.com", "0166661111", 15000, JobTitle.Receptionist, "Yearly" )

            };

            ListOfResidents.list = new List<object>();
            {

                new Resident("ali", 1, "aliagina@", "01111111", "1122334455");
                new Resident("moatez", 2, "mohamedmoatez@", "011111444", "112233664488");
                new Resident("Rawan", 3, "Rawan@", "0114546465777", "112231116688000");

            }

            ListOfPrivilegedWorkers.list = new List<object> {
                new Manager("Hossam El-Dien", 55, "hossam.eldien@gmail.com", "011192839924", 30000, JobTitle.Manager, "Weekly", "Hossam123"),
                new Manager("Sshraf Sherien", 40, "ashraf.sherien@gmail.com", "011134343466", 20000, JobTitle.Manager, "Monthly", "Ashraf123"),
                new Manager("Ahmed Fouda", 43, "ahmed.fouda@gmail.com", "011166090988", 15000, JobTitle.Manager, "Weekly", "Ahmed123"),

                new Receptionist("Mostafa Hussien", 27, "mostafa.hussien@gmail.com", "01004999999", 10000, JobTitle.Receptionist, "Weekly", "Mostafa123"),
                new Receptionist("Mona Yasser", 25, "mona.yasser@gmail.com", "01004993399", 10000, JobTitle.Receptionist, "Monthly", "Mona123"),
                new Receptionist("Salma Amin", 26, "salma.amin@gmail.com", "01004999911", 10000, JobTitle.Receptionist, "Monthly", "Salma123"),
            };

        }

        public static void InitializeApp()
        {
            logger = GetChainOfLoggers();
            BoardingTypesCache.LoadCache();
            InitializeAllRooms();
            InitializeFakeData();
        }
    }

    public struct JobTitle
    {
        public static string Manager = "Manager";
        public static string Receptionist = "Receptionist";
        public static string RoomService = "RoomService";
    }

    public struct RoomTypes
    {
        public static string Single = "Single";
        public static string Double = "Double";
        public static string Triple = "Triple";
    }

    public struct BoardingTypes
    {
        public static string Full = "Full";
        public static string Half = "Half";
        public static string BedAndBreakfast = "BedAndBreakfast";
    }

    public struct RoomStatus
    {
        public static string Reserved = "Reserved";
        public static string Available = "Available";
    }

    public struct Duration
    {
        public static string Weekly = "Weekly";
        public static string Monthly = "Monthly";
        public static string Yearly = "Yearly";
    }

}
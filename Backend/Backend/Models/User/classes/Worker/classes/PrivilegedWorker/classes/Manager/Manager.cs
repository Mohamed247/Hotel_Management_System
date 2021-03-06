using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Manager : PrivilegedWorker
    {
        public Manager(string userName, int age, string email, string phoneNumber, int salary, string jobTitle, string incomeType, string password) : 
            base(userName, age, email, phoneNumber, salary, jobTitle, incomeType, password) {}

        public static bool addWorker(Worker newWorker, string password)
        {
            if (UserAuthenticationServices.isUserNameFound(newWorker.userName)) return false;
            
            if (newWorker.jobTitle == JobTitle.Receptionist)
            {
                Receptionist receptionist = new Receptionist(newWorker.userName,newWorker.age,newWorker.email,newWorker.phoneNumber,newWorker.salary,newWorker.jobTitle,newWorker.incomeType, password);
                Apphost.ListOfPrivilegedWorkers.list.Add(receptionist);
            }
            else if(newWorker.jobTitle == JobTitle.Manager)
            {
                Manager manager = new Manager(newWorker.userName, newWorker.age, newWorker.email, newWorker.phoneNumber, newWorker.salary, newWorker.jobTitle, newWorker.incomeType, password);
                Apphost.ListOfPrivilegedWorkers.list.Add(manager);
            }
            else
            {
                RoomService roomService = (RoomService) newWorker;
                Apphost.ListOfRoomServices.list.Add(roomService);
            }
            return true;
        }

        public static bool deleteWorker(int id)
        {
            for (Iterator workerIterator = Apphost.ListOfRoomServices.GetIterator(); workerIterator.hasNext();)
            {
                RoomService worker = workerIterator.getNext() as RoomService;
                if (worker.id == id)
                {
                    Apphost.ListOfRoomServices.list.Remove(worker);
                    return true;
                }
            }

            for (Iterator workerIterator = Apphost.ListOfPrivilegedWorkers.GetIterator(); workerIterator.hasNext();)
            {
                PrivilegedWorker worker = workerIterator.getNext() as PrivilegedWorker;
                if (worker.id == id)
                {
                    Apphost.ListOfPrivilegedWorkers.list.Remove(worker);
                    return true;
                }
            }

            return false;



        }

        public static Worker getWorker(int id)
        {
            for (Iterator workerIterator = Apphost.ListOfRoomServices.GetIterator(); workerIterator.hasNext();)
            {
                Worker worker = workerIterator.getNext() as Worker;
                if (worker.id == id)
                {
                    return (RoomService)worker;
                }
            }
            for (Iterator workerIterator = Apphost.ListOfPrivilegedWorkers.GetIterator(); workerIterator.hasNext();)
            {
                PrivilegedWorker worker = workerIterator.getNext() as PrivilegedWorker;
                if (worker.id == id)
                {
                    if(worker.jobTitle == JobTitle.Receptionist)
                    {
                        worker = (Receptionist)worker;
                    }
                    else
                    {
                        worker = (Manager)worker;
                    }
                    return worker;
                }
            }
            return null;
        }

        public static bool editWorker(int oldId, Worker editedWorker, string password)
        {
            Worker oldWorker = getWorker(oldId);

            if (oldWorker.jobTitle == JobTitle.RoomService)
            {
                for (Iterator workerIterator = Apphost.ListOfRoomServices.GetIterator(); workerIterator.hasNext();)
                {
                    RoomService worker = workerIterator.getNext() as RoomService;
                    if (worker.id == oldId)
                    {
                        worker.email = editedWorker.email;
                        worker.age = editedWorker.age;
                        worker.jobTitle = editedWorker.jobTitle;
                        worker.incomeType = editedWorker.incomeType;    
                        worker.userName = editedWorker.userName;
                        worker.phoneNumber = editedWorker.phoneNumber;  
                        worker.salary = editedWorker.salary;
                        return true;
                    }
                }
            }
            else
            {
                for (Iterator workerIterator = Apphost.ListOfPrivilegedWorkers.GetIterator(); workerIterator.hasNext();)
                {
                    PrivilegedWorker worker = workerIterator.getNext() as PrivilegedWorker;
                   
                    if (worker.id == oldId)
                    {
                        worker.email = editedWorker.email;
                        worker.age = editedWorker.age;
                        worker.jobTitle = editedWorker.jobTitle;
                        worker.incomeType = editedWorker.incomeType;
                        worker.userName = editedWorker.userName;
                        worker.phoneNumber = editedWorker.phoneNumber;
                        worker.salary = editedWorker.salary;
                        worker.password = password;
                        return true;
                    }
                }
            }
            return false;
        }

        public static List<object> viewAllWorkers()
        {
            return Apphost.ListOfRoomServices.list.Concat(Apphost.ListOfPrivilegedWorkers.list).ToList();
        }
        public static List<object> viewAllResidents()
        {

            return Apphost.ListOfResidents.list;  
        }
        public static double getIncome(string duration)
        {
            long targetedDuration;
            double totalIncome = 0;
            TimeHandler timeHandler = TimeHandler.getInstance();
            if (duration == Duration.Weekly)
            {
                targetedDuration = timeHandler.GetLastWeekInEpoch();
            }
            else if(duration == Duration.Monthly)
            {
                targetedDuration = timeHandler.GetLastMonthInEpoch();
            }
            else
            {
                targetedDuration = timeHandler.GetLastYearInEpoch();
            }

            for (Iterator bookingIterator = Apphost.ListOfBookingInformation.GetIterator(); bookingIterator.hasNext();)
            {
                BookingInformation booking = bookingIterator.getNext() as BookingInformation;
                
                if (booking.endDate >= targetedDuration)
                {
                    totalIncome += booking.totalPrice;
                }
            }
            return totalIncome;
        }

    }
}
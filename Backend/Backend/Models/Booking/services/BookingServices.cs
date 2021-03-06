using Backend;
using Backend.Models;
using Backend.Models.BoardTypes;
using Backend.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

public static class BookingServices
{

    public static double GetBookingPrice(BoardingType board, Room room, long startDate, long endDate)
    {
        TimeHandler timeHandler = TimeHandler.getInstance();
        return (room.PricePerNight + board.price) * timeHandler.GetNumberOfDays(startDate, endDate);
    }
   
    public static BookingInformation MakeBooking(RoomAndBoarding bookingDetails, long startDate, long endDate, int residentId)
    {
        Room room = RoomFactory.GetRoom(bookingDetails.roomType, startDate, endDate);
        BoardingType board = BoardingTypesCache.GetBoardingType(bookingDetails.boardingType);
        BookingInformation bookingInformation = new BookingInformation(room, board, residentId, startDate, endDate);
        Apphost.ListOfBookingInformation.list.Add(bookingInformation);
        return bookingInformation;
    }

    public static BookingInformation EditBooking(int oldId,BookingInformation booking)
    {
        TimeHandler timeHandler = TimeHandler.getInstance();
        for (Iterator bookingIterator = Apphost.ListOfBookingInformation.GetIterator(); bookingIterator.hasNext();)
        {
            BookingInformation oldBooking = bookingIterator.getNext() as BookingInformation;
            if (oldBooking.id == oldId && oldBooking.residentId == booking.residentId)
            {
                oldBooking.roomId = booking.roomId;
                oldBooking.startDate = booking.startDate;
                oldBooking.endDate = booking.endDate;
                oldBooking.boardingType = booking.boardingType;
                if (oldBooking.startDate < timeHandler.GetTodayInEpoch())
                {
                    double oldPrice = GetBookingPrice(BoardingTypesCache.GetBoardingType(oldBooking.boardingType), Room.getRoomById(oldBooking.roomId), oldBooking.startDate, timeHandler.GetTodayInEpoch());
                    double newPrice = GetBookingPrice(BoardingTypesCache.GetBoardingType(booking.boardingType), Room.getRoomById(booking.roomId), timeHandler.GetTodayInEpoch(), booking.endDate);
                    oldBooking.totalPrice = newPrice + oldPrice;
                }
                else
                {
                    oldBooking.totalPrice = booking.totalPrice;

                }
                return oldBooking;
            }      
        }
        return null;
    }

    public static List<object> GetBookingInformations(int residentId)
    {
        List<object> bookingInformations = new List<object>();

        for (Iterator bookingIterator = Apphost.ListOfBookingInformation.GetIterator(); bookingIterator.hasNext();)
        {
            BookingInformation booking = bookingIterator.getNext() as BookingInformation;
            if (booking.residentId == residentId)
            {
                bookingInformations.Add(booking);
            }
        }
        return bookingInformations;
    }

    public static BookingInformation GetBooking(int id)
    {

        for (Iterator bookingIterator = Apphost.ListOfBookingInformation.GetIterator(); bookingIterator.hasNext();)
        {
            BookingInformation booking = bookingIterator.getNext() as BookingInformation;
            if (booking.id == id)
            {
                return booking;
            }
        }
        return null;
    }

    public static List<object> GetActiveBookingInformation()
    {
        List<object> activeBookingInformation = new List<object>();
        for (Iterator bookingIterator = Apphost.ListOfBookingInformation.GetIterator(); bookingIterator.hasNext();)
        {
            BookingInformation booking = bookingIterator.getNext() as BookingInformation;

            TimeHandler timeHandler = TimeHandler.getInstance();

            if (booking.endDate > timeHandler.GetTodayInEpoch())
            {
                activeBookingInformation.Add(booking);
            }
        }
        return activeBookingInformation;
    }

    public static List<object> GetAllBookingInformation()
    {
        return Apphost.ListOfBookingInformation.list;
    }  
    
    public static bool deleteBooking(int id)
    {
        TimeHandler timeHandler = TimeHandler.getInstance();
        for (Iterator bookingIterator = Apphost.ListOfBookingInformation.GetIterator(); bookingIterator.hasNext();)
        {
            BookingInformation booking = bookingIterator.getNext() as BookingInformation;
            if (id == booking.id)
            {
                if (booking.startDate < timeHandler.GetTodayInEpoch())
                {
                    return false;
                }
                Apphost.ListOfBookingInformation.list.Remove(booking);
                return true;
            }

        }
        return false;
    }
    

}
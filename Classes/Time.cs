using System;
using BerlinClock.Interfaces;

namespace BerlinClock.Classes
{
    public class Time : ITime
    {
        public Time(string inputTime)
        {
            var timeSections = inputTime.Split(':');
            Hours = Convert.ToInt32(timeSections[0]);
            Minutes = Convert.ToInt32(timeSections[1]);
            Seconds = Convert.ToInt32(timeSections[2]);
        }

        public int Hours { get; private set; }

        public int Minutes { get; private set; }

        public int Seconds { get; private set; }
    }
}

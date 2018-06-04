using System.Linq;
using System.Text;
using BerlinClock.Interfaces;

namespace BerlinClock.Classes
{
    public class TimeConverter : ITimeConverter
    {
        private const char YellowLight = 'Y';
        private const char RedLight = 'R';
        private const char OffLight = 'O';
        private const int FiveHoursGrouping = 5;
        public const int FourLightsGroup = 4;
        public const int ElevenMinutesLightsGroup = 11;
        public const int FiveMinutesGrouping = 5;
        public const int QuarterHourGrouping = 15;
        private static readonly string QuarterRepresentation = string.Concat(YellowLight, YellowLight, RedLight);
        private StringBuilder clockBuilder;

        public string ConvertTime(string inputTime)
        {
            var time = new Time(inputTime);
            return GenerateBerlinClockResult(time);
        }

        private string GenerateBerlinClockResult(ITime time)
        {
            clockBuilder = new StringBuilder(32);
            AppendSecondsSection(time.Seconds);
            AppendHoursSection(time.Hours);
            AppendMinutesSection(time.Minutes);

            return clockBuilder.ToString();
        }

        private void AppendHoursSection(int hours)
        {
            var fullFiveHourCycles = hours / FiveHoursGrouping;
            clockBuilder.Append(RedLight, fullFiveHourCycles)
                .Append(OffLight, FourLightsGroup - fullFiveHourCycles)
                .AppendLine();

            var individualHours = hours % FiveHoursGrouping;
            clockBuilder.Append(RedLight, individualHours)
                .Append(OffLight, FourLightsGroup - individualHours)
                .AppendLine();
        }

        private void AppendMinutesSection(int minutes)
        {
            var fullQuarters = minutes / QuarterHourGrouping;
            var fiveMinutesCycles = (minutes % QuarterHourGrouping) / FiveMinutesGrouping;
            clockBuilder.Append(string.Concat(Enumerable.Repeat(QuarterRepresentation, fullQuarters)))
                .Append(YellowLight, fiveMinutesCycles)
                .Append(OffLight, ElevenMinutesLightsGroup - (fullQuarters * QuarterRepresentation.Length) - fiveMinutesCycles)
                .AppendLine();

            var individualMinutes = minutes % FiveMinutesGrouping;
            clockBuilder.Append(YellowLight, individualMinutes)
                .Append(OffLight, FourLightsGroup - individualMinutes);
        }

        private void AppendSecondsSection(int seconds)
        {
            clockBuilder.Append(seconds % 2 == 0 ? YellowLight : OffLight)
                .AppendLine();
        }
    }
}
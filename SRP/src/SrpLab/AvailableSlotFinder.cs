using System;

namespace SrpLab;

public class AvailableSlotFinder
{
    private readonly AppointmentBusinessRules _rules;
    private readonly AppointmentDesk _appointment;

    public AvailableSlotFinder(AppointmentDesk appointment, AppointmentBusinessRules rules)
    {
        _appointment = appointment;
        _rules = rules;
    }

    public DateTimeOffset? FindNextSlot(DateTimeOffset from, int searchHours)
    {
        var cursor = Align(from);
        var end = from.AddHours(searchHours);
        while (cursor < end)
        {
            if (_rules.IsWithinBusinessHours(cursor) && !_appointment.GetBooked().Contains(cursor))
                return cursor;
            cursor = cursor.AddMinutes(_rules.SlotMinutes);
        }
        return null;
    }

    private DateTimeOffset Align(DateTimeOffset from)
    {
        var minutes = from.Minute - (from.Minute % _rules.SlotMinutes);
        return new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, minutes, 0, from.Offset);
    }
}


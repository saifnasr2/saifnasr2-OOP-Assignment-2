using System;

namespace SrpLab;

public class AppointmentDesk
{
    public AvailableSlotFinder SlotFinder {get; }
    public SmsNotification Notification {get; }
    private readonly AppointmentBusinessRules _rules;
    private readonly HashSet<DateTimeOffset> _booked = new();

    public AppointmentDesk(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        _rules = new AppointmentBusinessRules(open, close, slotMinutes);
        SlotFinder = new AvailableSlotFinder(this, _rules);
        Notification = new SmsNotification();
    }

    public IReadOnlyList<DateTimeOffset> GetBooked() => _booked.ToList();

    public bool TryBook(DateTimeOffset slot)
    {
        if (!_rules.IsWithinBusinessHours(slot) || _booked.Contains(slot)) return false;
        _booked.Add(slot);
        return true;
    }
   
}

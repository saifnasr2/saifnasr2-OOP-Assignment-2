using System;

namespace SrpLab;

public class IcsFormatter
{
    private readonly AppointmentBusinessRules _rules;
    public IcsFormatter(AppointmentBusinessRules rules)
    {
        _rules = rules;
    }

    public string ToIcs(DateTimeOffset slot, string patientName, string clinician)
    {
        // Calendar interoperability format changes with clients — not opening hours.
        var uid = Guid.NewGuid();
        var end = slot.AddMinutes(_rules.SlotMinutes);
        return "BEGIN:VCALENDAR\nVERSION:2.0\nBEGIN:VEVENT\n" +
               $"UID:{uid}\nDTSTART:{slot:yyyyMMdd'T'HHmmss'Z'}\nDTEND:{end:yyyyMMdd'T'HHmmss'Z'}\n" +
               $"SUMMARY:Visit {patientName} / {clinician}\nEND:VEVENT\nEND:VCALENDAR\n";
    }    
}

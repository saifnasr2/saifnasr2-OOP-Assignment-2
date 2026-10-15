namespace SrpLab;

public class ManagePiriority
{
    public string Piriority {get; private set; } = "P3";
    public DateTimeOffset OpenedAt {get; }
    private readonly SupportTicket _ticket;

    public ManagePiriority(SupportTicket ticket, DateTimeOffset openedAt)
    {
        _ticket = ticket;
        OpenedAt = openedAt;
    }

    public void RecalculatePiriorityFromText()
    {
        var blob = (_ticket.Subject + " " + _ticket.Body).ToLowerInvariant();
        if (blob.Contains("down") || blob.Contains("outage") || blob.Contains("cannot login"))
            Piriority = "P1";
        else if (blob.Contains("urgent") || blob.Contains("asap") || blob.Contains("blocked"))
            Piriority = "P2";
        else
            Piriority = "P3";
    }
    public DateTimeOffset SlaDeadLine()
    {
        var hours = Piriority switch
        {
            "P1" => 4,
            "P2" => 24,
            _ => 72
        };
        return OpenedAt.AddHours(hours);
    }
    public bool IsBreached (DateTimeOffset now) => now > SlaDeadLine();
}

namespace SrpLab;

public sealed class SupportTicket
{
    private readonly ManagePiriority _piriority;
    public TicketMessageFormatter _ticketMessage;
    public string Id {get; }
    public string Subject {get; private set; }
    public string Body {get; private set; }
    
    public SupportTicket(string id, string subject, string body, DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        _piriority = new ManagePiriority(this, openedAt);
        _ticketMessage = new TicketMessageFormatter(_piriority, this);
        _piriority.RecalculatePiriorityFromText();
    }
    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        _piriority.RecalculatePiriorityFromText();
    }
}

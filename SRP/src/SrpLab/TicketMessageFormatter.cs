using System;

namespace SrpLab;

public class TicketMessageFormatter
{
    private readonly ManagePiriority _piriority;
    private readonly SupportTicket _ticket;

    public TicketMessageFormatter(ManagePiriority piriority, SupportTicket ticket)
    {
        _piriority = piriority;
        _ticket = ticket;
    }

    public string DraftPublicReply(string agentName)
    {
        var apology = _piriority.Piriority == "P1" ? "We are treating this as a critical incident" : "Thanks for reaching out";
        return $"Hi,\n{apology}\nTicket {_ticket.Id} is with {agentName}. Next update before {_piriority.SlaDeadLine():u}.\n";
    }

    public string InternalEscalationBlurb()
    {
        return $"ESCALATE {_ticket.Id} piriority={_piriority.Piriority} breachAt={_piriority.SlaDeadLine():u} keywords-scanned=yes";
    }
}

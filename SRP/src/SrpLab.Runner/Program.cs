using SrpLab;

namespace SrpLabRunner;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("SrpLab — 10 intentional SRP violations (refactor me)");
        Console.WriteLine("=================================================");

        var ward = new WardBoard();
        ward.AssignBed(1, "p-88", heartRate: 130, spo2: 89);
        Console.WriteLine(ward.BuildHandoffNote(1));
        Console.WriteLine(string.Join(" | ", ward.DrainPagerLog()));


        var basket = new CheckoutBasket();
        var calc = new CalculateAmount(basket);
        var pay = new Payment(basket);
        basket.AddLine("SKU-1", 40m, 2);
        basket.ApplyCouponText("SAVE10");
        basket.EnableGiftWrap();
        Console.WriteLine($"basket total={calc.GrandTotal()} auth={pay.AuthorizePaymentStub("4242")}");


        var ticket = new SupportTicket("T-1", "cannot login", "prod is down for", DateTimeOffset.UtcNow);
        Console.WriteLine(ticket._ticketMessage.DraftPublicReply("Nora"));

        var loan = new LoanRiskCalculator(60_000m, 640, 4, hasCollateral: false);
        Console.WriteLine(loan._decision.DecisionLetter("Omar"));

        var course = new CourseRegistration("SEF-101", capacity: 1, tuition: 3000m);
        System.Console.WriteLine(course.Register("a@mail.com"));
        System.Console.WriteLine(course.Register("b@mail.com"));
        System.Console.WriteLine(course.Welcome.WelcomePacketMarkdown("b@mail.com", "Bea"));
    
        var kitchen = new ItemManagement();
        kitchen.AddItem("Pasta", new[] { "wheat", "milk" }, 12);
        Console.WriteLine(kitchen.ThermalTicket.RenderThermalTicket(42));
    
        var sub = new SubscriptionPeriod("c-9", 99m, new DateOnly(2026, 9, 1), new DateOnly(2026, 10, 1));
        sub.Handling.RegisterFailedPayment();
        Console.WriteLine(sub.Handling.DunningEmail("Sara", new DateOnly(2026, 9, 20)));
    
        var pick = new WarehouseManagement();
        pick.AddNeed("BOLT", "A", 3, 10, 7);
        pick.AddNeed("NUT", "B", 1, 5, 5);
        Console.WriteLine(pick.Script.PickerScript());

        var grades = new GradeBook();
        grades.Record("s1", 92);
        grades.Record("s1", 88);
        Console.WriteLine(grades.Transcript.TranscriptPlain("s1", "Ali"));
    
        var appt = new AppointmentDesk(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);
        var slot = appt.SlotFinder.FindNextSlot(DateTimeOffset.Parse("2026-09-21T08:00:00Z"), 48);
        if (slot is null) throw new InvalidOperationException("no slot");
        appt.TryBook(slot.Value);
        Console.WriteLine(appt.Notification.SmsReminder(slot.Value, "0100"));

        Console.WriteLine("Done. Now split responsibilities — without breaking behavior.");
    
    }
}

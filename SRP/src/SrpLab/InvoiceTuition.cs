using System;

namespace SrpLab;

public class InvoiceTuition
{
    private readonly CourseRegistration _course;
    public InvoiceTuition (CourseRegistration course)
    {
        _course = course;
    }

    public string TuitionInvoiceLine(string studentEmail)
    {
        if (!_course.IsSeated(studentEmail)) return $"{_course.CourseCode},WAITLIST,0.00";
        var vat = Math.Round(_course.Tuition * .14m, 2);
        return $"{_course.CourseCode},TUITION,{_course.Tuition:0.00},VAT,{vat:0.00},TOTAL,{(_course.Tuition + vat):0.00}";
    }
}

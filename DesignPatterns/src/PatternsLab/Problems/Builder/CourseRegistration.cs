using System;

namespace PatternsLab.Problems.Builder;

public class CourseRegistration
{
    public string StudentEmail { get; } 
    public string CourseCode { get; }   
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }    

    public CourseRegistration(
        string studentEmail,
        string courseCode,
        string accessMode,
        string? groupCode,
        string? discountCode,
        bool sendWhatsApp,
        bool sendEmailWelcome,
        string? mentorNote,
        DateOnly? preferredStart)
    {
        StudentEmail = studentEmail;
        CourseCode = courseCode;
        AccessMode = accessMode;
        GroupCode = groupCode;
        DiscountCode = discountCode;
        SendWhatsApp = sendWhatsApp;
        SendEmailWelcome = sendEmailWelcome;
        MentorNote = mentorNote;
        PreferredStart = preferredStart;
    }

    public override string ToString()
        => $"{StudentEmail} → {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";
}




public class BuilderCourse
{
    public string StudentEmail { get; } // required
    public string CourseCode { get; }   // required
    public string AccessMode { get; }   // required
    
    public string? GroupCode { get; private set; }
    public string? DiscountCode { get; private set; }
    public bool SendWhatsApp { get; private set; }
    public bool SendEmailWelcome { get; private set; }
    public string? MentorNote { get; private set; }
    public DateOnly? PreferredStart { get; private set; }
    
    public BuilderCourse(string studentEmail, string courseCode, string accessMode)
    {

        StudentEmail = studentEmail;
        CourseCode = courseCode;
        AccessMode = accessMode;
    }

    public BuilderCourse AddGroupCode(string? groupCode)
    {

        GroupCode = groupCode;
        return this;
    }

    public BuilderCourse AddDiscountCode(string? discountCode)
    {
        DiscountCode = discountCode;
        return this;
    }

    public BuilderCourse SendWhatsAppCode(bool enable)
    {
        SendWhatsApp = enable;
        return this;
    }

    public BuilderCourse SendEmail(bool enable)
    {
        SendEmailWelcome = enable;
        return this;
    }

    public BuilderCourse MentorNotes(string? note)
    {
        MentorNote = note;
        return this;
    }

    public BuilderCourse PreferredBeginning(DateOnly? preferredStart)
    {
        PreferredStart = preferredStart;
        return this;
    }

    public CourseRegistration Builder()
    {
        if (string.IsNullOrWhiteSpace(StudentEmail)) throw new ArgumentException("email required");
        if (string.IsNullOrWhiteSpace(CourseCode)) throw new ArgumentException("course required");
        if (AccessMode == "LiveGroup" && string.IsNullOrWhiteSpace(GroupCode))
            throw new InvalidOperationException("LiveGroup requires GroupCode");
        if (AccessMode == "VideosOnly" && !string.IsNullOrWhiteSpace(GroupCode))
            throw new InvalidOperationException("VideosOnly cannot have GroupCode");
        return new CourseRegistration(StudentEmail, CourseCode, AccessMode, GroupCode, DiscountCode, SendWhatsApp, SendEmailWelcome, MentorNote, PreferredStart);
    }

}
public static class RegistrationCallSites
{
    public static CourseRegistration CreateLiveStudentUgly()
    {
        return new BuilderCourse("sara@mail.com", "SEF-101", "LiveGroup")
                    .AddGroupCode("G1")
                    .AddDiscountCode("EARLY10")
                    .SendWhatsAppCode(true)
                    .SendEmail(true)
                    .MentorNotes( "Needs evening slot")
                    .PreferredBeginning(new DateOnly(2026, 10, 1))
                    .Builder();
    }

    public static CourseRegistration CreateVideosOnlyUgly()
    {
        return new BuilderCourse("ali@mail.com", "SEF-101", "VideosOnly")
                    .AddGroupCode(null)
                    .AddDiscountCode(null)
                    .SendWhatsAppCode(false)
                    .SendEmail(true)
                    .MentorNotes(null)
                    .PreferredBeginning(null)
                    .Builder();
    }

}

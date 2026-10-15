using System;

namespace SrpLab;

public class WelcomeMessage
{
    private readonly CourseRegistration _course;

    public WelcomeMessage (CourseRegistration course)
    {
        _course = course;
    }

    public string WelcomePacketMarkdown(string studentEmail, string studentName)
    {
        var status = _course.IsSeated(studentEmail) ? "confirmed seat" : $"waitlist #{_course.WaitlistPosition(studentEmail)}";
        return $"# Welcome to {_course.CourseCode}\nHi {studentName},\nYour status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: https://example.invalid/{_course.CourseCode.ToLowerInvariant()}\n";

    }
}

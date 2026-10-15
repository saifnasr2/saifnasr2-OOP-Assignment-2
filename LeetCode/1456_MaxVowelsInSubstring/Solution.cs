public class Solution
{
    public int MaxVowels(string s, int k)
    {
        int currentVowels = 0;

        // Count vowels in the first window.
        for (int i = 0; i < k; i++)
        {
            if (IsVowel(s[i]))
                currentVowels++;
        }

        int maxVowels = currentVowels;

        // Slide the window one character at a time.
        for (int i = k; i < s.Length; i++)
        {
            // Remove the character leaving the window.
            if (IsVowel(s[i - k]))
                currentVowels--;

            // Add the character entering the window.
            if (IsVowel(s[i]))
                currentVowels++;

            maxVowels = Math.Max(maxVowels, currentVowels);
        }

        return maxVowels;
    }

    private bool IsVowel(char c)
    {
        return c == 'a' || c == 'e' || c == 'i'
            || c == 'o' || c == 'u';
    }
}

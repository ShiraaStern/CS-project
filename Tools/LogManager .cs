using System;
using System.IO; 

namespace Tools;
public class LogManager
{
   private const  string LogFolder = "Log";

    public static string CurrentDir()
    {
        string dirName = DateTime.Now.ToString("yyyy-MM-dd");
        return Path.Combine(AppContext.BaseDirectory, LogFolder, dirName);
    }

    public static string CurrentFile() {

        string fileName = DateTime.Now.ToString("HH");
        return Path.Combine(CurrentDir(), fileName + ".txt");

    }
    public static void WriteLog( string projectName, string funcName,string message)
    {
        string dir = CurrentDir();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        string file = CurrentFile();
        if (!File.Exists(file))
            File.Create(file).Close();


        string line = $"{DateTime.Now}\t{projectName}.{funcName}:\t{message}";

        File.AppendAllText(file, line + Environment.NewLine);

    }

    public static void DeleteOldLogs()
    {

        string logRoot = Path.Combine(AppContext.BaseDirectory, LogFolder);

        // בדיקה אם התיקייה קיימת
        if (!Directory.Exists(logRoot))
            return;

        //חישוב תאריך לפני חודשיים
        DateTime limitDate = DateTime.Now.AddMonths(-2);

        //קבלת כל התיקיות בתיקיית הלוגים
        var directories = Directory.GetDirectories(logRoot);

        //לולאה שעוברת על כל התיקיות ובודקת אם שם התיקייה הוא תאריך בפורמט yyyy-MM-dd
        //ואם כן, האם הוא לפני התאריך המוגבל. אם כן, התיקייה נמחקת.
        foreach (var dir in directories)
        {
            string dirName = Path.GetFileName(dir);
            if (DateTime.TryParseExact(dirName, "yyyy-MM-dd", null,
                    System.Globalization.DateTimeStyles.None, out DateTime dirDate))
            {
                if (limitDate> dirDate)
                {
                    Directory.Delete(dir, true);
                }
            }
        }
    }
}


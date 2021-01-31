using Android.Media;
using Java.Interop;
using Java.IO;
using SQLite;
using System;
using System.Collections.Generic;
using Console = System.Console;

namespace ProjectX
{
    [Serializable, Table("Items")]
    public class DataTypeAlpha
    {
        public DataTypeAlpha(string title, string classNum, string info , int week )//, string time)
        {
            subjectTitle = title;
            subjectInfo = classNum;
            subjectClass = info;
            subjectDayofweek = week;
            //subjectTime = time;
        }
        public DataTypeAlpha() { }
        [Column("title")]
        public string subjectTitle { get; set; }
        [Column("info")]
        public string subjectInfo { get; set; }
        [Column("class")]
        public string subjectClass { get; set; }

        [Column("daysOfWeek")]
        public int subjectDayofweek { get; set; }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }

        [Column("time")]
        public string subjectTime { get; set; }

        public string getSubjectTitle() => subjectTitle;

        public string getSubjectInfo() => subjectInfo;

        public string getSubjectClass() => subjectClass;

        public int getSubjectDayofweek() => subjectDayofweek;

        
        public void print() //debug
        {
            Console.WriteLine(getSubjectTitle());
            Console.WriteLine(getSubjectClass());
            Console.WriteLine(getSubjectInfo());

        }

    }

    public class WeekDays
    {
        private static List<string>days = new List<string>
        {
            "Saturday",
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday"
        };


        public static List<KeyValuePair<string, int>> weekDaysListValue = new List<KeyValuePair<string, int>>
        {
            new KeyValuePair<string, int>("Saturday",0),
            new KeyValuePair<string, int>("Sunday",1),
            new KeyValuePair<string, int>("Monday",2),
            new KeyValuePair<string, int>("Tuesday",3),
            new KeyValuePair<string, int>("Wednesday",4),
            new KeyValuePair<string, int>("Thursday",5),
            new KeyValuePair<string, int>("Friday",6)

        };

        public List<KeyValuePair<string, int>> getWeekDaysListValue()
        {
            return weekDaysListValue;
        }

        public WeekDays()
        {
        }

        public List<string> getDayOfWeek()
        {
            return days;
        }

        public int getNumberOfDays { get { return days.Count; } }
    }
}
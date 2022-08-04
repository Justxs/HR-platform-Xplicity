﻿using System.ComponentModel.DataAnnotations;

namespace XplicityHRplatformBackEnd.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;  
       // public string[] DatesOfPastCalls { get; set; } = new string[] {}; //array?
        public string DateOfFutureCall { get; set; } = string.Empty;
        public bool OpenForSuggestions { get; set; } = false;

        public List<Technology> Technologies { get; set; }
        public List<CallDate> CallDates { get; set; }

    }
}

using System;
using Newtonsoft.Json;

namespace AppDemo2
{
    [JsonObject(MemberSerialization.OptOut)]
	public class Movie
	{
        public string Title { get; set; }
        public string Year { get; set; }
        public string Director { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }
	    public double Rating { get; set; }
    }
}


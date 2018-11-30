using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelotonData.JSONClasses.WorkoutSessionMetrics
{
    public class WorkoutSessionMetrics
    {
        public bool is_class_plan_shown { get; set; }
        public Average_Summaries[] average_summaries { get; set; }
        public Metric[] metrics { get; set; }
        public Segment_List[] segment_list { get; set; }
        public int duration { get; set; }
        public Summary[] summaries { get; set; }
        public int[] seconds_since_pedaling_start { get; set; }
    }

    public class Average_Summaries
    {
        public string display_name { get; set; }
        public string slug { get; set; }
        public float value { get; set; }
        public string display_unit { get; set; }
    }

    public class Metric
    {
        public string display_name { get; set; }
        public float max_value { get; set; }
        public float average_value { get; set; }
        public string display_unit { get; set; }
        public float[] values { get; set; }
        public string slug { get; set; }
        public int missing_data_duration { get; set; }
        public Zone[] zones { get; set; }
    }

    public class Zone
    {
        public string display_name { get; set; }
        public int max_value { get; set; }
        public int min_value { get; set; }
        public string range { get; set; }
        public int duration { get; set; }
        public string slug { get; set; }
    }

    public class Segment_List
    {
        public float intensity_in_mets { get; set; }
        public string name { get; set; }
        public int start_time_offset { get; set; }
        public string icon_url { get; set; }
        public string icon_name { get; set; }
        public string icon_slug { get; set; }
        public int length { get; set; }
        public string metrics_type { get; set; }
        public string id { get; set; }
    }

    public class Summary
    {
        public string display_name { get; set; }
        public string slug { get; set; }
        public float value { get; set; }
        public string display_unit { get; set; }
    }


}

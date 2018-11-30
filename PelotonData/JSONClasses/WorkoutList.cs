using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelotonData.JSONClasses.WorkoutList
{
    public class WorkoutList
    {
        public int count { get; set; }
        public int page_count { get; set; }
        public bool show_next { get; set; }
        public Next next { get; set; }
        public bool show_previous { get; set; }
        public string sort_by { get; set; }
        public int limit { get; set; }
        public object[] aggregate_stats { get; set; }
        public int total { get; set; }
        public RideDatum[] data { get; set; }
        public int page { get; set; }
    }

    public class Next
    {
        public int created_at { get; set; }
        public string workout_id { get; set; }
    }

    public class RideDatum
    {
        public string workout_type { get; set; }
        public float total_work { get; set; }
        public bool is_total_work_personal_record { get; set; }
        public string device_type { get; set; }
        public int device_time_created_at { get; set; }
        public string id { get; set; }
        public object fitbit_id { get; set; }
        public string peloton_id { get; set; }
        public string user_id { get; set; }
        public string title { get; set; }
        public bool has_leaderboard_metrics { get; set; }
        public bool has_pedaling_metrics { get; set; }
        public string platform { get; set; }
        public string metrics_type { get; set; }
        public string fitness_discipline { get; set; }
        public string status { get; set; }
        public int start_time { get; set; }
        public string name { get; set; }
        public string strava_id { get; set; }
        public int created { get; set; }
        public int created_at { get; set; }
        public int end_time { get; set; }
        public Ride ride { get; set; }
    }

    public class Ride
    {
        public int scheduled_start_time { get; set; }
        public bool is_live_in_studio_only { get; set; }
        public int rating { get; set; }
        public string content_provider { get; set; }
        public bool is_archived { get; set; }
        public int pedaling_end_offset { get; set; }
        public object live_stream_url { get; set; }
        public string series_id { get; set; }
        public bool sold_out { get; set; }
        public string instructor_id { get; set; }
        public int duration { get; set; }
        public float overall_estimate { get; set; }
        public string id { get; set; }
        public int total_ratings { get; set; }
        public string title { get; set; }
        public string difficulty_level { get; set; }
        public string live_stream_id { get; set; }
        public string ride_type_id { get; set; }
        public int length { get; set; }
        public int difficulty_rating_count { get; set; }
        public float? difficulty_estimate { get; set; }
        public string content_format { get; set; }
        public string location { get; set; }
        public float difficulty_rating_avg { get; set; }
        public bool has_closed_captions { get; set; }
        public int pedaling_duration { get; set; }
        public float trending_score { get; set; }
        public string fitness_discipline { get; set; }
        public string description { get; set; }
        public object sample_vod_stream_url { get; set; }
        public string[] ride_type_ids { get; set; }
        public object[] extra_images { get; set; }
        public int original_air_time { get; set; }
        public string studio_peloton_id { get; set; }
        public bool is_closed_caption_shown { get; set; }
        public string vod_stream_url { get; set; }
        public int total_in_progress_workouts { get; set; }
        public string home_peloton_id { get; set; }
        public int overall_rating_count { get; set; }
        public float overall_rating_avg { get; set; }
        public int pedaling_start_offset { get; set; }
        public int total_workouts { get; set; }
        public string language { get; set; }
        public bool is_explicit { get; set; }
        public bool has_pedaling_metrics { get; set; }
        public string image_url { get; set; }
        public string[] class_type_ids { get; set; }
        public string vod_stream_id { get; set; }
        public string fitness_discipline_display_name { get; set; }
        public Instructor instructor { get; set; }
    }

    public class Instructor
    {
        public string image_url { get; set; }
        public string name { get; set; }
    }
}

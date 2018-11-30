using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelotonData.JSONClasses.EventDetails
{

    public class EventDetails
    {
        public object[] excluded_platforms { get; set; }
        public Playlist playlist { get; set; }
        public Default_Album_Images default_album_images { get; set; }
        public bool is_ftp_test { get; set; }
        public Ride ride { get; set; }
        public Averages averages { get; set; }
        public Segments segments { get; set; }
        public Disabled_Leaderboard_Filters disabled_leaderboard_filters { get; set; }
    }

    public class Playlist
    {
        public string ride_id { get; set; }
        public Top_Artists[] top_artists { get; set; }
        public bool is_playlist_shown { get; set; }
        public bool is_top_artists_shown { get; set; }
        public bool is_in_class_music_shown { get; set; }
        public string id { get; set; }
        public Song[] songs { get; set; }
    }

    public class Top_Artists
    {
        public string artist_id { get; set; }
        public string artist_name { get; set; }
    }

    public class Song
    {
        public Album album { get; set; }
        public bool liked { get; set; }
        public string title { get; set; }
        public int start_time_offset { get; set; }
        public int cue_time_offset { get; set; }
        public Artist[] artists { get; set; }
        public string id { get; set; }
    }

    public class Album
    {
        public string image_url { get; set; }
        public string id { get; set; }
    }

    public class Artist
    {
        public string artist_id { get; set; }
        public string artist_name { get; set; }
    }

    public class Default_Album_Images
    {
        public string default_class_detail_image_url { get; set; }
        public string default_in_class_image_url { get; set; }
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
        public Equipment_Tags[] equipment_tags { get; set; }
        public string series_id { get; set; }
        public bool sold_out { get; set; }
        public bool is_favorite { get; set; }
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
        public float difficulty_estimate { get; set; }
        public string content_format { get; set; }
        public string location { get; set; }
        public float difficulty_rating_avg { get; set; }
        public bool has_closed_captions { get; set; }
        public int total_user_workouts { get; set; }
        public int pedaling_duration { get; set; }
        public float trending_score { get; set; }
        public string fitness_discipline { get; set; }
        public string description { get; set; }
        public object sample_vod_stream_url { get; set; }
        public string[] ride_type_ids { get; set; }
        public object[] extra_images { get; set; }
        public int original_air_time { get; set; }
        public string studio_peloton_id { get; set; }
        public int total_following_workouts { get; set; }
        public bool is_closed_caption_shown { get; set; }
        public string vod_stream_url { get; set; }
        public int total_in_progress_workouts { get; set; }
        public Instructor instructor { get; set; }
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
        public string[] equipment_ids { get; set; }
        public string fitness_discipline_display_name { get; set; }
    }

    public class Instructor
    {
        public bool is_visible { get; set; }
        public string last_name { get; set; }
        public bool featured_profile { get; set; }
        public int list_order { get; set; }
        public string music_bio { get; set; }
        public string id { get; set; }
        public string first_name { get; set; }
        public string user_id { get; set; }
        public string instagram_profile { get; set; }
        public object jumbotron_url_dark { get; set; }
        public string jumbotron_url { get; set; }
        public string spotify_playlist_uri { get; set; }
        public object web_instructor_list_gif_image_url { get; set; }
        public string strava_profile { get; set; }
        public string life_style_image_url { get; set; }
        public string instructor_hero_image_url { get; set; }
        public string username { get; set; }
        public string bio { get; set; }
        public string quote { get; set; }
        public string twitter_profile { get; set; }
        public string jumbotron_url_ios { get; set; }
        public string background { get; set; }
        public string film_link { get; set; }
        public string web_instructor_list_display_image_url { get; set; }
        public string facebook_fan_page { get; set; }
        public string name { get; set; }
        public string ios_instructor_list_display_image_url { get; set; }
        public object bike_instructor_list_display_image_url { get; set; }
        public bool is_filterable { get; set; }
        public string about_image_url { get; set; }
        public string[] fitness_disciplines { get; set; }
        public string short_bio { get; set; }
        public string[][] ordered_q_and_as { get; set; }
        public string image_url { get; set; }
        public string coach_type { get; set; }
    }

    public class Equipment_Tags
    {
        public string icon_url { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Averages
    {
        public int average_avg_resistance { get; set; }
        public float average_distance { get; set; }
        public int average_total_work { get; set; }
        public float average_avg_speed { get; set; }
        public int average_avg_power { get; set; }
        public int average_calories { get; set; }
        public int average_avg_cadence { get; set; }
    }

    public class Segments
    {
        public Segment_Body_Focus_Distribution segment_body_focus_distribution { get; set; }
        public Segment_Category_Distribution segment_category_distribution { get; set; }
        public Segment_List[] segment_list { get; set; }
    }

    public class Segment_Body_Focus_Distribution
    {
        public string cardio { get; set; }
        public string arms { get; set; }
    }

    public class Segment_Category_Distribution
    {
        public string CyclingWarmup { get; set; }
        public string Cycling_Arms { get; set; }
        public string cycling { get; set; }
        public string CyclingCoolDown { get; set; }
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

    public class Disabled_Leaderboard_Filters
    {
        public bool following { get; set; }
        public bool just_me { get; set; }
        public bool age_and_gender { get; set; }
    }

}

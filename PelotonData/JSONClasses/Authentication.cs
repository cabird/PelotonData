using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelotonData.JSONClasses
{

    public class AuthResponse
    {
        public Pubsub_Session pubsub_session { get; set; }
        public string user_id { get; set; }
        public User_Data user_data { get; set; }
        public string session_id { get; set; }
    }

    public class Pubsub_Session
    {
    }

    public class User_Data
    {
        public string phone_number { get; set; }
        public string last_name { get; set; }
        public bool is_demo { get; set; }
        public float weight { get; set; }
        public Contract_Agreements[] contract_agreements { get; set; }
        public bool is_profile_private { get; set; }
        public object cycling_ftp_workout_id { get; set; }
        public string created_country { get; set; }
        public int cycling_workout_ftp { get; set; }
        public float height { get; set; }
        public bool is_provisional { get; set; }
        public int cycling_ftp { get; set; }
        public object referral_code { get; set; }
        public int offense_count { get; set; }
        public string id { get; set; }
        public int total_pending_followers { get; set; }
        public bool block_explicit { get; set; }
        public object facebook_access_token { get; set; }
        public int customized_max_heart_rate { get; set; }
        public bool is_strava_authenticated { get; set; }
        public string obfuscated_email { get; set; }
        public object instructor_id { get; set; }
        public Paired_Devices[] paired_devices { get; set; }
        public int v1_referrals_made { get; set; }
        public int last_workout_at { get; set; }
        public string location { get; set; }
        public bool is_internal_beta_tester { get; set; }
        public object facebook_id { get; set; }
        public Workout_Counts[] workout_counts { get; set; }
        public bool has_active_digital_subscription { get; set; }
        public int total_non_pedaling_metric_workouts { get; set; }
        public Referral_Stats referral_stats { get; set; }
        public string username { get; set; }
        public object middle_initial { get; set; }
        public Quick_Hits quick_hits { get; set; }
        public bool can_charge { get; set; }
        public string first_name { get; set; }
        public int card_expires_at { get; set; }
        public int birthday { get; set; }
        public int subscription_credits_used { get; set; }
        public bool has_signed_waiver { get; set; }
        public object[] customized_heart_rate_zones { get; set; }
        public int referrals_made { get; set; }
        public bool is_external_beta_tester { get; set; }
        public string cycling_ftp_source { get; set; }
        public string name { get; set; }
        public int total_workouts { get; set; }
        public int default_max_heart_rate { get; set; }
        public int total_pedaling_metric_workouts { get; set; }
        public bool is_fitbit_authenticated { get; set; }
        public bool has_active_device_subscription { get; set; }
        public bool is_complete_profile { get; set; }
        public int created_at { get; set; }
        public string email { get; set; }
        public object member_groups { get; set; }
        public float[] default_heart_rate_zones { get; set; }
        public string image_url { get; set; }
        public int total_following { get; set; }
        public int estimated_cycling_ftp { get; set; }
        public string gender { get; set; }
        public int subscription_credits { get; set; }
        public int total_followers { get; set; }
    }

    public class Referral_Stats
    {
        public int num_workouts_taken { get; set; }
        public int referrals_made { get; set; }
    }

    public class Quick_Hits
    {
        public object speed_shortcuts { get; set; }
        public object incline_shortcuts { get; set; }
        public bool quick_hits_enabled { get; set; }
    }

    public class Contract_Agreements
    {
        public string contract_id { get; set; }
        public int agreed_at { get; set; }
        public string tread_contract_url { get; set; }
        public int contract_created_at { get; set; }
        public string contract_type { get; set; }
        public string bike_contract_url { get; set; }
    }

    public class Paired_Devices
    {
        public string serial_number { get; set; }
        public string name { get; set; }
        public string paired_device_type { get; set; }
    }

    public class Workout_Counts
    {
        public int count { get; set; }
        public string icon_url { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }

}

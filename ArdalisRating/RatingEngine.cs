﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace ArdalisRating
{
    public class PolicyReader
    {
        public string Read()
        {
            // load policy - open file policy.json
            return File.ReadAllText("policy.json");

        }
    }

    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
   

        public decimal Rating { get; set; }
        public Logger Logger { get; set; } = new Logger();
        public PolicyReader Reader { get; set; } = new PolicyReader();
        public void Rate()
        {
            Logger.Log("Starting rate.");

            Logger.Log("Loading policy.");

            var policyJson = Reader.Read();

            var policy = JsonConvert.DeserializeObject<Policy>(policyJson,
                new StringEnumConverter());

            switch (policy.Type)
            {
                case PolicyType.Auto:
                    AutoPolicyRater autoPolicyRater =new AutoPolicyRater(this,this.Logger);
                    autoPolicyRater.Rate(policy);
                    break;

                case PolicyType.Land:
                    LandPolicyRater landPolicyRater = new LandPolicyRater(this,this.Logger);
                    landPolicyRater.Rate(policy);
                    break;

                case PolicyType.Life:
                    LifePolicyRater lifePolicyRater = new LifePolicyRater(this,this.Logger);
                    lifePolicyRater.Rate(policy);
                    break;

                default:
                    Logger.Log("Unknown policy type");
                    break;
            }

            Logger.Log("Rating completed.");
            Logger.Log("Rating completed.");
        }
    }
}

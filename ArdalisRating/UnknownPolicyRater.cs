using System;
using System.Collections.Generic;
using System.Text;

namespace ArdalisRating
{
 public   class UnknownPolicyRater:Rater
    {
        public UnknownPolicyRater(RatingEngine engine, Logger logger) : base(engine, logger)
        {
        }

        public override void Rate(Policy policy)
        {
            _logger.Log("Uknown Policy rater");
        }
    }
}

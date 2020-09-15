﻿using System;

namespace ArdalisRating
{
    public class LifePolicyRater
    {
        private readonly RatingEngine _ratingEngine;
        private readonly Logger _logger;

        public LifePolicyRater(RatingEngine ratingEngine, Logger logger)
        {
            _ratingEngine = ratingEngine;
            _logger = logger;
        }

        public void Rate(Policy policy)
        {
            _logger.Log("Rating LIFE policy...");
            _logger.Log("Validating policy.");
            if (policy.DateOfBirth == DateTime.MinValue)
            {
                _logger.Log("Life policy must include Date of Birth.");
                return;
            }

            if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
            {
                _logger.Log("Centenarians are not eligible for coverage.");
                return;
            }

            if (policy.Amount == 0)
            {
                _logger.Log("Life policy must include an Amount.");
                return;
            }

            int age = DateTime.Today.Year - policy.DateOfBirth.Year;
            if (policy.DateOfBirth.Month == DateTime.Today.Month &&
                DateTime.Today.Day < policy.DateOfBirth.Day ||
                DateTime.Today.Month < policy.DateOfBirth.Month)
            {
                age--;
            }

            decimal baseRate = policy.Amount * age / 200;
            if (policy.IsSmoker)
            {
                _ratingEngine.Rating = baseRate * 2;
                return;
            }

            _ratingEngine.Rating = baseRate;
            return;
        }
    }
}
using System;
using FluentQueryBuilder.Application.Features;
using FluentQueryBuilder.Attributes;

namespace FluentQueryBuilder.Application.Models
{
    [FluentEntity("e_contacts")]
    public class Contact
    {
        [FluentProperty]
        public int ID { get; set; }

        [FluentProperty("p_name")]
        public string Name { get; set; }

        [FluentProperty("p_birthDate")]
        public DateTime BirthDate { get; set; }

        [FluentProperty("p_weight")]
        public double Wiegth { get; set; }

        [FluentProperty("p_doNotTrack", condition: FeatureToggle.DO_NOT_TRACK)]
        public string Info { get; set; }
    }
}

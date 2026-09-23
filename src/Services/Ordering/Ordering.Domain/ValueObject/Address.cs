using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace Ordering.Domain.ValueObject
{
    public record Address
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Street { get; init; } = default!;
        public string City { get; init; } = default!;
        public string State { get; init; } = default!;
        public string ZipCode { get; init; } = default!;
        public string Country { get; init; } = default!;

        protected Address(string firstName, string lastName, string email, string street, string city, string state, string zipCode, string country)
        {
            FirstName = firstName;
                LastName = lastName;
                Email = email;
                Street = street;
                City = city;
                State = state;
                ZipCode = zipCode;
                Country = country;
        }

        public static Address Of(string firstName, string lastName, string email, string street, string city, string state, string zipCode, string country)

        {
            ArgumentException.ThrowIfNullOrEmpty(firstName, nameof(firstName));
            ArgumentException.ThrowIfNullOrEmpty(lastName, nameof(lastName));

            return new Address(firstName, lastName, email, street, city, state, zipCode, country);
        }
    }
}

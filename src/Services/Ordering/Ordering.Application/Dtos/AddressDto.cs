using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Dtos
{
    public record AddressDto(
        string FirstName,
        string LastName,
        string Email,
        string Street,
        string City,
        string State,
        string ZipCode,
        string Country
    );

}

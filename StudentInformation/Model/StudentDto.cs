namespace StudentInformation.Model
{
    public record StudentDto(string FirstName, string LastName, string Contact, string Address, string Department);
    public record ContactAddressDto(string Contact, string Address);
}

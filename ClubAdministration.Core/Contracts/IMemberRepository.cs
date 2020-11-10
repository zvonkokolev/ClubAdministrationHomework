namespace ClubAdministration.Core.Contracts
{
    public interface IMemberRepository
    {
        bool IstMitgliedVorhanden(string lastName, string firstName, int id);
    }
}

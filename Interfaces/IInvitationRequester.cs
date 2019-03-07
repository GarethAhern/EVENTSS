namespace EventsAndInvitesUI.Interfaces
{
    public interface IInvitationRequester
    {
        //This is used for both bulk loading responses
        //and when manually changing responses by double clicking on datagridview
        void RefreshAllInvitesListView(string optionalSearchString = "");
    }
}

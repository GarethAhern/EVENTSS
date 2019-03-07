using EventsAndInvitesLogic.Models;

namespace EventsAndInvitesUI.Interfaces
{

    public interface ILocationRequester
    {
        void LocationComplete(LocationModel model);
    }
}

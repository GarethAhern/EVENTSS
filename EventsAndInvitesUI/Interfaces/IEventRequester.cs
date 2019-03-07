using EventsAndInvitesLogic.Models;

namespace EventsAndInvitesUI.Interfaces
{
    public interface IEventRequester
    {
        void EventComplete(EventModel model);
    }
}

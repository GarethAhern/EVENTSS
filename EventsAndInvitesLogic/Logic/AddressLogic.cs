namespace EventsAndInvitesLogic
{
   public static class AddressLogic
    {
        public static string ReturnAddressLineIfNotNull(string addressLine, ref int i)
        {
            string output = null;

            if (addressLine != null && addressLine.Length > 0)
            {
                output = addressLine;
                i++;
            }

            return output;
        }
    }
}

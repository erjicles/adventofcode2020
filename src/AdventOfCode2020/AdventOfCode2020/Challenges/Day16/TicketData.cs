using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Challenges.Day16
{
    public class TicketData
    {
        public IList<TicketField> TicketFields { get; private set; }
        public Ticket YourTicket { get; private set; }
        public IList<Ticket> NearbyTickets { get; private set; }

        public TicketData(
            IList<TicketField> ticketFields, 
            Ticket yourTicket, 
            IList<Ticket> nearbyTickets)
        {
            TicketFields = ticketFields;
            YourTicket = yourTicket;
            NearbyTickets = nearbyTickets;
        }
    }
}
